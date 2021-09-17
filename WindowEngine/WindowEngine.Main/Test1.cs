using System;
using System.Windows.Forms;
using WindowEngine.ClientFramework;

namespace WindowEngine.Main.Controls
{
    public partial class Test1 : UserControl,IWindow
    {
        public Test1()
        {
            InitializeComponent();
        }

        #region Implementation of IWindow

        public string ItemName
        {
            get { return "Test 1"; }
            set { throw new NotImplementedException(); }
        }

        public string ItemGroup
        {
            get { return "Main"; }
            set { throw new NotImplementedException(); }
        }

        public string ItemKey
        {
            get { return "Test1"; }
            set { throw new NotImplementedException(); }
        }

        #endregion
    }
}
