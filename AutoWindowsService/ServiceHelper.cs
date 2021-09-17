using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace AutoWindowsService
{
    public class ServiceHelper
    {
        private static readonly string ExePath =
           Assembly.GetExecutingAssembly().Location;
        public static bool InstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new[] { ExePath });
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool UninstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new[] { "/u", ExePath });
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool Launch(ServiceBase[] services)
        {
            try
            {
                if (Environment.UserInteractive)
                {
                    Type type = typeof(ServiceBase);
                    const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
                    var method = type.GetMethod("OnStart", flags);

                    foreach (var service in services)
                    {
                        method.Invoke(service, new object[] {null});
                    }

                    Console.WriteLine("Press any key to exit");
                    Console.Read();


                    foreach (var service in services)
                    {
                        service.Stop();
                    }
                }
                else
                {
                    ServiceBase.Run(services);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
