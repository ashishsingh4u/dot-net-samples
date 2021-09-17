using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace PgmServer
{
    public partial class Server : Form
    {
        private Socket _server;

        private IPAddress _ipMc;
        private IPAddress _ipLocal;
        private IPEndPoint _iep1;
        private IPEndPoint _iep2;
        private EndPoint _ep;

        public Server()
        {
            InitializeComponent();
            ConfigureServer();
        }

        private void StartServerButtonClick(object sender, EventArgs e)
        {
            //send data
            byte[] data = Encoding.ASCII.GetBytes(txtMsg.Text);
            _server.Send(data);
        }

        private void ConfigureServer()
        {
            //creating server socket
            _server = new Socket(AddressFamily.InterNetwork, SocketType.Rdm, (ProtocolType) 113);

            //_server.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.ReuseAddress,true);
            //_server.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.NoDelay, true);

            _ipLocal = IPAddress.Any;
            _iep1 = new IPEndPoint(_ipLocal, 0);

            //bind socket to network interface IP address ANY
            _server.Bind(_iep1);

            _ipMc = IPAddress.Parse("224.0.0.23");
            Text = _ipMc.ToString();
            _iep2 = new IPEndPoint(_ipMc, 9999);

            _ep = (EndPoint) _iep2;

            //connect socket to multicast address group
            _server.Connect(_ep);
        }
    }
}
