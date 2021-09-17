// Copyright (c) 2010 Daniel Wojciechowski 
// E-mail: at gmail.com daniel.wojciechowski

using System;
using System.Threading;
using System.Web;

namespace HttpPushFromMsSql
{
    public class CometAsyncResult:IAsyncResult
    {
        /// <summary> Some information about situation on client
        /// side. Last seen record ID. This is example parameter
        /// for clarification what are we waiting for. </summary>
        public int LastRecId;


        public DateTime StartTime;
        public TimeSpan WaitTime;

        /// <summary> Callback that will be called when operation is completed
        /// (When we have notification about hanges, or timeout)</summary>
        public AsyncCallback Callback;

        public HttpContext Context;
        /// <summary> True when there is notification, False if timeout </summary>
        public bool Result;
 
        /// <summary>
        /// Default constructor. Can be used when request completed
        /// synchronously. In this case must set CompletedSynchronously 
        /// to true.
        /// </summary>
        public CometAsyncResult()
        {
        }

        public CometAsyncResult(HttpContext context, AsyncCallback asyncCallback, int waitTime, int lastRecId)
        {
            Context = context;
            Callback = asyncCallback;
            StartTime = DateTime.Now;
            WaitTime = new TimeSpan(0, 0, waitTime);
            LastRecId = lastRecId; 
        }

        public bool IsCompleted { get; set; }

        public bool CompletedSynchronously { get; set; }

        private readonly WaitHandle _asyncWaitHandle;
        public WaitHandle AsyncWaitHandle { get { return _asyncWaitHandle; } }

        private readonly object _asyncState;
        public object AsyncState { get { return _asyncState; } }

    }
}
