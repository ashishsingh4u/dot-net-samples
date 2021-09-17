using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WindowEngine.ClientFramework;

namespace WindowEngine.Admin.GuiManager
{
    public class Main : IGuiManager
    {
        private IGuiFramework _guiFramework;
        private readonly List<ISubSystem> _subSystemList;

        public Main()
        {
            _subSystemList = new List<ISubSystem>();
        }

        public IGuiFramework GuiFramework
        {
            get { return _guiFramework; }
        }

        public IList<ISubSystem> SubSystems
        {
            get { return _subSystemList; }
        }

        public void Initialize(IGuiFramework guiFramework)
        {
            _guiFramework = guiFramework;
            var baseDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            foreach (var fileInfo in baseDirectory.GetFiles("WindowEngine.*.Controls.dll"))
            {
                var guiManagerModule = Assembly.LoadFrom(fileInfo.Name);
                foreach (var type in guiManagerModule.GetTypes())
                {
                    if (type.GetInterface(typeof(IGuiManager).Name) != null)
                    {
                        var subSystem = (ISubSystem)Activator.CreateInstance(type, false);
                        subSystem.Initialize(this);
                        _subSystemList.Add(subSystem);
                    }
                }
            }

        }

        public void UnInitialize()
        {
            
        }

        public void UpdateConrolKey(string oldKey, string newKey)
        {
            
        }
    }
}
