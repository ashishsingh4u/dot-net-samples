using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Simple.MVC.Controller;

namespace Simple.MVC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            TopEmployeeController controller = new TopEmployeeController();
            Application.Run(controller.View as Form);
        }
    }
}
