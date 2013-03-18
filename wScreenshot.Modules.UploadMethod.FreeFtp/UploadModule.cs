using wScreenshot.Modules.Base.Interface;
using wScreenshot.Modules.Base.Interface.View;

namespace wScreenshot.Modules.UploadMethod.FreeFtp
{
    public class UploadModule
    {
        public bool IsFileBrowserEnabled
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool IsFileDeletable
        {
            get { throw new System.NotImplementedException(); }
        }

        public ConfigurationView ConfigurationView
        {
            get
            {
                return null;
            }
        }

        public ConnectionSettingsView ConnectionSettingsView
        {
            get
            {
                return null;
            }
        }
    }
}