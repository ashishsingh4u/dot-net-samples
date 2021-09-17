using WindowEngine.ClientFramework;

namespace WindowEngine.Main.Controls
{
    public class Main : ISubSystem
    {
        #region Implementation of ISubSystem

        private IGuiManager _guiManager;

        public string Id { get; set; }

        public string DisplayName { get; set; }

        public IGuiManager GuiManager
        {
            get { return _guiManager; }
        }

        public void Initialize(IGuiManager guiManager)
        {
            _guiManager = guiManager;
        }

        public void UnInitialize()
        {
            
        }

        public void LaunchWindowByKey(string key)
        {
            
        }

        public void CloseWindowByKey(string key)
        {
            
        }

        #endregion
    }
}
