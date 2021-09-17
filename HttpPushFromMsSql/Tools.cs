// Copyright (c) 2010 Daniel Wojciechowski 
// E-mail: at gmail.com daniel.wojciechowski

using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HttpPushFromMsSql
{
    public static class Tools
    {
        /// <summary>
        /// Convert DateTime to string containing time with miliseconds
        /// </summary>
        public static string ToTimeStringWithMilisecond(DateTime time)
        {   // Based on http://msdn.microsoft.com/en-us/library/bb882581.aspx
            // Append millisecond pattern (".fff") to current culture's time pattern
            string miliPattern = DateTimeFormatInfo.CurrentInfo.LongTimePattern;
            miliPattern = Regex.Replace(miliPattern, "(:ss|:s)", "$1.fff");
            return time.ToString(miliPattern);
        }

        private static bool _notificationEnabled;
        public static void EnableNotifications(string connString)
        {
            if (_notificationEnabled) // prevent for calling twice 
                return; // TODO: Do it in other way. For example call
                        // those methods on application start:

            // Go to following link to fix problems with user rights to Query Notification:
            // http://www.simple-talk.com/sql/t-sql-programming/using-and-monitoring-sql-2005-query-notification/

            System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(connString);
            bool startResult = SqlDependency.Start(connString);
            Debug.WriteLineIf(!startResult, "SqlDependency.Start(\"" + connString +
                                           "\") returned false: compatible listener already exists.");
            _notificationEnabled = true;
        }

    }
}
