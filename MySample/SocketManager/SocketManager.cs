using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketManager
{
    public class SocketManager
    {
        private List<Socket> _clientSockets;
        private readonly object _locker = new object();
        private string _logPath;
        private Queue<string> _queueData;
        private AutoResetEvent _resetEvent;
        private Socket _socket;
        private Thread _threadDispatchMessage;
        private Thread _threadProcessClient;

        public void InitializeSocket(string bindingAddress, string logPath)
        {
            _clientSockets = new List<Socket>();
            _queueData = new Queue<string>();
            _resetEvent = new AutoResetEvent(false);
            _logPath = logPath;
            _threadProcessClient = new Thread(ProcessClient);
            _threadDispatchMessage = new Thread(ProcessMessage);
            _threadProcessClient.IsBackground = true;
            _threadDispatchMessage.IsBackground = true;
            _threadProcessClient.Start(bindingAddress);
            _threadDispatchMessage.Start();
        }

        private void ProcessClient(object state)
        {
            try
            {
                if (state == null)
                {
                    return;
                }
                string[] socketParams = state.ToString().Split(new[] { ':' });
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.Bind(new IPEndPoint(IPAddress.Parse(socketParams[0]), int.Parse(socketParams[1])));
                _socket.Listen(5);
                while (true)
                {
                    Socket clientSocket = _socket.EndAccept(_socket.BeginAccept(null, null));
                    lock (_locker)
                    {
                        if (clientSocket.Connected)
                        {
                            _clientSockets.Add(clientSocket);
                        }
                    }
                }
            }
            catch (ThreadAbortException exception)
            {
                if (!string.IsNullOrEmpty(_logPath))
                {
                    File.WriteAllText(_logPath, exception.StackTrace);
                }
            }
            catch (SocketException exception)
            {
                if (!string.IsNullOrEmpty(_logPath))
                {
                    File.WriteAllText(_logPath, exception.StackTrace);
                }
            }
            finally
            {
                if (_socket != null)
                {
                    _socket.Close();
                }
            }
        }

        private void ProcessMessage()
        {
            try
            {
                var removeSocket = new List<Socket>();
                while (true)
                {
                    _resetEvent.WaitOne();
                    lock (_locker)
                    {
                        while (_queueData.Count > 0)
                        {
                            string data = _queueData.Dequeue();
                            foreach (Socket socket in _clientSockets)
                            {
                                try
                                {
                                    socket.Send(Encoding.ASCII.GetBytes(data));
                                    continue;
                                }
                                catch (SocketException)
                                {
                                    removeSocket.Add(socket);
                                    continue;
                                }
                            }
                            removeSocket.ForEach(delegate(Socket socket)
                            {
                                socket.Shutdown(SocketShutdown.Both);
                                socket.Close();
                                _clientSockets.Remove(socket);
                            });
                            removeSocket.Clear();
                        }
                    }
                }
            }
            catch (ThreadAbortException exception)
            {
                foreach (Socket socket in _clientSockets)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                if (!string.IsNullOrEmpty(_logPath))
                {
                    File.WriteAllText(_logPath, exception.StackTrace);
                }
            }
        }

        public void Send(string data)
        {
            lock (_locker)
            {
                _queueData.Enqueue(data);
                _resetEvent.Set();
            }
        }

        public void UnInitializeSocket()
        {
            _threadDispatchMessage.Abort();
            _threadDispatchMessage.Join();
            _threadProcessClient.Abort();
            _threadProcessClient.Join();
        }
    }
}
