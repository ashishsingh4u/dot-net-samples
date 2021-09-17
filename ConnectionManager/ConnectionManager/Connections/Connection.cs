using System;
using ConnectionManager.Contracts;

namespace ConnectionManager.Connections
{
    public class Connection : IConnection
    {
        #region Implementation of IConnection

        public string Address
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ConnectionName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void Connect(string address)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Send(byte[] data)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<ReceiveEventArgs<byte[]>> OnReceived;
        public event EventHandler<ReceiveEventArgs<ConnectionStatus>> StatusChanged;

        #endregion
    }
}
