using System;

namespace DBHelper
{
    /// <summary>
    /// Sql constants
    /// </summary>
    public static class SqlConst
    {
        #region Construction
        static SqlConst()
        {
            MinDateValue = new DateTime(1753, 01, 01);
        }
        #endregion
        #region Constants
        /// <summary>
        /// Default error variable name
        /// </summary>
        public const string DefErrName = "@Err";
        /// <summary>
        /// Default return variable name
        /// </summary>
        public const string DefRetName = "@R";
        /// <summary>
        /// Normalized precision
        /// </summary>
        public const int NormalizedPrecision = 9;
        /// <summary>
        /// String used to represent NULL in SQL statements
        /// </summary>
        public const string NullString = "NULL";
        /// <summary>
        /// String used to decorate named parameter
        /// </summary>
        public const char NamedParameterPrefix = '@';
        /// <summary>
        /// Minimum value that can be converted and used in SQL statement
        /// </summary>
        public static DateTime MinDateValue { get; private set; }
        /// <summary>
        /// Character used to identify stored procedure calls originating from the client
        /// </summary>
        public const char StoredProcedureModeClient = 'C';
        /// <summary>
        /// Character used to identify stored procedure calls originating from the server
        /// </summary>
        public const char StoredProcedureModeServer = 'S';
        #endregion
    }

}
