using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;

public class SqlPush
{
    private static readonly SocketManager.SocketManager _manager = new SocketManager.SocketManager();

    [SqlFunction]
    public static SqlString Intialize(SqlString bindingAddress, SqlString logPath)
    {
        try
        {
            _manager.InitializeSocket(bindingAddress.ToString(), logPath.ToString());
            return new SqlString("Socket Manager Intialized.");
        }
        catch (Exception exception)
        {
            return new SqlString(exception.Message);
        }
    }

    [SqlProcedure]
    public static void SendData(SqlString data)
    {
        _manager.Send(data.ToString());
    }

    [SqlFunction]
    public static SqlString UnIntialize()
    {
        try
        {
            _manager.UnInitializeSocket();
            return new SqlString("Socket Manager UnIntialized.");
        }
        catch (Exception exception)
        {
            return new SqlString(exception.StackTrace);
        }
    }
}

