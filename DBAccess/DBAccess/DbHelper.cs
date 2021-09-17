using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DBAccess
{
    public interface IDataAccessProperties
    {
        string Application { get; set; }
        string Database { get; set; }
        string User { get; set; }
        string Site { get; set; }
        string ConnectionString { get; set; }
    }

    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public sealed class DbHelper
    {
        #region Data

        readonly IDataAccessProperties _dataAccessProperties;
        string _error = "";

        #endregion

        #region Constants

        const string TransactionPatch = "\nIF @@TRANCOUNT > 0 ROLLBACK TRANSACTION\n";

        #endregion

        #region Construction

        /// <summary>
        /// 
        /// </summary>
        static DbHelper()
        {
            SqlConnectionString = "";
        }

        /// <summary>
        /// Used to construct DBAccess for a default database type (see DatabaseType)
        /// </summary>
        /// <param name="dataAccessProperties">Requirements</param>
        public DbHelper(IDataAccessProperties dataAccessProperties)
        {
            BulkCopyTimeout = 30 * 60 * 1000;
            Timeout = 5 * 60 * 1000;
            _dataAccessProperties = dataAccessProperties;
        }

        #endregion

        #region Properties

        public int Timeout { get; set; }
        public int BulkCopyTimeout { get; set; }
        public static string SqlConnectionString { get; set; }
        string LocalConnectionString
        {
            get { return _dataAccessProperties != null ? _dataAccessProperties.ConnectionString : ""; }
        }

        #endregion

        #region Operations

        public bool GetRecords(string strQuery, ref DataSet objDataSet, ref string strError)
        {
            try
            {
                return GetRecordsMs(strQuery, ref objDataSet, ref strError);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        public bool ExecuteSql(string query, ref string error)
        {
            try
            {
                return ExecuteSqlMs(query, ref error);
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
                using (var sqlConnection = new SqlConnection(_dataAccessProperties.ConnectionString))
                {
                    sqlConnection.InfoMessage += SqlInfoMessageEventHandler;
                    sqlConnection.Open();
                    SqlTransaction sqlTransaction = useTransaction ? sqlConnection.BeginTransaction() : null;
                    if (preCommads != null)
                    {
                        foreach (string command in preCommads)
                        {
                            using (SqlCommand sqlCommand = sqlTransaction != null
                                                               ? new SqlCommand(command, sqlConnection, sqlTransaction)
                                                               : new SqlCommand(command, sqlConnection))
                            {
                                sqlCommand.CommandTimeout = Timeout;
                                sqlCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    using (SqlBulkCopy sqlBulkCopy = useTransaction
                                                         ? new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default,
                                                                           sqlTransaction)
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

        bool GetRecordsMs(string strQuery, ref DataSet objDataSet, ref string strError)
        {
            try
            {
                _error = "";
                using (var connection = new SqlConnection(_dataAccessProperties.ConnectionString))
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
            strError = errors.Cast<SqlError>().Where(error => error.Number != 0 && error.Number != 5701).Aggregate(strError, (current, error) => current + (error.Message + ";"));
        }
        bool ExecuteSqlMs(string sql, ref string strError)
        {
            try
            {
                _error = "";
                using (var connection = new SqlConnection(_dataAccessProperties.ConnectionString))
                {
                    connection.InfoMessage += SqlInfoMessageEventHandler;
                    using (var command = new SqlCommand(TransactionPatch + sql + TransactionPatch, connection))
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
        public string FormatQueryTimeLog(string methodName, string logString, TimeSpan time)
        {
            return string.Format("QUERY TIME(ms)=[{3}], [{0}] [{1}], \r\n-- STARTSQL\r\n\t{2}\r\n-- ENDSQL",
                                 methodName, FormatConnectionInfo(), logString.Trim().Replace("\n", "\n\t"),
                                 time.TotalMilliseconds);
        }

        #endregion
    }
}
