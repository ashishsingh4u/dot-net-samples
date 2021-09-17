using System;
using System.Data;
using System.Diagnostics;

namespace DBHelper
{
    public class AutoStopwatch : IDisposable
    {
        readonly Stopwatch _stopwatch;
        readonly Action<TimeSpan> _action;
        public AutoStopwatch(Action<TimeSpan> action)
        {
            _action = action;
            _stopwatch = Stopwatch.StartNew();
        }
        public void Dispose()
        {
            _stopwatch.Stop();
            _action(_stopwatch.Elapsed);
        }
    }

    public interface ISqlRequest
    {
        string BuildSql();
    }

    public enum DatabaseType
    {
        Odbc,
        MsNativeClient,
    }

    public interface IDataAccess
    {
        DataSet ReadData(IDataAccessProperties dataAccessProperties, ISqlRequest sqlRequest);
        void ExecuteSql(IDataAccessProperties dataAccessProperties, ISqlRequest sqlRequest);
    }

    public interface IDataAccessProperties
    {
        string Application { get; set; }
        string Database { get; set; }
        string User { get; set; }
        string Password { get; set; }
        int Site { get; set; }
        string ConnectionString { get; set; }
        string SpPrefix { get; set; }
        DatabaseType DatabaseType { get; set; }
    }

    public interface IDataAccessRequirements : IDataAccessProperties
    {
        IDataAccess DataAccess { get; set; }
    }

    public class DataAccessInitializeProperties
    {
        public const string AccessMode = "CLIENT_SERVER";
    }

    public class DataAccessRequirements:IDataAccessRequirements
    {
        public DataAccessRequirements()
        {
            DataAccess = new DataAccess(this);
        }
        #region Implementation of IDataAccessProperties

        public string Application { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int Site { get; set; }
        public string ConnectionString { get; set; }
        public string SpPrefix { get; set; }
        public DatabaseType DatabaseType { get; set; }

        #endregion

        #region Implementation of IDataAccessRequirements

        public IDataAccess DataAccess { get; set; }

        #endregion
    }

    public class DataAccess:IDataAccess
    {
        private readonly DbAccess _dbAccess;
        public DataAccess(IDataAccessProperties dataAccessProperties)
        {
            _dbAccess = new DbAccess(dataAccessProperties);
        }
        #region Implementation of IDataAccess

        public DataSet ReadData(IDataAccessProperties dataAccessProperties, ISqlRequest sqlRequest)
        {
            DataSet dataSet = null;
            string error = null;
            _dbAccess.GetRecords(sqlRequest, ref dataSet, ref error);
            return dataSet;
        }

        public void ExecuteSql(IDataAccessProperties dataAccessProperties, ISqlRequest sqlRequest)
        {
            string error = null;
            _dbAccess.ExecuteSql(sqlRequest, ref error);
        }

        #endregion
    }
}
