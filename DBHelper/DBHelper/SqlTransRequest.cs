using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace DBHelper
{
    /// <summary>
    /// Sql transaction request string formatting class
    /// </summary>
    [Serializable]
    public class SqlTranRequest : ISqlRequest
    {
        #region Construction
        public SqlTranRequest()
        {
            ErrorParName = SqlConst.DefErrName;
            ReturnParName = SqlConst.DefRetName;
            UseTransaction = true;
            DatabaseName = "";
        }
        public SqlTranRequest(string databaseName)
        {
            ErrorParName = SqlConst.DefErrName;
            ReturnParName = SqlConst.DefRetName;
            UseTransaction = true;
            DatabaseName = databaseName;
        }
        public SqlTranRequest(string databaseName, bool useTransaction)
        {
            ErrorParName = SqlConst.DefErrName;
            ReturnParName = SqlConst.DefRetName;
            DatabaseName = databaseName;
            UseTransaction = useTransaction;
        }
        #endregion
        #region Data
        readonly List<SqlRequest> _requestList = new List<SqlRequest>();
        readonly List<ISqlRequest> _publishDataRequests = new List<ISqlRequest>();
        readonly StringCollection _prepSqlCollection = new StringCollection();
        #endregion
        #region Properties
        public string DatabaseName { get; set; }
        public bool UseTransaction { get; set; }
        public bool UseSqlError { get; set; }
        string ReturnParName { get; set; }
        string ErrorParName { get; set; }
        public string SqlString { get; private set; }
        public int Count
        {
            get { return _requestList.Count; }
        }
        public StringCollection PrepSqlCollection
        {
            get { return _prepSqlCollection; }
        }
        #endregion
        #region Operations
        public void AddRequest(SqlRequest sqlRequest)
        {
            _requestList.Add(sqlRequest);
        }
        /// <summary>
        /// Add publish data statement to sql request
        /// </summary>
        //public void AddPublishData(string function, int site, string key1, string key2, string key3, string key4,
        // int? fromInt, int? toInt, string fromStr, string toStr, DateTime? fromDate, DateTime? toDate)
        public void AddPublishData(string function, int site,
        int? fromInt, int? toInt, string fromStr, string toStr, DateTime? fromDate, DateTime? toDate,
        params string[] keyParams)
        {
            var sqlRequest = new SqlRequest("", "PublishData");
            sqlRequest.AddNamedParam("@Function", function);
            sqlRequest.AddNamedParam("@Site", site);
            if (fromInt.HasValue)
                sqlRequest.AddNamedParam("@FromInt", fromInt.Value);
            else
                sqlRequest.AddNamedParam("@FromInt", DBNull.Value);
            if (toInt.HasValue)
                sqlRequest.AddNamedParam("@ToInt", toInt.Value);
            else
                sqlRequest.AddNamedParam("@ToInt", DBNull.Value);
            if (fromStr != null)
                sqlRequest.AddNamedParam("@FromStr", fromStr);
            else
                sqlRequest.AddNamedParam("@FromStr", DBNull.Value);
            if (toStr != null)
                sqlRequest.AddNamedParam("@ToStr", toStr);
            else
                sqlRequest.AddNamedParam("@ToStr", DBNull.Value);
            if (fromDate.HasValue)
                sqlRequest.AddNamedParam("@FromDate", fromDate.Value, true);
            else
                sqlRequest.AddNamedParam("@FromDate", DBNull.Value);
            if (toDate.HasValue)
                sqlRequest.AddNamedParam("@ToDate", toDate.Value, true);
            else
                sqlRequest.AddNamedParam("@ToDate", DBNull.Value);
            int keyCount = 1;
            foreach (string keyParam in keyParams)
            {
                if (keyParam != null)
                    sqlRequest.AddNamedParam(string.Format("@Key{0}", keyCount), keyParam);
                else
                    sqlRequest.AddNamedParam(string.Format("@Key{0}", keyCount), DBNull.Value);
                keyCount++;
            }
            _publishDataRequests.Add(sqlRequest);
        }
        public void AddPublishData(ISqlRequest sqlRequest)
        {
            _publishDataRequests.Add(sqlRequest);
        }
        string BuildSql(bool addDeclareErrRet)
        {
            // Cleanup
            SqlString = "";
            var stringBuilder = new StringBuilder();
            // Add any preparation sql
            foreach (string prepSql in _prepSqlCollection)
            {
                stringBuilder.AppendLine(prepSql);
            }
            // Format beginning
            bool useDatabaseName = !string.IsNullOrEmpty(DatabaseName);
            stringBuilder.Append(FormatBegin(UseTransaction,
            UseSqlError,
            addDeclareErrRet,
            useDatabaseName,
            DatabaseName,
            ReturnParName,
            ErrorParName));
            // Requests
            // Vars out of the loop to speed things up
            string oldDbName = "";
            foreach (SqlRequest sqlRequest in _requestList)
            {
                // Memorize database Name and set to empty
                if (useDatabaseName)
                {
                    oldDbName = sqlRequest.DatabaseName;
                    sqlRequest.DatabaseName = "";
                }
                stringBuilder.Append(string.Format("IF {0} = 0 ", ReturnParName));
                if (UseSqlError)
                {
                    stringBuilder.AppendLine(string.Format("AND {0} = 0", ErrorParName));
                }
                if (UseSqlError || UseTransaction)
                {
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("BEGIN");
                    if (UseTransaction)
                    {
                        stringBuilder.AppendLine(string.Format("SET {0} = 1", ReturnParName));
                    }
                }
                stringBuilder.Append(sqlRequest.BuildSql(ReturnParName));
                stringBuilder.AppendLine();
                if (UseSqlError)
                {
                    stringBuilder.AppendLine(string.Format("SELECT {0} = @@ERROR", ErrorParName));
                }
                if (UseSqlError || UseTransaction)
                {
                    stringBuilder.AppendLine("END");
                }
                // Restore Database Name
                if (useDatabaseName)
                {
                    sqlRequest.DatabaseName = oldDbName;
                }
            }
            stringBuilder.Append(FormatEnd(UseTransaction, UseSqlError, ReturnParName, ErrorParName, _publishDataRequests));
            SqlString = stringBuilder.ToString();
            return SqlString;
        }
        #endregion
        #region Static helper methods

        static string FormatBegin(bool useTransaction = false, bool useSqlError = false, bool addDeclareErrRet = true, bool useDatabaseName = false, string databaseName = null, string returnParName = SqlConst.DefRetName, string errorParName = SqlConst.DefErrName)
        {
            var stringBuilder = new StringBuilder();
            //strRes += "SET NOCOUNT ON\n";
            //strRes += "SET ANSI_NULLS ON\n";
            if (useDatabaseName)
            {
                if (string.IsNullOrEmpty(databaseName))
                {
                    throw new ArgumentNullException("databaseName", "must be given when useDatabaseName is true");
                }
                stringBuilder.Append("USE ");
                stringBuilder.AppendLine('[' + databaseName + ']');
            }
            if (useTransaction)
            {
                stringBuilder.AppendLine("BEGIN TRANSACTION");
            }
            // Declaration of variables
            if (addDeclareErrRet)
            {
                stringBuilder.AppendLine(string.Format("DECLARE {0} int", returnParName));
                if (useSqlError)
                {
                    stringBuilder.AppendLine(string.Format("DECLARE {0} int", errorParName));
                }
                stringBuilder.AppendLine(string.Format("SELECT {0} = 0", returnParName));
                if (useSqlError)
                {
                    stringBuilder.AppendLine(string.Format("SELECT {0} = 0", errorParName));
                }
            }
            return stringBuilder.ToString();
        }

        static string FormatEnd(bool useTransaction, bool useSqlError, string returnParName, string errorParName, List<ISqlRequest> publishDataRequests)
        {
            var stringBuilder = new StringBuilder();
            if (useTransaction)
            {
                stringBuilder.Append(string.Format("IF {0} = 0 ", returnParName));
                if (useSqlError)
                {
                    stringBuilder.Append(string.Format("AND {0} = 0 ", errorParName));
                }
                if (publishDataRequests.Count == 0)
                {
                    stringBuilder.AppendLine("COMMIT TRANSACTION");
                    stringBuilder.AppendLine("ELSE");
                }
                else
                {
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("BEGIN");
                    stringBuilder.AppendLine("COMMIT TRANSACTION");
                    stringBuilder.AppendLine("IF @@ERROR = 0");
                    stringBuilder.AppendLine("BEGIN");
                    foreach (var strPublishDataRequest in publishDataRequests)
                    {
                        stringBuilder.AppendLine(strPublishDataRequest.BuildSql());
                    }
                    stringBuilder.AppendLine("END");
                    stringBuilder.AppendLine("END");
                    stringBuilder.AppendLine("ELSE");
                }
                stringBuilder.AppendLine("BEGIN");
                stringBuilder.AppendLine("ROLLBACK TRANSACTION");
                stringBuilder.AppendLine("RAISERROR 51001 'Could not process request.'");
                stringBuilder.Append("END");
            }
            else
            {
                if (publishDataRequests.Count > 0)
                {
                    stringBuilder.Append(string.Format("IF {0} = 0 ", returnParName));
                    if (useSqlError)
                    {
                        stringBuilder.Append(string.Format("AND {0} = 0 ", errorParName));
                    }
                    stringBuilder.AppendLine("IF @@ERROR = 0");
                    stringBuilder.AppendLine("BEGIN");
                    foreach (var strPublishDataRequest in publishDataRequests)
                    {
                        stringBuilder.AppendLine(strPublishDataRequest.BuildSql());
                    }
                    stringBuilder.AppendLine("END");
                }
            }
            return stringBuilder.ToString();
        }
        #endregion
        #region ISqlRequest Members
        public string BuildSql()
        {
            return BuildSql(true);
        }
        #endregion
        #region Logging
        public override string ToString()
        {
            return BuildSql();
        }
        #endregion
    }
}
