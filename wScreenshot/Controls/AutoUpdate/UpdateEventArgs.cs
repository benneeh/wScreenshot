using System;

namespace wScreenshot.Controls.AutoUpdate
{
    public class UpdateEventArgs : EventArgs
    {
        private readonly bool _Available;
        private readonly bool _Success;
        private readonly string _Updatemessage;
        private readonly string _Url;
        private readonly string _Version;

        public UpdateEventArgs(string url, bool success, bool available, string updatemessage, string version)
        {
            _Url = url;
            _Success = success;
            _Available = available;
            _Updatemessage = updatemessage;
            _Version = version;
        }

        public string Updatemessage
        {
            get { return _Updatemessage; }
        }

        public bool Available
        {
            get { return _Available; }
        }

        public string Url
        {
            get { return _Url; }
        }

        public string Version
        {
            get { return _Version; }
        }

        public bool Success
        {
            get { return _Success; }
        }
    }
}