using System.ServiceProcess;
using log4net.Config;

namespace AutoWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var servicesToRun = new ServiceBase[] 
                                              { 
                                                  new AutoService() 
                                              };
            if (args != null && args.Length == 1 && args[0].Length > 1
                && (args[0][0] == '-' || args[0][0] == '/'))
            {
                switch (args[0].Substring(1).ToLower())
                {
                    case "i":
                    case "install":
                        ServiceHelper.InstallMe();
                        break;
                    case "u":
                    case "uninstall":
                        ServiceHelper.UninstallMe();
                        break;
                }
            }
            else
                ServiceHelper.Launch(servicesToRun);
        }
    }
}
