using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WindowEngine.ClientFramework
{
    public interface IGuiFramework
    {
        IList<IGuiManager> GuiManagers { get; }
        void LaunchWindow(string windowKey);
        void ShowModalPopup();
        void KillProcess();
        void UpdateControlKey(string oldKey, string newKey);
        string AppTitle { set; get; }
        void InitializeGuiMangers();
        bool ContainsControl(string windowKey);
    }
    public interface IGuiManager
    {
        IGuiFramework GuiFramework { get; }
        IList<ISubSystem> SubSystems { get; }
        void Initialize(IGuiFramework guiFramework);
        void UnInitialize();
        void UpdateConrolKey(string oldKey, string newKey);
    }
    public interface ISubSystem
    {
        string Id { set; get; }
        string DisplayName { set; get; }
        IGuiManager GuiManager { get; }
        void Initialize(IGuiManager guiManager);
        void UnInitialize();
        void LaunchWindowByKey(string key);
        void CloseWindowByKey(string key);
    }
    public interface IWindow
    {
        bool Visible { get; set; }
        string ItemName { get; set; }
        string ItemGroup { get; set; }
        string ItemKey { set; get; }
    }
    [Serializable]
    public class Utils
    {
        public IList<WindowItem> GetControlList()
        {
            var itemList = new List<WindowItem>();
            var baseDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            foreach (var fileInfo in baseDirectory.GetFiles("*.dll"))
            {
                var guiManagerModule = Assembly.LoadFrom(fileInfo.Name);
                itemList.AddRange(from type in guiManagerModule.GetTypes()
                                  where type.GetInterface(typeof (IWindow).Name) != null
                                  select (IWindow) Activator.CreateInstance(type, false)
                                  into window select new WindowItem(window.ItemKey, window.ItemName, window.ItemGroup, window.Visible));
            }

            return itemList;
        }
    }
    public class WindowItem
    {
        public WindowItem(string key, string name, string group, bool visible)
        {
            Key = key;
            Group = group;
            Name = name;
            Visible = visible;
        }

        public string Key { get; private set; }
        public string Group { get; private set; }
        public string Name { get; private set; }
        public bool Visible { get; private set; }
    }
}
