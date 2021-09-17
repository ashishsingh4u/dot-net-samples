using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using WebServiceDemo.MyService;

namespace WebServiceDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //System.Net.ServicePointManager.ServerCertificateValidationCallback =
            //   ((sender, certificate, chain, sslPolicyErrors) => true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyService.Service service = new Service();
            service.ClientCertificates.Add(
                new X509Certificate(@"C:\testclient.cer"));
            string value = service.HelloWorld();
            _browser.DocumentText =
        "<html><body>Please enter your name:<br/>" +
        "<input type='text' name='userName'/><br/>" +
        "<a href='http://www.microsoft.com'>continue</a>" +
        "</body></html>";

            _browser.Navigating += (newSender, args) =>
                                       {
                                           var document =
                                               this._browser.Document;

                                           if (document != null && document.All["userName"] != null &&
                                               String.IsNullOrEmpty(
                                                   document.All["userName"].GetAttribute("value")))
                                           {
                                               args.Cancel = true;
                                               MessageBox.Show(
                                                   "You must enter your name before you can navigate to " +
                                                   args.Url.ToString());
                                           }
                                       };
        }
    }
}
