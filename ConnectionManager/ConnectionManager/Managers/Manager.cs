using System;
using ConnectionManager.Contracts;

namespace ConnectionManager.Managers
{
    public class Manager : IConnectionManager
    {
        #region Implementation of IConnectionManager

        public void DisconnectAll()
        {
            throw new System.NotImplementedException();
        }

        public event EventHandler<ReceiveEventArgs<ConnectionStatus>> StatusChanged;

        #endregion
    }
}
