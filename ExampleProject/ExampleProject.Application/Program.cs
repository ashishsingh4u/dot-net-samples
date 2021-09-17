using System;

namespace ExampleProject.Application
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var form = new Form1();

            System.Windows.Forms.Application.Run(form);
        }
    }
}
