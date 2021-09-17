#define b
#undef b
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using log4net.Config;

namespace Log4NetSample
{
    public partial class ExceptionTest : Form
    {
        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ExceptionTest()
        {
            InitializeComponent();
#if (b)
{
    Stream configuration = Assembly.GetExecutingAssembly().GetManifestResourceStream("Log4NetSample.Log4Net.config");
                XmlConfigurator.Configure(configuration);
                configuration.Close();
}
#else
{
                XmlConfigurator.Configure();
}

#endif
        }

        private void ButtonClick(object sender, System.EventArgs e)
        {
            try
            {
                Regex regex = new Regex(@"(?<=\/)(.*?)(?=\/");
                var match = regex.Matches(@"http://lnapln005.ldn.emea.cib/fxtp-external/FXTP.application?FXTP?FXTP");
                if (match[2].Success)
                {
                    var value = match[2].Value;
                }
            }
            catch (Exception exception)
            {
                Log.Debug("MyException", exception);
                Log.Error("MyException", exception);
                Log.Warn("MyException", exception);
                Log.Info("MyException", exception);
                Log.Fatal("MyException", exception);
            }
        }
    }
}
