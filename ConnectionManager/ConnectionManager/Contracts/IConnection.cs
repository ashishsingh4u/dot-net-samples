using System;

namespace ConnectionManager.Contracts
{
    public enum ConnectionStatus
    {
        Connected,
        Disconnected,
        Error
    }

    public interface IConnection
    {
        string Address { get; set; }
        string ConnectionName { get; set; }
        void Connect(string address);
        void Disconnect();
        void Send(byte[] data);
        event EventHandler<ReceiveEventArgs<byte[]>> OnReceived;
        event EventHandler<ReceiveEventArgs<ConnectionStatus>> StatusChanged;
    }

    public class ReceiveEventArgs<T> : EventArgs
    {
        public string ConnectionName { get; private set; }

        public ReceiveEventArgs(T data, string connectionname)
        {
            Data = data;
            ConnectionName = connectionname;
        }

        public T Data { private set; get; }
    }
}
