using System.ComponentModel;
using System.Configuration.Install;


namespace AutoWindowsService
{
    [RunInstaller(true)]
    public partial class AutoInstaller :Installer
    {
        public AutoInstaller()
        {
            InitializeComponent();
        }
    }
}
