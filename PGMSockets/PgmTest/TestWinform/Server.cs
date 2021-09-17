using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TestWinform
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var bytes = Convert.FromBase64String(File.ReadAllText(@"abc.txt"));
            //File.WriteAllBytes("baretailpro.exe",bytes);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Rdm, (ProtocolType) 113);
            socket.Bind(new IPEndPoint(IPAddress.Parse("224.0.0.4"), 4024));
            socket.Listen(50);
            socket.Accept();
        }
    }
}
