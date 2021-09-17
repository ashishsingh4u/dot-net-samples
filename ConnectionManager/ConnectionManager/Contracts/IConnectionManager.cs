using System;

namespace ConnectionManager.Contracts
{
    public interface IConnectionManager
    {
        void DisconnectAll();
        event EventHandler<ReceiveEventArgs<ConnectionStatus>> StatusChanged;
    }
}
