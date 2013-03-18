using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using wScreenshot.Hooks;

namespace wScreenshot.Hooks
{
    /// <summary>
    /// Class allow to register hotkeys in system
    /// </summary>
    public sealed class HotKey : IDisposable
    {
        public event Action<HotKey> HotKeyPressed;

        private readonly int _id;
        private bool _isKeyRegistered;
        private readonly IntPtr _handle;

        private bool _disposed;

        public HotKey(ModifierKeys modifierKeys, Keys keys, Window window)
            : this(modifierKeys, keys, new WindowInteropHelper(window))
        {
            Contract.Requires(window != null);
        }

        public HotKey(ModifierKeys modifierKeys, Keys keys, WindowInteropHelper window)
            : this(modifierKeys, keys, window.Handle)
        {
            Contract.Requires(window != null);
        }

        public HotKey(ModifierKeys modifierKeys, Keys keys, IntPtr windowHandle)
        {
            Contract.Requires(modifierKeys != ModifierKeys.None || keys != Keys.None);
            Contract.Requires(windowHandle != IntPtr.Zero);

            Keys = keys;
            KeyModifier = modifierKeys;
            _id = GetHashCode();
            _handle = windowHandle;
            RegisterHotKey();
            ComponentDispatcher.ThreadPreprocessMessage += ThreadPreprocessMessageMethod;
        }

        ~HotKey()
        {
            Dispose(false);
        }

        public Keys Keys { get; private set; }

        public ModifierKeys KeyModifier { get; private set; }

        public void RegisterHotKey()
        {
            if (Keys == Keys.None)
            {
                return;
            }
            if (_isKeyRegistered)
            {
                UnregisterHotKey();
            }

            _isKeyRegistered = HotKeyWinApi.RegisterHotKey(_handle, _id, KeyModifier, Keys);

            if (!_isKeyRegistered)
            {
                throw new ApplicationException("Hotkey already in use");
            }
        }

        public void UnregisterHotKey()
        {
            _isKeyRegistered = !HotKeyWinApi.UnregisterHotKey(_handle, _id);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ComponentDispatcher.ThreadPreprocessMessage -= ThreadPreprocessMessageMethod;
                }

                UnregisterHotKey();
                _disposed = true;
            }
        }

        private void ThreadPreprocessMessageMethod(ref MSG msg, ref bool handled)
        {
            if (!handled)
            {
                if (msg.message == HotKeyWinApi.WmHotKey
                    && (int)(msg.wParam) == _id)
                {
                    OnHotKeyPressed();
                    handled = true;
                }
            }
        }

        private void OnHotKeyPressed()
        {
            if (HotKeyPressed != null)
            {
                HotKeyPressed(this);
            }
        }
    }
}