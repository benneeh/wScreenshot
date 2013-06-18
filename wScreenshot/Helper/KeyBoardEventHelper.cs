﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace wScreenshot.Helper
{
    public class KeyBoardEventHelper : INotifyPropertyChanged
    {
        private readonly Dictionary<string, bool> KeyDictionary = new Dictionary<string, bool>();
        private Binding b = new Binding();

        public KeyBoardEventHelper()
        {
            Keyboard.AddKeyDownHandler(Application.Current.MainWindow, (s, e) =>
            {
                string key = e.Key.ToString();
                string subKey = key.Replace("Left", "").Replace("Right", "");
                if (!string.IsNullOrEmpty(subKey))
                {
                    if (!KeyDictionary.ContainsKey(subKey))
                    {
                        KeyDictionary.Add(subKey, true);
                    }
                    else
                    {
                        KeyDictionary[subKey] = true;
                    }
                    OnPropertyChanged(string.Format("[{0}]", subKey));
                }

                if (!KeyDictionary.ContainsKey(key))
                {
                    KeyDictionary.Add(key, true);
                }
                else
                {
                    KeyDictionary[key] = true;
                }
                OnPropertyChanged(string.Format("[{0}]", e.Key));
            });
            Keyboard.AddKeyUpHandler(Application.Current.MainWindow, (s, e) =>
            {
                string key = e.Key.ToString();
                string subKey = key.Replace("Left", "").Replace("Right", "");
                if (!string.IsNullOrEmpty(subKey))
                {
                    if (!KeyDictionary.ContainsKey(subKey))
                    {
                        KeyDictionary.Add(subKey, false);
                    }
                    else
                    {
                        KeyDictionary[subKey] = false;
                    }
                    OnPropertyChanged(string.Format("[{0}]", subKey));
                }

                if (!KeyDictionary.ContainsKey(key))
                {
                    KeyDictionary.Add(key, false);
                }
                else
                {
                    KeyDictionary[key] = false;
                }
                OnPropertyChanged(string.Format("[{0}]", e.Key));
            });
        }

        public bool this[string key]
        {
            get
            {
                if (KeyDictionary.ContainsKey(key))
                {
                    return KeyDictionary[key];
                }
                Key k;
                if (Enum.TryParse(key, true, out k))
                {
                    return Keyboard.IsKeyDown(k);
                }
                return false;
            }
        }

        public bool IsCtrlDown { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}