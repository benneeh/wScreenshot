using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wScreenshot.Controls.AutoUpdate
{
    public class UpdateEventArgs : EventArgs
    {
        public UpdateEventArgs(string url, bool success, bool available, string updatemessage, string version)
        {
            _Url = url;
            _Success = success;
            _Available = available;
            _Updatemessage = updatemessage;
            _Version = version;
        }

        private string _Url, _Version, _Updatemessage;
        private bool _Success;
        private bool _Available;

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