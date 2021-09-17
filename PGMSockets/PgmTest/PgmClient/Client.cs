using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PgmClient
{
    public partial class Client : Form
    {
        private Socket client;
        private Socket clientRead;

        private IPAddress ipMC;
        private IPEndPoint iepMC;
        private EndPoint ep;
        private Thread receiverThread;

        public Client()
        {
            InitializeComponent();
        }

        private void ReceiveButtonClick(object sender, EventArgs e)
        {
            ConfigureClient();
        }

        private void ConfigureClient()
        {
            //create the socket
            client = new Socket(AddressFamily.InterNetwork, SocketType.Rdm, (ProtocolType) 113);

            ipMC = IPAddress.Parse("224.0.0.23");
            iepMC = new IPEndPoint(ipMC,9999);
            ep = (EndPoint) iepMC;

            //bind socket to multicast group
            client.Bind(ep);
            client.Listen(10);

            //start multithreading to be able to receive packets
            receiverThread = new Thread(new ThreadStart(packetReceive));
            receiverThread.IsBackground = true;
            receiverThread.Start();

            Text = "Ready to receive messages!";
            _receiveButton.Enabled = false;
        }

        private void packetReceive()
        {
            try
            {
                //enable socket for accepting connections
                clientRead = client.Accept();
                byte[] data = new byte[1024];
                string stringData;
                int recv;
                while(true)
                {
                    recv = clientRead.Receive(data);
                    stringData = Encoding.ASCII.GetString(data, 0, recv);
                    MessageBox.Show(stringData);
                }
            }
            catch(Exception exception)
            {
                
            }
        }
    }
}
