using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace wScreenshot.Configuration
{
    [Serializable]
    public class wScreenshot : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region constructors

        public wScreenshot()
        {
        }

        #endregion constructors

        #region private fields

        private bool _IsDirty;

        #endregion private fields

        #region events

        /// <summary>
        /// Occurs after the value of an application settings property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs before the value of an application settings property is changed.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Occurs before the value of an application settings property is changed.
        /// </summary>
        public event SettingChangingEventHandler SettingChanging;

        /// <summary>
        /// Occurs after the application settings are retrieved from storage.
        /// </summary>
        public event SettingsLoadedEventHandler SettingsLoaded;

        /// <summary>
        /// Occurs before values are saved to the data store.
        /// </summary>
        public event SettingsSavingEventHandler SettingsSaving;

        #endregion events

        #region public properties

        public bool IsDirty
        {
            get
            {
                return _IsDirty;
            }
            set
            {
                if (_IsDirty != value)
                {
                    SendPropertyChanging("IsDirty");
                    _IsDirty = value;
                    SendPropertyChanged("IsDirty");
                }
            }
        }

        public string ConfigName
        {
            get
            {
                return "wScreenshot";
            }
        }

        public string ConfigVersion
        {
            get
            {
                return "v0.0.0.1";
            }
        }

        public Environment.SpecialFolder Location
        {
            get
            {
                return Environment.SpecialFolder.ApplicationData;
            }
        }

        public string FullPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Location), Assembly.GetExecutingAssembly().GetName().Name, ConfigVersion, string.Format(@"{0}.config", ConfigName));
            }
        }

        #endregion public properties

        #region public methods

        public void Save()
        {
            var fi = new FileInfo(FullPath);
            using (var fs = fi.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite | FileShare.Delete))
            {
                var xs = new XmlSerializer(typeof(wScreenshot));
                xs.Serialize(fs, this);
            }
        }

        #endregion public methods

        #region private methods

        private void SendPropertyChanging(string PropertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(PropertyName));
            }
        }

        private void SendPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        #endregion private methods
    }
}