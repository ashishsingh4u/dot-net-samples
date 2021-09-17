// Copyright (c) 2010 Daniel Wojciechowski 
// E-mail: at gmail.com daniel.wojciechowski

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace HttpPushFromMsSql
{
    /// <summary>
    /// Message data access layer
    /// </summary>
    public class MessageDal
    {
        const string ConnString = "Server=ASHISHKUMAR;Initial Catalog=Ashu;Integrated Security=SSPI;User Id=ashu;password=ashu";

        // Query for selecting data
        private const string Query = @"
SELECT RecId, Text, Time
FROM TestTable";

        // Query for making decision whether data was changed or we must wait.
        // In our case we are interested in new records so we select MAX.
        private const string QueryForCheck = @"
SELECT MAX(RecId)
FROM dbo.TestTable";

        // This query follows rules of creating query for "Query Notification"
        // and is filtered by record ID, because (in this case) we expect only
        // "INSERT" of new records. We are not observing old records. To be
        // compatibile with Query Notification we must use schema name ("dbo"
        // in our case) before table! For other compatibility issues you must
        // search MSDN for "Creating a Query for Notification" at
        // http://msdn.microsoft.com/en-us/library/ms181122.aspx
        // And don't look at this: (older - not full list):
        // "Special Considerations When Using Query Notifications" at
        // http://msdn.microsoft.com/en-us/library/aewzkxxh%28VS.80%29.aspx
        private const string QueryForNotification = @"
SELECT RecId
FROM dbo.TestTable
WHERE RecID > @recId
";


        //private static ManualResetEvent releasePhaseStartEvent = new ManualResetEvent(false); // used to wait for Query Notif.
        //private static ManualResetEvent releasePhaseEndEvent = new ManualResetEvent(false); // used to wait for Query Notif.

        private static SqlDependency _depend;
        private static readonly object DependSync = new object();

        // For simplicity CometAsyncHandler.ProcessAllWaitingClients() will be called
        // instead if OnQueryNotification
        //public delegate void OnQueryNotificationEventHandler();
        //public static OnQueryNotificationEventHandler OnQueryNotification = null;


        /// <summary>
        /// Executes simple select and convert result to list
        /// </summary>
        /// <param name="lastRecId">Last record ID</param>
        /// <param name="dateTime">Date and time of last inserted record.
        /// This value is returned only for debugging purposes</param>
        /// <returns></returns>
        public static List<string> GetMessageData(out int lastRecId, out DateTime dateTime)
        {
            var messageList = new List<string>();

            lastRecId = -1;
            dateTime = new DateTime();
            using (var conn = new SqlConnection(ConnString))
            using (var cmd = new SqlCommand(Query, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        lastRecId = (int) reader["RecId"];
                        dateTime = (DateTime) reader["Time"];
                        messageList.Add(reader["Text"] + " (Time of data: " + Tools.ToTimeStringWithMilisecond(dateTime) + ")");
                    }
            }

            return messageList;
        }

  
        
        /// <summary> This method will wait for changes in Sql Server. </summary>
        /// <returns>false if there is new data,
        /// true if you must wait for new data.</returns>
        public static bool WaitMessageDataAsync(int lastRecId)
        {
            lock (DependSync)
            {
                if (_depend != null)
                    return true; // "return" is to prevent creating many dependencies
                                 // "true" means you must wait for result (asynchrounous)

                using (var conn = new SqlConnection(ConnString))
                {
                    conn.Open();

                    // 1. First query, to check if we need to wait for changes
                    using (var cmd = new SqlCommand(QueryForCheck, conn))
                    {
                        object o = cmd.ExecuteScalar();
                        int max;
                        if (o is DBNull)
                            max = -1; // no records
                        else
                            max = Convert.ToInt32(o);

                        if (max > lastRecId) // if max > last seen recId
                        {
                            Trace.WriteLine("New data available right now!");
                            return false; // No async! New data available right now!
                        }
                    }


                    // 2. Second query, to run dependency
                    Trace.Write("Creating new dependency... recId: " + lastRecId);
                    Tools.EnableNotifications(ConnString);

                    SqlDataReader reader;

                    using (var qnCmd = new SqlCommand(QueryForNotification, conn))
                    {
                        qnCmd.Parameters.AddWithValue("@recId", lastRecId);

                        // Setup dependency which will be used to wait for changes
                        _depend = new SqlDependency(qnCmd);
                        _depend.OnChange += Depend_OnChangeAsync; // calback 1

                        // Execute second query to run dependency (Query Notif.),
                        // and to get content of our table data, that will be used
                        // by Query Notification as a starting point for observing
                        // changes.
                        reader = qnCmd.ExecuteReader();
                    }


                    // 3. Make sure that nothing has changed betwean point 1. and point 2.
                    //    (just before firing query notification)
                    bool newData = reader.HasRows;
                    reader.Close();
                    if (newData)
                    {
                        // very rare case - data changed before
                        // firing Query Notif. (SqlDependency)
                        // TODO: test this case by making some Sleep() betwean 1. and 2.

                        // We have new data and we decide not to receive notification:
                        _depend.OnChange -= Depend_OnChangeAsync;
                        _depend = null;

                        Trace.WriteLine("                       ...creating new dependency ABORTED!");

                        return false; // No async! New data available right now!
                    }
                    Trace.WriteLine("                       ...creating new dependency end.");
                }
            }
            return true; // true - you must wait for notification
        }


        private static void Depend_OnChangeAsync(object sender, SqlNotificationEventArgs e)
        {
            lock (DependSync)
            {
                var sqlDependency = (SqlDependency) sender;
                sqlDependency.OnChange -= Depend_OnChangeAsync;

                _depend = null; // current dependecy finished his work - mark it as null to create new

                // For simplicity it is not called as event
                CometAsyncHandler.ProcessAllWaitingClients(); 
            }
        }
    }
}

