using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Client
{
    public partial class Subscriber : Form
    {
        public Subscriber()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o=>
                                             {  
                                                 Socket socket = new Socket(AddressFamily.InterNetwork,
                                                                            SocketType.Stream, ProtocolType.Tcp);
                                                 socket.Connect("127.0.0.1", 8085);
                                                 int count = 0;
                                                 byte[] bytes = new byte[Int16.MaxValue];
                                                 try
                                                 {
                                                     while ((count = socket.Receive(bytes)) > 0)
                                                     {

                                                     }
                                                 }
                                                 catch(SocketException exception)
                                                 {
                                                     
                                                 }
                                             });
        }
    }
}
