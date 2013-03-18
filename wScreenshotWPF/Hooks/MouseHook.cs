using System;
using System.ComponentModel;
using System.Windows.Input;
using wScreenshot.Hooks;
using wScreenshot.Native;

namespace wScreenshot.Hooks
{
    public class MouseHook 
    {
        #region Declarations

        private Win32.mouseHookProc HookProcHandler;
        private bool m_bHook = false;

        /// <summary>
        /// Handle to the hook, need this to unhook and call the next hook
        /// </summary>
        private IntPtr m_hHook = IntPtr.Zero;

        private IntPtr m_hInstance = IntPtr.Zero;

        #endregion Declarations

        #region Properties

        /// <summary>
        ///
        /// </summary>
        [Category("Hook"), DefaultValue(false), Description("Start or stop the hook.")]
        public bool IsHooked
        {
            get { return this.m_bHook; }
            set
            {
                if (value) this.InitHook();
                else this.UnHook();

                this.m_bHook = value;
            }
        }

        #endregion Properties

        #region Events

        public delegate void MouseHookEventHandler(object sender, MouseHookEventArgs e);

        [Category("Hook"), Description("Sets an global mouse down event.")]
        public event MouseHookEventHandler MouseDown;

        /// <summary>
        /// Occurs when one of the hooked keys is pressed
        /// </summary>
        [Category("Hook"), Description("Sets an global mouse move event.")]
        public event MouseHookEventHandler MouseMove;

        [Category("Hook"), Description("Sets an global mouse up event.")]
        public event MouseHookEventHandler MouseUp;

        [Category("Hook"), Description("Sets an global mouse wheel event.")]
        public event MouseHookEventHandler MouseWheel;

        #endregion Events

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public MouseHook()
        {

        }

        /// <summary>
        /// Release unmanaged code
        /// </summary>
        ~MouseHook()
        {
            this.UnHook();
        }

        #endregion Constructors and Destructors

        #region Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="mouseMessages"></param>
        /// <returns></returns>
        private MouseButton GetMouseButton(wScreenshot.Native.Win32.MouseMessages mouseMessages)
        {
            switch (mouseMessages)
            {
                case wScreenshot.Native.Win32.MouseMessages.WM_LBUTTONUP:
                case wScreenshot.Native.Win32.MouseMessages.WM_LBUTTONDOWN:
                    return MouseButton.Left;

                case wScreenshot.Native.Win32.MouseMessages.WM_MBUTTONUP:
                case wScreenshot.Native.Win32.MouseMessages.WM_MBUTTONDOWN:
                    return MouseButton.Middle;

                case wScreenshot.Native.Win32.MouseMessages.WM_RBUTTONUP:
                case wScreenshot.Native.Win32.MouseMessages.WM_RBUTTONDOWN:
                    return MouseButton.Right;
            }
            return MouseButton.Left;
        }

        /// <summary>
        /// The callback for the mouse hook
        /// </summary>
        /// <param name="code"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int HookProc(int code, int wParam, ref wScreenshot.Native.Win32.MSLLHOOKSTRUCT lParam)
        {
            if (code >= 0)
            {
                MouseHookEventArgs eventArgs=null;
                if (this.MouseDown != null
                    && (wParam == (int)wScreenshot.Native.Win32.MouseMessages.WM_LBUTTONDOWN
                    || wParam == (int)wScreenshot.Native.Win32.MouseMessages.WM_MBUTTONDOWN
                    || wParam == (int)wScreenshot.Native.Win32.MouseMessages.WM_RBUTTONDOWN))
                {
                    MouseButton eMouseButtons = this.GetMouseButton((wScreenshot.Native.Win32.MouseMessages)wParam);
                    eventArgs = new MouseHookEventArgs(eMouseButtons, 0, lParam.pt.X, lParam.pt.Y, 0,false);
                    //this.MouseDown.Invoke(this, new MouseEventArgs(eMouseButtons, 0, lParam.pt.X, lParam.pt.Y, 0));
                    MouseDown(this, eventArgs);
                }
                else if (this.MouseUp != null
                    && (wParam == (int)wScreenshot.Native.Win32.MouseMessages.WM_LBUTTONUP
                    || wParam == (int)wScreenshot.Native.Win32.MouseMessages.WM_MBUTTONUP
                    || wParam == (int)wScreenshot.Native.Win32.MouseMessages.WM_RBUTTONUP))
                {
                    MouseButton eMouseButtons = this.GetMouseButton((wScreenshot.Native.Win32.MouseMessages)wParam);
                    eventArgs = new MouseHookEventArgs(eMouseButtons, 0, lParam.pt.X, lParam.pt.Y, 0, false);
                    //this.MouseUp.Invoke(this, new MouseEventArgs(eMouseButtons, 0, lParam.pt.X, lParam.pt.Y, 0));
                    MouseUp(this, eventArgs);
                }
                else if (this.MouseWheel != null
                    && (wParam == (int)wScreenshot.Native.Win32.MouseMessages.WM_MOUSEWHEEL || wParam == (int)wScreenshot.Native.Win32.MouseMessages.WM_MOUSEHWHEEL))
                {
                    eventArgs = new MouseHookEventArgs(MouseButton.Middle, 0, lParam.pt.X, lParam.pt.Y, 0, false);
                    this.MouseWheel.Invoke(this, eventArgs);
                }

                else if (this.MouseMove != null
                    && wParam == (int)wScreenshot.Native.Win32.MouseMessages.WM_MOUSEMOVE)
                {
                    eventArgs = new MouseHookEventArgs(null, 0, lParam.pt.X, lParam.pt.Y, 0, false);
                    this.MouseMove.Invoke(this, eventArgs);
                }
                if (eventArgs != null && eventArgs.Handled)
                {
                    return -1;
                }
            }
            return wScreenshot.Native.Win32.CallNextHookEx(this.m_hHook, code, wParam, ref lParam);
        }

        /// <summary>
        /// Installs the global hook 'mouse'
        /// </summary>
        private void InitHook()
        {
            this.HookProcHandler = new wScreenshot.Native.Win32.mouseHookProc(this.HookProc);
            this.m_hInstance = wScreenshot.Native.Win32.LoadLibrary("User32");
            this.m_hHook = wScreenshot.Native.Win32.SetWindowsHookEx((int)wScreenshot.Native.Win32.WindowsHook.WH_MOUSE_LL, this.HookProcHandler, this.m_hInstance, 0);
        }

        /// <summary>
        /// Uninstalls the global hook
        /// </summary>
        private void UnHook()
        {
            this.HookProcHandler = null;
            wScreenshot.Native.Win32.FreeLibrary(this.m_hInstance);
            wScreenshot.Native.Win32.UnhookWindowsHookEx(m_hHook);
        }

        #endregion Methods
    }
}