using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace TestSecuWebService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SecuWebService.Service service = new SecuWebService.Service();
            var certificate = X509Certificate.CreateFromCertFile(@"c:\projects\certificates\testclient.cer");
            service.ClientCertificates.Add(certificate);
            service.Url = @"https://localhost:443/SecuWebService/Service.asmx";
            string message = service.HelloWorld();
        }
    }
}
