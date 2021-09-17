using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace DBHelper
{
    /// <summary>
    /// Sql request string formatting class
    /// </summary>
    [Serializable]
    public class SqlRequest : ISqlRequest
    {
        #region Constants
        const string DateTimeFormat = "dd MMM yyyy HH:mm:ss:fff";
        const string DateFormat = "dd MMM yyyy";
        #endregion

        #region Construction
        public SqlRequest(string databaseName, string spName)
        {
            _databaseName = databaseName;
            _spName = spName;
        }
        #endregion

        #region Data
        readonly StringCollection _paramCollection = new StringCollection();
        readonly Dictionary<string, string> _namedParamCollection = new Dictionary<string, string>();
        //readonly StringCollection _outputParamCollection = new StringCollection();
        readonly Dictionary<string, string> _outputParamCollection = new Dictionary<string, string>();
        readonly StringCollection _prepSqlCollection = new StringCollection();
        readonly StringCollection _postSqlCollection = new StringCollection();
        #endregion

        #region Properties
        public static char StoredProcedureMode
        {
            get
            {
                if (!ForceClientMode)
                {
                    return SqlConst.StoredProcedureModeServer;
                }
                return SqlConst.StoredProcedureModeClient;
            }
        }
        // FXJ1-4183 Allow the External API server to fake Client Mode
        public static bool ForceClientMode { get; set; }
        string _databaseName;
        public string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }
        string _spName;
        public string SpName
        {
            get { return _spName; }
            set { _spName = value; }
        }
        public StringCollection PrepSqlCollection
        {
            get { return _prepSqlCollection; }
        }
        public StringCollection PostSqlCollection
        {
            get { return _postSqlCollection; }
        }
        string _sqlString;
        public string SqlString
        {
            get { return _sqlString; }
        }
        bool _validateDates = true;
        /// <summary>
        /// When set to true dates will be validated to assure they fall within the SQL-accepted range
        /// </summary>
        public bool ValidateDates
        {
            get { return _validateDates; }
            set { _validateDates = value; }
        }
        #endregion

        #region Parameter Processing
        /// <summary>
        /// Process parameter for inclusion in the SQL statement
        /// </summary>
        /// <param name="param">Parameter to process</param>
        /// <param name="wrap">true to wrap the parameter with the quote</param>
        /// <returns>Processed parameter</returns>
        /// <remarks>Single quote present in the parameter will be escaped by replacing it with a two single quotes (to prevent SQL-injection attacks)</remarks>
        public static string ProcessParam(string param, bool wrap)
        {
            string result = param;
            if (string.IsNullOrEmpty(result))
            {
                result = wrap ? "''" : SqlConst.NullString;
            }
            else if (wrap)
            {
                result = result.Replace("'", "''"); //escape the "'" character
                result = "'" + result + "'"; //wrap it
            }
            return result;
        }
        /// <summary>
        /// Decorate parameter name
        /// </summary>
        /// <param name="name">Undecorated name</param>
        /// <returns>Decorated name</returns>
        static string DecorateParameterName(string name)
        {
            if (name[0] != SqlConst.NamedParameterPrefix)
            {
                name = SqlConst.NamedParameterPrefix + name;
            }
            return name;
        }
        #endregion

        #region Operations - Adding named parameters
        /// <summary>
        /// Add string (it will be wrapper with single quotes)
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="param">Parameter value</param>
        public void AddNamedParam(string name, string param)
        {
            AddNamedParam(name, param, true);
        }
        /// <summary>
        /// Add character
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="param">Parameter value</param>
        public void AddNamedParam(string name, char param)
        {
            AddNamedParam(name, param.ToString(), true);
        }
        /// <summary>
        /// Add number
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="param">Parameter value</param>
        public void AddNamedParam(string name, double param)
        {
            if (double.IsNaN(param))
            {
                param = 0;
            }
            AddNamedParam(name, FormatNormalizedDoubleValue(param), false);
        }
        /// <summary>
        /// Add date
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="param">Parameter value</param>
        /// <exception cref="InvalidOperationException">May perform date validation</exception>
        public void AddNamedParam(string name, DateTime param)
        {
            AddNamedParam(name, param, false);
        }
        /// <summary>
        /// Add date or date and time 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="param">Parameter value</param>
        /// <param name="addTime">true to add time part</param>
        /// <exception cref="InvalidOperationException">May perform date validation</exception>
        public void AddNamedParam(string name, DateTime param, bool addTime)
        {
            if (_validateDates)
            {
                string error = "";
                if (!ValidateDate(param, ref error))
                {
                    throw new ArgumentOutOfRangeException(string.Format("param ({0}): {1}", name, error));
                }
            }
            AddNamedParam(name, Tools.ConvertDateTimeToString(param, addTime));
        }
        /// <summary>
        /// Add string, optionally wrapped with single quotes
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="param">Parameter value</param>
        /// <param name="wrap">True to wrap parameter with single quotes</param>
        public void AddNamedParam(string name, string param, bool wrap)
        {
            name = DecorateParameterName(name);
            _namedParamCollection[name] = ProcessParam(param, wrap);
        }
        /// <summary>
        /// Add NULL
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="param">System.DBNull.value</param>
        public void AddNamedParam(string name, DBNull param)
        {
            AddNamedParam(name, SqlConst.NullString, false);
        }
        /// <summary>
        /// Add output string parameter (it will be wrapper with single quotes)
        /// </summary>
        /// <param name="name">Parameter name</param>
        public void AddOutputParam(string name)
        {
            AddOutputParam(name, SqlConst.NullString, false);
        }
        public void AddOutputParam(string name, object param, bool wrap)
        {
            name = DecorateParameterName(name);
            _outputParamCollection[name] = ProcessParam(param.ToString(), wrap);
        }
        #endregion

        #region Build SQL statement
        internal string BuildSql(string returnVarName)
        {
            return BuildSql(returnVarName, false);
        }
        string BuildSql(string returnVarName, bool onlyReturnCheck)
        {
            if (string.IsNullOrEmpty(_spName))
            {
                throw new InvalidOperationException("No stored procedure specified");
            }
            // Cleanup
            _sqlString = "";
            var stringBuilder = new StringBuilder();
            // Add any preparation sql
            foreach (string prepSql in _prepSqlCollection)
            {
                stringBuilder.AppendLine(prepSql);
            }
            // Add the previous return check
            if (onlyReturnCheck && returnVarName != null)
            {
                stringBuilder.Append("IF ");
                stringBuilder.Append(returnVarName);
                stringBuilder.Append(" = 0 ");
            }
            // Add execute statement
            stringBuilder.Append("EXEC ");
            // Assign the result
            if (onlyReturnCheck || returnVarName != null)
            {
                stringBuilder.Append(returnVarName);
                stringBuilder.Append(" = ");
            }
            // Add database name
            if (!string.IsNullOrEmpty(_databaseName))
            {
                stringBuilder.Append('[' + _databaseName + ']');
                stringBuilder.Append("..");
            }
            // Add stored procedure
            stringBuilder.Append('[' + _spName + ']');
            stringBuilder.Append(" ");
            // Add parameters
            AddParameters(stringBuilder);
            // Add any post-execution sql
            if (_postSqlCollection.Count > 0)
            {
                stringBuilder.AppendLine();
                foreach (string postSql in _postSqlCollection)
                {
                    stringBuilder.AppendLine(postSql);
                }
            }
            // Build the resulting string
            _sqlString = stringBuilder.ToString();
            return _sqlString;
        }
        /// <summary>
        /// Helper method to add various parameters
        /// </summary>
        /// <param name="stringBuilder"></param>
        void AddParameters(StringBuilder stringBuilder)
        {
            // This counter is used to check whether the comma is needed in front of the parameter
            int paramCount = 0;
            // Add unnamed parameters
            if (_paramCollection.Count > 0)
            {
                foreach (string param in _paramCollection)
                {
                    if (paramCount > 0)
                    {
                        stringBuilder.Append(", ");
                    }
                    stringBuilder.Append(param);
                    paramCount++;
                }
            }
            // Add named parameters
            if (_namedParamCollection.Count > 0)
            {
                foreach (string name in _namedParamCollection.Keys)
                {
                    if (paramCount > 0)
                    {
                        stringBuilder.Append(", ");
                    }
                    stringBuilder.Append(string.Format("{0}={1}", name, _namedParamCollection[name]));
                    paramCount++;
                }
            }
            // Add output parameters
            if (_outputParamCollection.Count > 0)
            {
                foreach (string name in _outputParamCollection.Keys)
                {
                    if (paramCount > 0)
                    {
                        stringBuilder.Append(", ");
                    }
                    stringBuilder.Append(string.Format("{0}={1} OUTPUT", name, _outputParamCollection[name]));
                    paramCount++;
                }
            }
        }
        #endregion

        #region Formatting
        public static string FormatDateConvert(string fieldName, bool addTime)
        {
            return string.Format("datediff({0},{1},", addTime ? "second" : "Day", fieldName);
        }
        /// <summary>
        /// Format normalized double value that can be used in SQL statement
        /// </summary>
        /// <param name="value">Value to normalize</param>
        /// <returns>Normalized value</returns>
        public static string FormatNormalizedDoubleValue(double value)
        {
            return Tools.ConvertDoubleToString(value, SqlConst.NormalizedPrecision, true, false);
        }

        #endregion

        #region Validation
        /// <summary>
        /// Validate given date to assure that it can be used in SQL statement
        /// </summary>
        /// <param name="date">Date to validate</param>
        /// <param name="error">Return error</param>
        /// <returns>true on success, false otherwise</returns>
        /// <remarks>Only the minimum value is checked since the maximum value is the same for DateTime class and SQL(December 31, 9999)</remarks>
        static bool ValidateDate(DateTime date, ref string error)
        {
            if (date < SqlConst.MinDateValue)
            {
                error = string.Format("{0} is less than {1} and can't be used used by database",
                Tools.ConvertDateTimeToString(date, true), SqlConst.MinDateValue);
                return false;
            }
            return true;
        }
        #endregion

        #region ISqlRequest Members
        public string BuildSql()
        {
            return BuildSql(null, false);
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
