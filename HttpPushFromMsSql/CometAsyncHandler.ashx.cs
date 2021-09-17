// Copyright (c) 2010 Daniel Wojciechowski 
// E-mail: at gmail.com daniel.wojciechowski

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace HttpPushFromMsSql
{
    public class CometAsyncHandler : IHttpAsyncHandler, IReadOnlySessionState
    {
        public static List<CometAsyncResult> AllWaitingClients = new List<CometAsyncResult>();
        public static object AllWaitingClientsSync = new object();
        private static bool _threadForTimeoutsWorking;

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public bool IsReusable
        {
            get { return true; }
        }

        ///<summary>Initiates an asynchronous call to the HTTP handler.</summary>
        ///
        ///<returns>An <see cref="T:System.IAsyncResult"></see> that contains
        /// information about the status of the process.
        /// </returns>
        ///
        ///<param name="context">An <see cref="T:System.Web.HttpContext"></see>
        /// object that provides references to intrinsic server objects (for
        /// example, Request, Response, Session, and Server) used to service
        /// HTTP requests. </param>
        ///<param name="extraData">Any extra data needed to process the request. </param>
        ///<param name="cb">The <see cref="T:System.AsyncCallback"></see> to
        /// call when the asynchronous method call is complete. If cb is null,
        /// the delegate is not called. </param>
        public IAsyncResult BeginProcessRequest(HttpContext context,
            AsyncCallback cb, object extraData)
        {
            context.Response.ContentType = "text/plain";

            // Get wait time from request
            int waitTime;
            ParseRequest(context.Request, out waitTime);

            // Get last seen record ID from Session
            object o = context.Session["LastRecId"];
            CometAsyncResult result;

            
            if (o == null) // some error handling
            {
                context.Response.Write("ERROR: Problem with session");
                result = new CometAsyncResult {CompletedSynchronously = true, IsCompleted = true};
                return result;
            }

            // Wait for data or timeout
            var lastRecId = (int) o;

            result = new CometAsyncResult(context, cb, waitTime, lastRecId);
            lock (AllWaitingClientsSync)
            {
                // register to Query Notification or complete
                // request synchronously in case if there is
                // already new data:
                if (!MessageDal.WaitMessageDataAsync(lastRecId))
                {
                    // if not waiting (there is new data)
                    // result is to be completed synchronously
                    result.IsCompleted = true;
                    result.CompletedSynchronously = true;
                    result.Result = true; // new data is available

                    WriteResponseToClient(result);
                    return result;
                }
                // asynchronous (normal case):
                AllWaitingClients.Add(result);
                if (AllWaitingClients.Count == 1)
                    StartClientTimeouter();
            }
            return result;
        }

        ///<summary>
        /// Provides an asynchronous process End
        /// method when the process ends.
        ///</summary>
        ///
        ///<param name="result">An <see cref="T:System.IAsyncResult"></see>
        /// that contains information about the status of the process. </param>
        public void EndProcessRequest(IAsyncResult result)
        {
            Debug.WriteLine("EndProcessRequest");
            var cometAsyncResult = (CometAsyncResult) result;
            
            if(cometAsyncResult.CompletedSynchronously)
                return;

            WriteResponseToClient(cometAsyncResult);
        }

        public void WriteResponseToClient(CometAsyncResult cometAsyncResult)
        {
            cometAsyncResult.Context.Response.Write(cometAsyncResult.Result ? "NEWDATAISAVAILABLE" : "TOOLONG-DOITAGAIN");
        }

        public static void ProcessAllWaitingClients()
        {
            ThreadPool.QueueUserWorkItem(
                delegate
                    {
                        lock (AllWaitingClientsSync) // this will block all new clients that wants to be inserted in list
                        {
                            Trace.WriteLine("Processing all clients: " + AllWaitingClients.Count);
                            foreach (CometAsyncResult asyncResult in AllWaitingClients)
                            {
                                asyncResult.Result = true; // New data available
                                asyncResult.Callback(asyncResult);
                                Trace.WriteLine("Callback() " + asyncResult.LastRecId);
                            }
                            AllWaitingClients.Clear();
                        }
                    });
        }

        public static void StartClientTimeouter()
        {
            lock (AllWaitingClientsSync)
            {
                if (_threadForTimeoutsWorking)
                    return;
                _threadForTimeoutsWorking = true;
            }

            ThreadPool.QueueUserWorkItem(
                delegate
                    {
                        int count;
                        lock (AllWaitingClientsSync)
                            count = AllWaitingClients.Count;
                        while( count > 0)
                        {
                            // Call Callback() to all timed out requests and
                            // remove from list.
                            lock (AllWaitingClientsSync)
                            {
                                DateTime now = DateTime.Now;
                                AllWaitingClients.RemoveAll(
                                    delegate(CometAsyncResult asyncResult)
                                        {
                                            if (asyncResult.StartTime.Add(asyncResult.WaitTime) < now)
                                            {
                                                asyncResult.Result = false; // timeout
                                                asyncResult.Callback(asyncResult);
                                                return true; // true for remove from list
                                            }

                                            return false; // not remove (because not timed out)
                                        });
                            }

                            // This sleep causes that some timeouted clients are removed with delay
                            // Example: if timeout=60s, sleep=1s then timeouted client can be removed after 60,7s.
                            // In some cases this can be considered as bug. TODO: Change it to WaitOne() and
                            // calculate proper sleep time
                            Thread.Sleep(1000); 
                            lock (AllWaitingClientsSync)
                            {
                                count = AllWaitingClients.Count;
                                if (count == 0)
                                    _threadForTimeoutsWorking = false;
                            }
                        }
                    });
        }

        private static void ParseRequest(HttpRequest request, out int waitTime)
        {
            const int defaultWaitTime = 1000 * 30;
            const int maxWaitTime = 1000 * 60 * 60;
            string waitTimeString = request["waitTime"];
            if (!int.TryParse(waitTimeString, out waitTime))
                waitTime = defaultWaitTime;
            if (waitTime < 0)
                waitTime = 0;
            if (waitTime > maxWaitTime)
                waitTime = maxWaitTime;
        }
    }
}
