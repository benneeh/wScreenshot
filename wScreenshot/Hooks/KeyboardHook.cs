using System;
using System.ComponentModel;
using System.Windows.Input;
using wScreenshot.Native;

namespace wScreenshot.Hooks
{
    public class KeyboardHook
    {
        #region Declarations

        private Win32.keyboardHookProc HookProcHandler;
        private bool m_bHook;

        /// <summary>
        ///     Handle to the hook, need this to unhook and call the next hook
        /// </summary>
        private IntPtr m_hHook = IntPtr.Zero;

        private IntPtr m_hInstance = IntPtr.Zero;

        #endregion Declarations

        #region Properties

        /// <summary>
        /// </summary>
        [Category("Hook"), DefaultValue(false), Description("Start or stop the hook.")]
        public bool IsHooked
        {
            get { return m_bHook; }
            set
            {
                if (value) InitHook();
                else UnHook();

                m_bHook = value;
            }
        }

        #endregion Properties

        #region Events

        public delegate void KeyboardHookEventHandler(object sender, KeyboardHookEventArgs e);

        [Category("Hook"), Description("Sets an global Keyboard down event.")]
        public event KeyboardHookEventHandler KeyDown;

        [Category("Hook"), Description("Sets an global Keyboard up event.")]
        public event KeyboardHookEventHandler KeyUp;

        #endregion Events

        #region Constructors and Destructors

        /// <summary>
        ///     Release unmanaged code
        /// </summary>
        ~KeyboardHook()
        {
            UnHook();
        }

        #endregion Constructors and Destructors

        #region Methods

        /// <summary>
        ///     The callback for the Keyboard hook
        /// </summary>
        /// <param name="code"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int HookProc(int code, int wParam, ref Win32.KBDLLHOOKSTRUCT lParam)
        {
            if (code >= 0)
            {
                Key key = System.Windows.Input.KeyInterop.KeyFromVirtualKey(lParam.vkCode);

                //if (HookedKeys.Contains(key))
                {
                    KeyboardHookEventArgs kea = new KeyboardHookEventArgs(key, true);
                    if ((wParam == (int)Win32.WindowsMessages.WM_KEYDOWN || wParam == (int)Win32.WindowsMessages.WM_SYSKEYDOWN) && (KeyDown != null))
                    {
                        KeyDown(this, kea);
                    }
                    else if ((wParam == (int)Win32.WindowsMessages.WM_KEYUP || wParam == (int)Win32.WindowsMessages.WM_SYSKEYUP) && (KeyUp != null))
                    {
                        kea.IsDown = false;
                        KeyUp(this, kea);
                    }
                    if (kea.Handled)
                        return 1;
                }
            }
            return Win32.CallNextHookEx(m_hHook, code, wParam, ref lParam);
        }

        /// <summary>
        ///     Installs the global hook 'Keyboard'
        /// </summary>
        private void InitHook()
        {
            HookProcHandler = HookProc;
            m_hInstance = Win32.LoadLibrary("User32");
            m_hHook = Win32.SetWindowsHookEx((int)Win32.WindowsHook.WH_KEYBOARD_LL, HookProcHandler, m_hInstance, 0);
        }

        /// <summary>
        ///     Uninstalls the global hook
        /// </summary>
        private void UnHook()
        {
            HookProcHandler = null;
            Win32.FreeLibrary(m_hInstance);
            Win32.UnhookWindowsHookEx(m_hHook);
        }

        #endregion Methods
    }

    public class KeyboardHookEventArgs
    {
        public Key Key;
        public bool IsDown;
        public bool Handled = false;

        public KeyboardHookEventArgs(Key key, bool isDown)
        {
            this.Key = key;
            this.IsDown = isDown;
        }
    }
}