using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
//using log4net;

namespace DBHelper
{
    /// <summary>
    /// Summary description for DBAccess.
    /// </summary>
    sealed public class DbAccess
    {
        //static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region Construction
        static DbAccess()
        {
            SqlConnectionString = "";
        }

        /// <summary>
        /// Used to construct DBAccess for a default database type (see DatabaseType)
        /// </summary>
        /// <param name="dataAccessProperties">Requirements</param>
        public DbAccess(IDataAccessProperties dataAccessProperties)
        {
            BulkCopyTimeout = 30 * 60 * 1000;
            Timeout = 5 * 60 * 1000;
            _dataAccessProperties = dataAccessProperties;
        }
        #endregion

        #region Data
        readonly IDataAccessProperties _dataAccessProperties;
        string _error = "";
        #endregion

        #region Constants
        const string TransactionPatch = "\nIF @@TRANCOUNT > 0 ROLLBACK TRANSACTION\n";
        #endregion

        #region Properties
        public int Timeout { get; set; }
        public int BulkCopyTimeout { get; set; }
        public static string SqlConnectionString { get; set; }
        public DatabaseType DatabaseType
        {
            get { return _dataAccessProperties.DatabaseType; }
            set { _dataAccessProperties.DatabaseType = value; }
        }
        string LocalConnectionString
        {
            get { return _dataAccessProperties != null ? _dataAccessProperties.ConnectionString : ""; }
        }
        #endregion

