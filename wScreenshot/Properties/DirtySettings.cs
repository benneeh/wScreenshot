using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace wScreenshot.Properties
{
    public static class DirtySettingsExtender
    {
        private class DirtySet
        {
            public bool IsDirty;
            public bool IsReset;
        }

        private static Dictionary<object, DirtySet> IsDirtyDictionary = new Dictionary<object, DirtySet>();

        public static void InitializeDirtifying(this ApplicationSettingsBase Config)
        {
            IsDirtyDictionary.Add(Config, new DirtySet());
            Config.SettingsSaving += new SettingsSavingEventHandler(Config_SettingsSaving);
            Config.SettingsLoaded += new SettingsLoadedEventHandler(Config_SettingsLoaded);
            Config.SettingChanging += new SettingChangingEventHandler(Config_SettingChanging);
        }

        public static void DoReset(this ApplicationSettingsBase Config)
        {
            Config.Reset();
            if (Config != null && IsDirtyDictionary.ContainsKey(Config))
            {
                IsDirtyDictionary[Config].IsReset = true;
            }
            else IsDirtyDictionary.Add(Config, new DirtySet()
            {
                IsDirty = false,
                IsReset = true
            });
        }

        public static bool IsDirty(object Config)
        {
            if (Config != null && IsDirtyDictionary.ContainsKey(Config))
            {
                return IsDirtyDictionary[Config].IsDirty;
            }
            else return false;
        }

        public static bool IsReset(object Config)
        {
            if (Config != null && IsDirtyDictionary.ContainsKey(Config))
            {
                return IsDirtyDictionary[Config].IsReset;
            }
            else return false;
        }

        private static void Config_SettingChanging(object sender, SettingChangingEventArgs e)
        {
            IsDirtyDictionary[sender].IsReset = false;
            IsDirtyDictionary[sender].IsDirty = true;
        }

        private static void Config_SettingsLoaded(object sender, SettingsLoadedEventArgs e)
        {
            IsDirtyDictionary[sender].IsReset = false;
            IsDirtyDictionary[sender].IsDirty = false;
        }

        private static void Config_SettingsSaving(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsDirtyDictionary[sender].IsReset = false;
            IsDirtyDictionary[sender].IsDirty = false;
        }
    }
}