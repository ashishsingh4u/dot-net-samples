using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WindowEngine.ClientFramework;

namespace WindowEngine.GuiSystem
{
    public partial class MainForm : Form, IGuiFramework
    {
        #region Class Variables

        private readonly List<IGuiManager> _guiManagerList;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            _guiManagerList = new List<IGuiManager>();
            InitializeGuiMangers();
        }

        public IList<IGuiManager> GuiManagers
        {
            get { return _guiManagerList; }
        }

        public void LaunchWindow(string windowKey)
        {
        }

        public void ShowModalPopup()
        {
        }

        public void KillProcess()
        {
        }

        public void UpdateControlKey(string oldKey, string newKey)
        {
        }

        public string AppTitle
        {
            get { return null; }
            set
            {
                
            }
        }

        public void InitializeGuiMangers()
        {
            var baseDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            foreach (var fileInfo in baseDirectory.GetFiles("WindowEngine.*.GuiManager.dll"))
            {
                var guiManagerModule = Assembly.LoadFrom(fileInfo.Name);
                foreach (var type in guiManagerModule.GetTypes())
                {
                    if (type.GetInterface(typeof(IGuiManager).Name) != null)
                    {
                        var guiManager = (IGuiManager) Activator.CreateInstance(type, false);
                        guiManager.Initialize(this);
                        _guiManagerList.Add(guiManager);
                    }
                }
            }
        }

        //public void LoadMenuItems()
        //{
        //    var tempDomain = AppDomain.CreateDomain("temp", AppDomain.CurrentDomain.Evidence,
        //                                            AppDomain.CurrentDomain.SetupInformation);
        //    var utils = (Utils)tempDomain.CreateInstanceAndUnwrap("WindowEngine.ClientFramework", "WindowEngine.ClientFramework.Utils");
        //    var windowItemList = utils.GetControlList();
        //    AppDomain.Unload(tempDomain);
        //}

        public bool ContainsControl(string windowKey)
        {
            return false;
        }
    }
}