        #region Operations
        public bool GetRecords(ISqlRequest sqlRequest, ref DataSet objDataSet, ref string strError)
        {
            return GetRecords(sqlRequest.BuildSql(), ref objDataSet, ref strError);
        }
        bool GetRecords(string strQuery, ref DataSet objDataSet, ref string strError)
        {
            return GetRecords(strQuery, strQuery, ref objDataSet, ref strError);
        }
        bool GetRecords(string strQuery, string logString, ref DataSet objDataSet, ref string strError)
        {
            try
            {
                if (DatabaseType == DatabaseType.Odbc)
                    return GetOdbcRecords(_dataAccessProperties.Application, strQuery, logString, ref objDataSet, ref strError);
                return GetRecordsMs(_dataAccessProperties.Application, strQuery, logString, ref objDataSet, ref strError);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        public bool ExecuteSql(ISqlRequest sqlRequest, ref string error)
        {
            return ExecuteSql(sqlRequest.BuildSql(), ref error);
        }
        bool ExecuteSql(string query, ref string error)
        {
            return ExecuteSql(query, query, ref error);
        }
        bool ExecuteSql(string query, string logString, ref string error)
        {
            try
            {
                if (DatabaseType == DatabaseType.Odbc)
                    return ExecuteSqlOdbc(_dataAccessProperties.Application, query, logString, ref error);
                return ExecuteSqlMs(_dataAccessProperties.Application, query, logString, ref error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Bulk copy command
        /// </summary>
        /// <param name="application">Application name</param>
        /// <param name="dataTable">Data table to be copied into database</param>
        /// <param name="destinationTableName">Destination table name</param>
        /// <param name="useTransaction">true to use the transaction, false to execute directly</param>
        /// <param name="columnMappings">Column mappings</param>
        /// <param name="preCommads">SQL commands to be executed before the bulk copy command</param>
        /// <param name="error">Return error message</param>
        /// <returns>true on success, false on error</returns>
        public bool ExecuteBulkCopyMs(string application, DataTable dataTable, string destinationTableName, bool useTransaction, List<SqlBulkCopyColumnMapping> columnMappings, List<string> preCommads, ref string error)
        {
            try
            {
                _error = "";
                using (new AutoStopwatch(time => LogDetails(FormatQueryTimeLog(MethodBase.GetCurrentMethod().Name, dataTable.TableName, time))))
                {
                    using (var sqlConnection = new SqlConnection(GetSqlConnectionString(application)))
                    {
                        sqlConnection.InfoMessage += SqlInfoMessageEventHandler;
                        sqlConnection.Open();
                        SqlTransaction sqlTransaction = useTransaction ? sqlConnection.BeginTransaction() : null;
                        if (preCommads != null)
                        {
                            foreach (string command in preCommads)
                            {
                                using (
                                SqlCommand sqlCommand = sqlTransaction != null
                                ? new SqlCommand(command, sqlConnection, sqlTransaction)
                                : new SqlCommand(command, sqlConnection))
                                {
                                    sqlCommand.CommandTimeout = Timeout;
                                    sqlCommand.ExecuteNonQuery();
                                }
                            }
                        }
                        using (
                        SqlBulkCopy sqlBulkCopy = useTransaction
                        ? new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, sqlTransaction)
                        : new SqlBulkCopy(sqlConnection))
                        {
                            sqlBulkCopy.DestinationTableName = destinationTableName;
                            foreach (SqlBulkCopyColumnMapping mapping in columnMappings)
                            {
                                sqlBulkCopy.ColumnMappings.Add(mapping);
                            }
                            sqlBulkCopy.BulkCopyTimeout = BulkCopyTimeout;
                            sqlBulkCopy.WriteToServer(dataTable);
                            sqlBulkCopy.Close();
                            if (sqlTransaction != null)
                            {
                                sqlTransaction.Commit();
                            }
                        }
                        sqlConnection.Close();
                    }
                }
                return true;
            }
            catch (SqlException e)
            {
                BuildErrorString(ref error, e.Errors);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            error = "[CALDBERROR]" + error;
            return false;
        }
        #endregion

        #region Implementation
        string GetSqlConnectionString(string strApplication)
        {
            if (!string.IsNullOrEmpty(LocalConnectionString))
                return LocalConnectionString;
            if (!string.IsNullOrEmpty(SqlConnectionString))
                return SqlConnectionString;
            try
            {
                //RegistryKey regkey;
                //if (Manager.Server)
                //{
                //    regkey = Registry.LocalMachine.OpenSubKey("Software\\CALYON\\" + strApplication);
                //}
                //else
                //{
                //    regkey = Registry.CurrentUser.OpenSubKey("Software\\CALYON\\" + strApplication);
                //}
                //SQLConnectionString = (string)regkey.GetValue(_dataAccessProperties.DatabaseType == DatabaseType.MSNativeClient ? "SQLDB" : "DB");
                //regkey.Close();
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex);
            }
            return SqlConnectionString;
        }
        bool GetRecordsMs(string strApplication, string strQuery, string logString, ref DataSet objDataSet, ref string strError)
        {
            try
            {
                _error = "";
                using (new AutoStopwatch(time => LogDetails(FormatQueryTimeLog(MethodBase.GetCurrentMethod().Name, logString, time))))
                {
                    using (var connection = new SqlConnection(GetSqlConnectionString(strApplication)))
                    {
                        connection.InfoMessage += SqlInfoMessageEventHandler;
                        using (var dataAdapter = new SqlDataAdapter())
                        {
                            using (var command = new SqlCommand())
                            {
                                command.CommandText = TransactionPatch + strQuery + TransactionPatch;
                                command.CommandType = CommandType.Text;
                                command.Connection = connection;
                                command.CommandTimeout = Timeout;
                                dataAdapter.SelectCommand = command;
                                objDataSet = new DataSet();
                                if (dataAdapter.Fill(objDataSet) < 0)
                                {
                                    if (!string.IsNullOrEmpty(_error))
                                        throw new Exception(_error);
                                }
                            }
                            connection.Close();
                        }
                    }
                }
                return true;
            }
            catch (SqlException e)
            {
                BuildErrorString(ref strError, e.Errors);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            strError = "[CALDBERROR]" + strError;
            return false;
        }
        static void BuildErrorString(ref string strError, SqlErrorCollection errors)
        {
            // read http://support.microsoft.com/default.aspx?scid=KB;EN-US;197459
            // for the reason of ignoring.
            // In brief: when SET NOCOUNT ON sql server sends info messages
            // to the connected client, like when encountered with USE FXTP in the sql.
            // Unfortunately that message appears as error in errors collection
            // with Number == 5701. Number == 0 is generally for error message which
            // is actually just informational message.
            // Note: we are not ignoring 5703 on purpose since we don't intent to 
            // change the language.
            strError = errors.Cast<SqlError>().Where(error => error.Number != 0 && error.Number != 5701).Aggregate(strError, (current, error) => current + (error.Message + ";"));
        }

        bool ExecuteSqlMs(string strApplication, string strSql, string logString, ref string strError)
        {
            try
            {
                _error = "";
                using (new AutoStopwatch(time => LogDetails(FormatQueryTimeLog(MethodBase.GetCurrentMethod().Name, logString, time))))
                {
                    using (var connection = new SqlConnection(GetSqlConnectionString(strApplication)))
                    {
                        connection.InfoMessage += SqlInfoMessageEventHandler;
                        using (var command = new SqlCommand(TransactionPatch + strSql + TransactionPatch, connection))
                        {
                            command.Connection.Open();
                            command.CommandTimeout = Timeout;
                            if (command.ExecuteNonQuery() < 0)
                            {
                                if (!string.IsNullOrEmpty(_error))
                                    throw new Exception(_error);
                            }
                        }
                        connection.Close();
                    }
                }
                return true;
            }
            catch (SqlException e)
            {
                BuildErrorString(ref strError, e.Errors);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            strError = "[CALDBERROR]" + strError;
            return false;
        }
        void SqlInfoMessageEventHandler(object sender, SqlInfoMessageEventArgs e)
        {
            BuildErrorString(ref _error, e.Errors);
        }
        bool GetOdbcRecords(string strApplication, string strQuery, string logString, ref DataSet objDataSet, ref string strError)
        {
            try
            {
                _error = "";
                using (new AutoStopwatch(time => LogDetails(FormatQueryTimeLog(MethodBase.GetCurrentMethod().Name, logString, time))))
                {
                    using (var connection = new OdbcConnection(GetSqlConnectionString(strApplication)))
                    {
                        connection.InfoMessage += OdbcInfoMessageEventHandler;
                        using (var dataAdapter = new OdbcDataAdapter())
                        {
                            using (var command = new OdbcCommand())
                            {
                                command.CommandText = TransactionPatch + strQuery + TransactionPatch;
                                command.CommandType = CommandType.StoredProcedure;
                                command.Connection = connection;
                                command.CommandTimeout = Timeout;
                                dataAdapter.SelectCommand = command;
                                objDataSet = new DataSet();
                                if (dataAdapter.Fill(objDataSet) < 0)
                                {
                                    if (!string.IsNullOrEmpty(_error))
                                        throw new Exception(_error);
                                }
                            }
                            connection.Close();
                        }
                    }
                }
                return true;
            }
            catch (OdbcException e)
            {
                strError = e.Errors.Cast<OdbcError>().Aggregate(strError, (current, objOdbcError) => current + (objOdbcError.Message + ":" + objOdbcError.NativeError + ";"));
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            strError = strError.Replace("ERROR [HY000] [DataDirect][ODBC Sybase Wire Protocol driver][SQL Server]", "");
            strError = "[CALDBERROR]" + strError;
            return false;
        }
        bool ExecuteSqlOdbc(string strApplication, string strSql, string logString, ref string strError)
        {
            try
            {
                _error = "";
                using (new AutoStopwatch(time => LogDetails(FormatQueryTimeLog(MethodBase.GetCurrentMethod().Name, logString, time))))
                {
                    using (var connection = new OdbcConnection(GetSqlConnectionString(strApplication)))
                    {
                        connection.InfoMessage += OdbcInfoMessageEventHandler;
                        using (var command = new OdbcCommand(TransactionPatch + strSql + TransactionPatch, connection))
                        {
                            command.Connection.Open();
                            command.CommandTimeout = Timeout;
                            if (command.ExecuteNonQuery() < 0)
                            {
                                if (!string.IsNullOrEmpty(_error))
                                    throw new Exception(_error);
                            }
                        }
                        connection.Close();
                    }
                }
                return true;
            }
            catch (OdbcException e)
            {
                strError = e.Errors.Cast<OdbcError>().Aggregate(strError, (current, objOdbcError) => current + (objOdbcError.Message + ":" + objOdbcError.NativeError + ";"));
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            strError = strError.Replace("ERROR [HY000] [DataDirect][ODBC Sybase Wire Protocol driver][SQL Server]", "");
            strError = "[CALDBERROR]" + strError;
            return false;
        }
        void OdbcInfoMessageEventHandler(object sender, OdbcInfoMessageEventArgs e)
        {
            foreach (OdbcError objOdbcError in e.Errors)
            {
                _error += objOdbcError.Message + ":" + objOdbcError.NativeError + ";";
            }
        }
        #endregion

        #region Implementation
        string FormatConnectionInfo()
        {
            string connectionInfo;
            if (string.IsNullOrEmpty(LocalConnectionString))
            {
                connectionInfo = string.Format("{0}/{1}", "Server", SqlConnectionString);
            }
            else
            {
                connectionInfo = string.Format("{0}/{1}/{2}/{3}/{4}",
                _dataAccessProperties.Application, _dataAccessProperties.Database,
                _dataAccessProperties.User, _dataAccessProperties.Site,
                _dataAccessProperties.ConnectionString);
            }
            return connectionInfo;
        }
        string FormatQueryTimeLog(string methodName, string logString, TimeSpan time)
        {
            return string.Format("QUERY TIME(ms)=[{3}], [{0}] [{1}], \r\n-- STARTSQL\r\n\t{2}\r\n-- ENDSQL",
            methodName, FormatConnectionInfo(), logString.Trim().Replace("\n", "\n\t"), time.TotalMilliseconds);
        }

        static void LogDetails(string log)
        {
            //_log.Info(log);
        }

        static void LogError(string error, Exception ex)
        {
            //_log.Error(error, ex);
        }
        #endregion
    }
}
