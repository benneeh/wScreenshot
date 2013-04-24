using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace wScreenshot.Native
{
    public class Win32
    {
        #region Const

        // public static readonly IntPtr TRUE = new IntPtr(1);
        // public static readonly IntPtr FALSE = IntPtr.Zero;

        #endregion Const

        public enum WindowsHook
        {
            //WH_CALLWNDPROC = 4,
            //WH_CALLWNDPROCRET = 12,
            //WH_CBT = 5,
            //WH_DEBUG = 9,
            //WH_FOREGROUNDIDLE = 11,
            //WH_GETMESSAGE = 3,
            //WH_JOURNALPLAYBACK = 1,
            //WH_JOURNALRECORD = 0,
            WH_KEYBOARD = 2,

            WH_KEYBOARD_LL = 13,
            WH_MOUSE = 7,
            WH_MOUSE_LL = 14,

            //WH_MSGFILTER = -1,
            //WH_SHELL = 10,
            //WH_SYSMSGFILTER = 6,
        }

        #region Declarations

        /// <summary>
        /// defines the callback type for the hook
        /// </summary>
        public delegate int keyboardHookProc(int code, int wParam, ref KBDLLHOOKSTRUCT lParam);

        /// <summary>
        /// defines the callback type for the hook
        /// </summary>
        public delegate int mouseHookProc(int code, int wParam, ref MSLLHOOKSTRUCT lParam);

        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hwnd"></param>
        // /// <param name="lParam"></param>
        // /// <returns></returns>
        // public delegate bool EnumWindowsProc(IntPtr hwnd, int lParam);

        #endregion Declarations

        #region Imports

        /// <summary>
        ///
        /// </summary>
        /// <param name="Hwnd"></param>
        /// <param name="ID"></param>
        /// <param name="Modifiers"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        /// <summary>
        ///
        /// </summary>
        /// <param name="Hwnd"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool UnregisterHotKey(IntPtr Hwnd, int ID);

        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="IDString"></param>
        // /// <returns></returns>
        // [DllImport("kernel32", EntryPoint = "GlobalAddAtomA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        // public static extern short GlobalAddAtom(string IDString);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="Atom"></param>
        // /// <returns></returns>
        // [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        // public static extern short GlobalDeleteAtom(short Atom);
        /// <summary>
        ///
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="callback"></param>
        /// <param name="hInstance"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, keyboardHookProc callback, IntPtr hInstance, uint threadId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);

        /// <summary>
        ///
        /// </summary>
        /// <param name="hModule"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// Sets the windows hook, do the desired event, one of hInstance or threadId must be non-null
        /// </summary>
        /// <param name="idHook">The id of the event you want to hook</param>
        /// <param name="callback">The callback.</param>
        /// <param name="hInstance">The handle you want to attach the event to, can be null</param>
        /// <param name="threadId">The thread you want to attach the event to, can be null</param>
        /// <returns>a handle to the desired hook</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, mouseHookProc callback, IntPtr hInstance, uint threadId);

        /// <summary>
        /// Unhooks the windows hook.
        /// </summary>
        /// <param name="hInstance">The hook handle that was returned from SetWindowsHookEx</param>
        /// <returns>True if successful, false otherwise</returns>
        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        /// <summary>
        /// Calls the next hook.
        /// </summary>
        /// <param name="idHook">The hook id</param>
        /// <param name="nCode">The hook code</param>
        /// <param name="wParam">The wparam.</param>
        /// <param name="lParam">The lparam.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref MSLLHOOKSTRUCT lParam);

        // /// <summary>
        // /// Loads the library.
        // /// </summary>
        // /// <param name="lpFileName">Name of the library</param>
        // /// <returns>A handle to the library</returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);

        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <param name="Msg"></param>
        // /// <param name="wParam"></param>
        // /// <param name="lParam"></param>
        // /// <returns></returns>
        // [return: MarshalAs(UnmanagedType.Bool)]
        // [DllImport("user32.dll", SetLastError = true)]
        // public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <param name="lpPaint"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern bool EndPaint(IntPtr hWnd, [In] ref PAINTSTRUCT lpPaint);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hwnd"></param>
        // /// <param name="lpPaint"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern IntPtr BeginPaint(IntPtr hwnd, out PAINTSTRUCT lpPaint);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <param name="lpRect"></param>
        // /// <param name="bErase"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hwnd"></param>
        // /// <param name="pwi"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hdc"></param>
        // /// <returns></returns>
        // [DllImport("gdi32.dll")]
        // public static extern bool DeleteDC(IntPtr hdc);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hObject"></param>
        // /// <returns></returns>
        // [DllImport("gdi32.dll")]
        // public static extern bool DeleteObject(IntPtr hObject);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hdc"></param>
        // /// <param name="nXDest"></param>
        // /// <param name="nYDest"></param>
        // /// <param name="nWidth"></param>
        // /// <param name="nHeight"></param>
        // /// <param name="hdcSrc"></param>
        // /// <param name="nXSrc"></param>
        // /// <param name="nYSrc"></param>
        // /// <param name="dwRop"></param>
        // /// <returns></returns>
        // [DllImport("gdi32.dll")]
        // public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth,
        // int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hdc"></param>
        // /// <param name="hgdiobj"></param>
        // /// <returns></returns>
        // [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        // public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hdc"></param>
        // /// <param name="nWidth"></param>
        // /// <param name="nHeight"></param>
        // /// <returns></returns>
        // [DllImport("gdi32.dll")]
        // public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth,
        // int nHeight);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hdc"></param>
        // /// <returns></returns>
        // [DllImport("gdi32.dll", SetLastError = true)]
        // public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hwnd"></param>
        // /// <param name="wMsg"></param>
        // /// <param name="wParam"></param>
        // /// <param name="lParam"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <param name="rectUpdate"></param>
        // /// <param name="hrgnUpdate"></param>
        // /// <param name="flags"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        // public static extern bool RedrawWindow(IntPtr hWnd, IntPtr rectUpdate, IntPtr hrgnUpdate, uint flags);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <param name="X"></param>
        // /// <param name="Y"></param>
        // /// <param name="nWidth"></param>
        // /// <param name="nHeight"></param>
        // /// <param name="bRepaint"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hwnd"></param>
        // /// <param name="pszSubAppName"></param>
        // /// <param name="pszSubIdList"></param>
        // /// <returns></returns>
        // [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        // public static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hwnd"></param>
        // /// <param name="lpRect"></param>
        // /// <returns></returns>
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr WindowFromPoint(POINT p);

        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hwnd"></param>
        // /// <param name="hrgnclip"></param>
        // /// <param name="fdwOptions"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        // public static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr hrgnclip, uint fdwOptions);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="nVirtKey"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern short GetKeyState(int nVirtKey);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <param name="nIndex"></param>
        // /// <param name="dwNewLong"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hwndOwner"></param>
        // /// <param name="x"></param>
        // /// <param name="y"></param>
        // /// <param name="lpszCaption"></param>
        // /// <param name="cObjects"></param>
        // /// <param name="lplpUnk"></param>
        // /// <param name="cPages"></param>
        // /// <param name="lpPageClsID"></param>
        // /// <param name="lcid"></param>
        // /// <param name="dwReserved"></param>
        // /// <param name="lpvReserved"></param>
        // [DllImport("olepro32.dll", PreserveSig = false)]
        // public static extern int OleCreatePropertyFrame(
        // IntPtr hwndOwner,
        // int x,
        // int y,
        // [MarshalAs(UnmanagedType.LPWStr)] string lpszCaption,
        // int cObjects,
        // [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown)] Object[] lplpUnk,
        // int cPages,
        // [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] Guid[] lpPageClsID,
        // int lcid,
        // int dwReserved,
        // IntPtr lpvReserved);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <returns></returns>
        // [DllImport("user32.dll", SetLastError = false)]
        // public static extern IntPtr GetDesktopWindow();
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <param name="lpdwProcessId"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll", SetLastError = true)]
        // public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="callback"></param>
        // /// <param name="lPar"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern int EnumWindows(EnumWindowsProc callback, int lPar);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <param name="nIndex"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll", SetLastError = true)]
        // public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="lpPoint"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern bool GetCursorPos(out Point lpPoint);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWndChild"></param>
        // /// <param name="hWndNewParent"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <returns></returns>
        // [DllImport("kernel32.dll")]
        // public static extern uint GetCurrentProcessId();
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <param name="hWndInsertAfter"></param>
        // /// <param name="X"></param>
        // /// <param name="Y"></param>
        // /// <param name="cx"></param>
        // /// <param name="cy"></param>
        // /// <param name="uFlags"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
        // int Y, int cx, int cy, int uFlags);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hwndParent"></param>
        // /// <param name="hwndChildAfter"></param>
        // /// <param name="lpszClass"></param>
        // /// <param name="lpszWindow"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll", SetLastError = true)]
        // public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="parentHandle"></param>
        // /// <param name="childAfter"></param>
        // /// <param name="className"></param>
        // /// <param name="windowTitle"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll", SetLastError = true)]
        // public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // [return: MarshalAs(UnmanagedType.Bool)]
        // public static extern bool SetForegroundWindow(IntPtr hWnd);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <param name="lpString"></param>
        // /// <param name="nMaxCount"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        #endregion Imports

        #region Structs

        /// <summary>
        ///
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        /// <summary>
        ///
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        /// <summary>
        ///
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        // /// <summary>
        // ///
        // /// </summary>
        // [StructLayout(LayoutKind.Sequential)]
        // public struct PAINTSTRUCT
        // {
        // public IntPtr hdc;
        // public bool fErase;
        // public RECT rcPaint;
        // public bool fRestore;
        // public bool fIncUpdate;
        // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        // public byte[] rgbReserved;
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // [StructLayoutAttribute(LayoutKind.Sequential)]
        // public struct WINDOWINFO
        // {
        // /// DWORD->unsigned int
        // public uint cbSize;

        // /// RECT->tagRECT
        // public RECT rcWindow;

        // /// RECT->tagRECT
        // public RECT rcClient;

        // /// DWORD->unsigned int
        // public uint dwStyle;

        // /// DWORD->unsigned int
        // public uint dwExStyle;

        // /// DWORD->unsigned int
        // public uint dwWindowStatus;

        // /// UINT->unsigned int
        // public uint cxWindowBorders;

        // /// UINT->unsigned int
        // public uint cyWindowBorders;

        // /// ATOM->WORD->unsigned short
        // public ushort atomWindowType;

        // /// WORD->unsigned short
        // public ushort wCreatorVersion;
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // [StructLayout(LayoutKind.Sequential)]
        // public struct TOOLINFO
        // {
        // public int cbSize;
        // public int uFlags;
        // public IntPtr hwnd;
        // public IntPtr uId;
        // public Rectangle rect;
        // public IntPtr hinst;
        // [MarshalAs(UnmanagedType.LPTStr)]
        // public string lpszText;
        // public uint lParam;
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern IntPtr GetWindowDC(IntPtr hWnd);
        // /// <summary>
        // ///
        // /// </summary>
        // [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        // public struct CREATESTRUCT
        // {
        // public IntPtr lpCreateParams;
        // public IntPtr hInstance;
        // public IntPtr hMenu;
        // public IntPtr hwndParent;
        // public int cy;
        // public int cx;
        // public int y;
        // public int x;
        // public int style;
        // public string lpszName;
        // public string lpszClass;
        // public int dwExStyle;

        // public Rectangle Rectangle
        // {
        // get { return Rectangle.FromLTRB(x, y, cx, cy); }
        // }
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // [StructLayout(LayoutKind.Sequential)]
        // public struct NCCALCSIZE_PARAMS
        // {
        // /// <summary>
        // /// Contains the new coordinates of a window that has been moved or resized, that is, it is the proposed new window coordinates.
        // /// </summary>
        // public RECT rectProposed;
        // /// <summary>
        // /// Contains the coordinates of the window before it was moved or resized.
        // /// </summary>
        // public RECT rectBeforeMove;
        // /// <summary>
        // /// Contains the coordinates of the window's client area before the window was moved or resized.
        // /// </summary>
        // public RECT rectClientBeforeMove;
        // /// <summary>
        // /// Pointer to a WINDOWPOS structure that contains the size and position values specified in the operation that moved or resized the window.
        // /// </summary>
        // public WINDOWPOS lpPos;
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // [StructLayout(LayoutKind.Sequential)]
        // public struct WINDOWPOS
        // {
        // internal IntPtr hwnd;
        // internal IntPtr hWndInsertAfter;
        // internal int x;
        // internal int y;
        // internal int cx;
        // internal int cy;
        // internal uint flags;
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // [StructLayout(LayoutKind.Sequential)]
        // public struct MSG
        // {
        // public IntPtr hwnd;
        // public uint message;
        // public IntPtr wParam;
        // public IntPtr lParam;
        // public uint time;
        // public POINT pt;
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // [StructLayout(LayoutKind.Sequential)]
        // public struct SIZE
        // {
        // public int cx;
        // public int cy;

        // public SIZE(int cx, int cy)
        // {
        // this.cx = cx;
        // this.cy = cy;
        // }
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // [StructLayout(LayoutKind.Sequential)]
        // public struct PROPPAGEINFO
        // {
        // public UInt32 cb;
        // public IntPtr pszTitle;
        // public SIZE size;
        // public IntPtr pszDocString;
        // public IntPtr pszHelpFile;
        // public UInt32 dwHelpContext;
        // }
        /// <summary>
        ///
        /// </summary>
        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            #region Declarations

            public int m_nLeft;
            public int m_nTop;
            public int m_nRight;
            public int m_nBottom;

            #endregion Declarations

            #region Properties

            /// <summary>
            ///
            /// </summary>
            public int Height { get { return this.m_nBottom - this.m_nTop; } }

            /// <summary>
            ///
            /// </summary>
            public int Width { get { return this.m_nRight - this.m_nLeft; } }

            /// <summary>
            ///
            /// </summary>
            public Size Size { get { return new Size(this.Width, this.Height); } }

            /// <summary>
            ///
            /// </summary>
            public Point Location { get { return new Point(this.m_nLeft, this.m_nTop); } }

            /// <summary>
            ///
            /// </summary>
            public Rectangle Rectangle
            {
                get { return Rectangle.FromLTRB(this.m_nLeft, this.m_nTop, this.m_nRight, this.m_nBottom); }
            }

            /// <summary>
            ///
            /// </summary>
            public System.Windows.Rect Rect
            {
                get { return new System.Windows.Rect(this.m_nLeft, this.m_nTop, this.m_nRight, this.m_nBottom); }
            }

            #endregion Properties

            #region Constructors

            /// <summary>
            ///
            /// </summary>
            /// <param name="_nLeft_"></param>
            /// <param name="_nTop_"></param>
            /// <param name="_nRight_"></param>
            /// <param name="_nBottom_"></param>
            public RECT(int _nLeft_, int _nTop_, int _nRight_, int _nBottom_)
            {
                this.m_nLeft = _nLeft_;
                this.m_nTop = _nTop_;
                this.m_nRight = _nRight_;
                this.m_nBottom = _nBottom_;
            }

            #endregion Constructors

            #region Methods

            /// <summary>
            ///
            /// </summary>
            /// <param name="rect"></param>
            /// <returns></returns>
            public static RECT FromRectangle(Rectangle rect)
            {
                return new RECT(rect.Left,
                rect.Top,
                rect.Right,
                rect.Bottom);
            }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return this.m_nLeft ^ ((this.m_nTop << 13) | (this.m_nTop >> 0x13))
                ^ ((this.Width << 0x1a) | (this.Width >> 6))
                ^ ((this.Height << 7) | (this.Height >> 0x19));
            }

            #endregion Methods
        }

        #endregion Structs

        #region Enumerations

        /// <summary>
        ///
        /// </summary>
        public enum MODKEY : int
        {
            MOD_NONE = 0,
            MOD_ALT = 1,
            MOD_CONTROL = 2,
            MOD_SHIFT = 4,
            MOD_WIN = 8
        }

        /// <summary>
        ///
        /// </summary>
        public enum MouseMessages
        {
            WM_LBUTTONDBLCLK = 0x203,
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202,
            WM_MBUTTONDOWN = 0x207,
            WM_MBUTTONUP = 0x208,
            WM_RBUTTONDOWN = 0x204,
            WM_RBUTTONUP = 0x205,
            WM_MOUSEMOVE = 0x200,
            WM_MOUSEWHEEL = 0x20A,
            WM_MOUSEHWHEEL = 0x20E,
        }

        // /// <summary>
        // ///
        // /// </summary>
        // public enum NCHITTEST
        // {
        // /// <summary>
        // /// On the screen background or on a dividing line between windows
        // /// (same as HTNOWHERE, except that the DefWindowProc function produces a system beep to indicate an error).
        // /// </summary>
        // HTERROR = (-2),
        // /// <summary>
        // /// In a window currently covered by another window in the same thread
        // /// (the message will be sent to underlying windows in the same thread until one of them returns a code that is not HTTRANSPARENT).
        // /// </summary>
        // HTTRANSPARENT = (-1),
        // /// <summary>
        // /// On the screen background or on a dividing line between windows.
        // /// </summary>
        // HTNOWHERE = 0,
        // /// <summary>In a client area.</summary>
        // HTCLIENT = 1,
        // /// <summary>In a title bar.</summary>
        // HTCAPTION = 2,
        // /// <summary>In a window menu or in a Close button in a child window.</summary>
        // HTSYSMENU = 3,
        // /// <summary>In a size box (same as HTSIZE).</summary>
        // HTGROWBOX = 4,
        // /// <summary>In a menu.</summary>
        // HTMENU = 5,
        // /// <summary>In a horizontal scroll bar.</summary>
        // HTHSCROLL = 6,
        // /// <summary>In the vertical scroll bar.</summary>
        // HTVSCROLL = 7,
        // /// <summary>In a Minimize button.</summary>
        // HTMINBUTTON = 8,
        // /// <summary>In a Maximize button.</summary>
        // HTMAXBUTTON = 9,
        // /// <summary>In the left border of a resizable window
        // /// (the user can click the mouse to resize the window horizontally).</summary>
        // HTLEFT = 10,
        // /// <summary>
        // /// In the right border of a resizable window
        // /// (the user can click the mouse to resize the window horizontally).
        // /// </summary>
        // HTRIGHT = 11,
        // /// <summary>In the upper-horizontal border of a window.</summary>
        // HTTOP = 12,
        // /// <summary>In the upper-left corner of a window border.</summary>
        // HTTOPLEFT = 13,
        // /// <summary>In the upper-right corner of a window border.</summary>
        // HTTOPRIGHT = 14,
        // /// <summary>	In the lower-horizontal border of a resizable window
        // /// (the user can click the mouse to resize the window vertically).</summary>
        // HTBOTTOM = 15,
        // /// <summary>In the lower-left corner of a border of a resizable window
        // /// (the user can click the mouse to resize the window diagonally).</summary>
        // HTBOTTOMLEFT = 16,
        // /// <summary>	In the lower-right corner of a border of a resizable window
        // /// (the user can click the mouse to resize the window diagonally).</summary>
        // HTBOTTOMRIGHT = 17,
        // /// <summary>In the border of a window that does not have a sizing border.</summary>
        // HTBORDER = 18,

        // HTOBJECT = 19,
        // /// <summary>In a Close button.</summary>
        // HTCLOSE = 20,
        // /// <summary>In a Help button.</summary>
        // HTHELP = 21,
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <param name="lpRect"></param>
        // /// <returns></returns>
        // [DllImport("User32", SetLastError = true)]
        // public static extern int GetClientRect(IntPtr hWnd, ref RECT lpRect);
        // /// <summary>
        // ///
        // /// </summary>
        // /// <param name="hWnd"></param>
        // /// <param name="hDC"></param>
        // /// <returns></returns>
        // [DllImport("user32.dll")]
        // public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        // /// <summary>
        // ///
        // /// </summary>
        // public enum TernaryRasterOperations : uint
        // {
        // SRCCOPY = 0x00CC0020, /* dest = source*/
        // SRCPAINT = 0x00EE0086, /* dest = source OR dest*/
        // SRCAND = 0x008800C6, /* dest = source AND dest*/
        // SRCINVERT = 0x00660046, /* dest = source XOR dest*/
        // SRCERASE = 0x00440328, /* dest = source AND (NOT dest )*/
        // NOTSRCCOPY = 0x00330008, /* dest = (NOT source)*/
        // NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */
        // MERGECOPY = 0x00C000CA, /* dest = (source AND pattern)*/
        // MERGEPAINT = 0x00BB0226, /* dest = (NOT source) OR dest*/
        // PATCOPY = 0x00F00021, /* dest = pattern*/
        // PATPAINT = 0x00FB0A09, /* dest = DPSnoo*/
        // PATINVERT = 0x005A0049, /* dest = pattern XOR dest*/
        // DSTINVERT = 0x00550009, /* dest = (NOT dest)*/
        // BLACKNESS = 0x00000042, /* dest = BLACK*/
        // WHITENESS = 0x00FF0062, /* dest = WHITE*/
        // };
        // /// <summary>
        // ///
        // /// </summary>
        public enum SystemCommands
        {
            // SC_SIZE = 0xF000,
            // SC_MOVE = 0xF010,
            // SC_MINIMIZE = 0xF020,
            // SC_MAXIMIZE = 0xF030,
            // SC_NEXTWINDOW = 0xF040,
            // SC_PREVWINDOW = 0xF050,
            // SC_CLOSE = 0xF060,
            // SC_VSCROLL = 0xF070,
            // SC_HSCROLL = 0xF080,
            // SC_MOUSEMENU = 0xF090,
            // SC_KEYMENU = 0xF100,
            // SC_ARRANGE = 0xF110,
            // SC_RESTORE = 0xF120,
            // SC_TASKLIST = 0xF130,
            // SC_SCREENSAVE = 0xF140,
            // SC_HOTKEY = 0xF150,

            SC_DRAGMOVE = 0xF012,
            SC_DRAGSIZE_N = 0xF003,
            SC_DRAGSIZE_S = 0xF006,
            SC_DRAGSIZE_E = 0xF002,
            SC_DRAGSIZE_W = 0xF001,
            SC_DRAGSIZE_NW = 0xF004,
            SC_DRAGSIZE_NE = 0xF005,
            SC_DRAGSIZE_SW = 0xF007,
            SC_DRAGSIZE_SE = 0xF008

            // SC_DEFAULT = 0xF160,
            // SC_MONITORPOWER = 0xF170,
            // SC_CONTEXTHELP = 0xF180,
            // SC_SEPARATOR = 0xF00F
        }

        // /// <summary>
        // ///
        // /// </summary>
        // [Flags]
        // public enum RedrawWindowOptions
        // {
        // RDW_INVALIDATE = 0x0001,
        // RDW_INTERNALPAINT = 0x0002,
        // RDW_ERASE = 0x0004,
        // RDW_VALIDATE = 0x0008,
        // RDW_NOINTERNALPAINT = 0x0010,
        // RDW_NOERASE = 0x0020,
        // RDW_NOCHILDREN = 0x0040,
        // RDW_ALLCHILDREN = 0x0080,
        // RDW_UPDATENOW = 0x0100,
        // RDW_ERASENOW = 0x0200,
        // RDW_FRAME = 0x0400,
        // RDW_NOFRAME = 0x0800
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // public enum DeviceContextValues : uint
        // {
        // /// <summary>DCX_WINDOW: Returns a DC that corresponds to the window rectangle rather
        // /// than the client rectangle.</summary>
        // Window = 0x00000001,
        // /// <summary>DCX_CACHE: Returns a DC from the cache, rather than the OWNDC or CLASSDC
        // /// window. Essentially overrides CS_OWNDC and CS_CLASSDC.</summary>
        // Cache = 0x00000002,
        // /// <summary>DCX_NORESETATTRS: Does not reset the attributes of this DC to the
        // /// default attributes when this DC is released.</summary>
        // NoResetAttrs = 0x00000004,
        // /// <summary>DCX_CLIPCHILDREN: Excludes the visible regions of all child windows
        // /// below the window identified by hWnd.</summary>
        // ClipChildren = 0x00000008,
        // /// <summary>DCX_CLIPSIBLINGS: Excludes the visible regions of all sibling windows
        // /// above the window identified by hWnd.</summary>
        // ClipSiblings = 0x00000010,
        // /// <summary>DCX_PARENTCLIP: Uses the visible region of the parent window. The
        // /// parent's WS_CLIPCHILDREN and CS_PARENTDC style bits are ignored. The origin is
        // /// set to the upper-left corner of the window identified by hWnd.</summary>
        // ParentClip = 0x00000020,
        // /// <summary>DCX_EXCLUDERGN: The clipping region identified by hrgnClip is excluded
        // /// from the visible region of the returned DC.</summary>
        // ExcludeRgn = 0x00000040,
        // /// <summary>DCX_INTERSECTRGN: The clipping region identified by hrgnClip is
        // /// intersected with the visible region of the returned DC.</summary>
        // IntersectRgn = 0x00000080,
        // /// <summary>DCX_EXCLUDEUPDATE: Unknown...Undocumented</summary>
        // ExcludeUpdate = 0x00000100,
        // /// <summary>DCX_INTERSECTUPDATE: Unknown...Undocumented</summary>
        // IntersectUpdate = 0x00000200,
        // /// <summary>DCX_LOCKWINDOWUPDATE: Allows drawing even if there is a LockWindowUpdate
        // /// call in effect that would otherwise exclude this window. Used for drawing during
        // /// tracking.</summary>
        // LockWindowUpdate = 0x00000400,
        // /// <summary>DCX_VALIDATE When specified with DCX_INTERSECTUPDATE, causes the DC to
        // /// be completely validated. Using this function with both DCX_INTERSECTUPDATE and
        // /// DCX_VALIDATE is identical to using the BeginPaint function.</summary>
        // Validate = 0x00200000,
        // }
        /// <summary>
        ///
        /// </summary>
        public enum WindowsMessages : uint
        {
            //WM_ACTIVATE = 0x6,
            //WM_ACTIVATEAPP = 0x1C,
            //WM_AFXFIRST = 0x360,
            //WM_AFXLAST = 0x37F,
            //WM_APP = 0x8000,
            //WM_ASKCBFORMATNAME = 0x30C,
            //WM_CANCELJOURNAL = 0x4B,
            //WM_CANCELMODE = 0x1F,
            //WM_CAPTURECHANGED = 0x215,
            WM_CHANGECBCHAIN = 0x30D,

            //WM_CHAR = 0x102,
            //WM_CHARTOITEM = 0x2F,
            //WM_CHILDACTIVATE = 0x22,
            //WM_CLEAR = 0x303,
            //WM_CLOSE = 0x10,
            //WM_COMMAND = 0x111,
            //WM_COMPACTING = 0x41,
            //WM_COMPAREITEM = 0x39,
            //WM_CONTEXTMENU = 0x7B,
            //WM_COPY = 0x301,
            //WM_COPYDATA = 0x4A,
            //WM_CREATE = 0x1,
            //WM_CTLCOLORBTN = 0x135,
            //WM_CTLCOLORDLG = 0x136,
            //WM_CTLCOLOREDIT = 0x133,
            //WM_CTLCOLORLISTBOX = 0x134,
            //WM_CTLCOLORMSGBOX = 0x132,
            //WM_CTLCOLORSCROLLBAR = 0x137,
            //WM_CTLCOLORSTATIC = 0x138,
            //WM_CUT = 0x300,
            //WM_DEADCHAR = 0x103,
            //WM_DELETEITEM = 0x2D,
            //WM_DESTROY = 0x2,
            //WM_DESTROYCLIPBOARD = 0x307,
            //WM_DEVICECHANGE = 0x219,
            //WM_DEVMODECHANGE = 0x1B,
            //WM_DISPLAYCHANGE = 0x7E,
            WM_DRAWCLIPBOARD = 0x308,

            //WM_DRAWITEM = 0x2B,
            //WM_DROPFILES = 0x233,
            //WM_ENABLE = 0xA,
            //WM_ENDSESSION = 0x16,
            //WM_ENTERIDLE = 0x121,
            //WM_ENTERMENULOOP = 0x211,
            //WM_ENTERSIZEMOVE = 0x231,
            //WM_ERASEBKGND = 0x14,
            //WM_EXITMENULOOP = 0x212,
            //WM_EXITSIZEMOVE = 0x232,
            //WM_FONTCHANGE = 0x1D,
            //WM_GETDLGCODE = 0x87,
            //WM_GETFONT = 0x31,
            //WM_GETHOTKEY = 0x33,
            //WM_GETICON = 0x7F,
            //WM_GETMINMAXINFO = 0x24,
            //WM_GETOBJECT = 0x3D,
            //WM_GETSYSMENU = 0x313,
            //WM_GETTEXT = 0xD,
            //WM_GETTEXTLENGTH = 0xE,
            //WM_HANDHELDFIRST = 0x358,
            //WM_HANDHELDLAST = 0x35F,
            //WM_HELP = 0x53,
            WM_HOTKEY = 0x312,

            //WM_HSCROLL = 0x114,
            //WM_HSCROLLCLIPBOARD = 0x30E,
            //WM_ICONERASEBKGND = 0x27,
            //WM_IME_CHAR = 0x286,
            //WM_IME_COMPOSITION = 0x10F,
            //WM_IME_COMPOSITIONFULL = 0x284,
            //WM_IME_CONTROL = 0x283,
            //WM_IME_ENDCOMPOSITION = 0x10E,
            //WM_IME_KEYDOWN = 0x290,
            //WM_IME_KEYLAST = 0x10F,
            //WM_IME_KEYUP = 0x291,
            //WM_IME_NOTIFY = 0x282,
            //WM_IME_REQUEST = 0x288,
            //WM_IME_SELECT = 0x285,
            //WM_IME_SETCONTEXT = 0x281,
            //WM_IME_STARTCOMPOSITION = 0x10D,
            //WM_INITDIALOG = 0x110,
            //WM_INITMENU = 0x116,
            //WM_INITMENUPOPUP = 0x117,
            //WM_INPUTLANGCHANGE = 0x51,
            //WM_INPUTLANGCHANGEREQUEST = 0x50,
            WM_KEYDOWN = 0x100,

            //WM_KEYFIRST = 0x100,
            //WM_KEYLAST = 0x108,
            WM_KEYUP = 0x101,

            //WM_KILLFOCUS = 0x8,
            //WM_LBUTTONDBLCLK = 0x203,
            //WM_LBUTTONDOWN = 0x201,
            //WM_LBUTTONUP = 0x202,
            //WM_MBUTTONDBLCLK = 0x209,
            //WM_MBUTTONDOWN = 0x207,
            //WM_MBUTTONUP = 0x208,
            //WM_MDIACTIVATE = 0x222,
            //WM_MDICASCADE = 0x227,
            //WM_MDICREATE = 0x220,
            //WM_MDIDESTROY = 0x221,
            //WM_MDIGETACTIVE = 0x229,
            //WM_MDIICONARRANGE = 0x228,
            //WM_MDIMAXIMIZE = 0x225,
            //WM_MDINEXT = 0x224,
            //WM_MDIREFRESHMENU = 0x234,
            //WM_MDIRESTORE = 0x223,
            //WM_MDISETMENU = 0x230,
            //WM_MDITILE = 0x226,
            //WM_MEASUREITEM = 0x2C,
            //WM_MENUCHAR = 0x120,
            //WM_MENUCOMMAND = 0x126,
            //WM_MENUDRAG = 0x123,
            //WM_MENUGETOBJECT = 0x124,
            //WM_MENURBUTTONUP = 0x122,
            //WM_MENUSELECT = 0x11F,
            //WM_MOUSEACTIVATE = 0x21,
            //WM_MOUSEFIRST = 0x200,
            //WM_MOUSEHOVER = 0x2A1,
            //WM_MOUSELAST = 0x20A,
            //WM_MOUSELEAVE = 0x2A3,
            //WM_MOUSEMOVE = 0x200,
            //WM_MOUSEWHEEL = 0x20A,
            //WM_MOUSEHWHEEL = 0x20E,
            //WM_MOVE = 0x3,
            //WM_MOVING = 0x216,
            //WM_NCACTIVATE = 0x86,
            //WM_NCCALCSIZE = 0x83,
            //WM_NCCREATE = 0x81,
            //WM_NCDESTROY = 0x82,
            //WM_NCHITTEST = 0x84,
            //WM_NCLBUTTONDBLCLK = 0xA3,
            //WM_NCLBUTTONDOWN = 0xA1,
            //WM_NCLBUTTONUP = 0xA2,
            //WM_NCMBUTTONDBLCLK = 0xA9,
            //WM_NCMBUTTONDOWN = 0xA7,
            //WM_NCMBUTTONUP = 0xA8,
            //WM_NCMOUSEHOVER = 0x2A0,
            //WM_NCMOUSELEAVE = 0x2A2,
            //WM_NCMOUSEMOVE = 0xA0,
            //WM_NCPAINT = 0x85,
            //WM_NCRBUTTONDBLCLK = 0xA6,
            //WM_NCRBUTTONDOWN = 0xA4,
            //WM_NCRBUTTONUP = 0xA5,
            //WM_NEXTDLGCTL = 0x28,
            //WM_NEXTMENU = 0x213,
            //WM_NOTIFY = 0x4E,
            //WM_NOTIFYFORMAT = 0x55,
            //WM_NULL = 0x0,
            //WM_PAINT = 0xF,
            //WM_PAINTCLIPBOARD = 0x309,
            //WM_PAINTICON = 0x26,
            //WM_PALETTECHANGED = 0x311,
            //WM_PALETTEISCHANGING = 0x310,
            //WM_PARENTNOTIFY = 0x210,
            //WM_PASTE = 0x302,
            //WM_PENWINFIRST = 0x380,
            //WM_PENWINLAST = 0x38F,
            //WM_POWER = 0x48,
            //WM_PRINT = 0x317,
            //WM_PRINTCLIENT = 0x318,
            //WM_QUERYDRAGICON = 0x37,
            //WM_QUERYENDSESSION = 0x11,
            //WM_QUERYNEWPALETTE = 0x30F,
            //WM_QUERYOPEN = 0x13,
            //WM_QUERYUISTATE = 0x129,
            //WM_QUEUESYNC = 0x23,
            //WM_QUIT = 0x12,
            //WM_RBUTTONDBLCLK = 0x206,
            //WM_RBUTTONDOWN = 0x204,
            //WM_RBUTTONUP = 0x205,
            //WM_RENDERALLFORMATS = 0x306,
            //WM_RENDERFORMAT = 0x305,
            //WM_SETCURSOR = 0x20,
            //WM_SETFOCUS = 0x7,
            //WM_SETFONT = 0x30,
            //WM_SETHOTKEY = 0x32,
            //WM_SETICON = 0x80,
            //WM_SETREDRAW = 0xB,
            //WM_SETTEXT = 0xC,
            //WM_SETTINGCHANGE = 0x1A,
            //WM_SHOWWINDOW = 0x18,
            //WM_SIZE = 0x5,
            //WM_SIZECLIPBOARD = 0x30B,
            //WM_SIZING = 0x214,
            //WM_SPOOLERSTATUS = 0x2A,
            //WM_STYLECHANGED = 0x7D,
            //WM_STYLECHANGING = 0x7C,
            //WM_SYNCPAINT = 0x88,
            //WM_SYSCHAR = 0x106,
            //WM_SYSCOLORCHANGE = 0x15,
            WM_SYSCOMMAND = 0x112,

            //WM_SYSDEADCHAR = 0x107,
            WM_SYSKEYDOWN = 0x104,

            WM_SYSKEYUP = 0x105,

            //WM_SYSTIMER = 0x118, // undocumented, see http://support.microsoft.com/?id=108938
            //WM_TCARD = 0x52,
            //WM_TIMECHANGE = 0x1E,
            //WM_TIMER = 0x113,
            //WM_UNDO = 0x304,
            //WM_UNINITMENUPOPUP = 0x125,
            //WM_USER = 0x400,
            //WM_USERCHANGED = 0x54,
            //WM_VKEYTOITEM = 0x2E,
            //WM_VSCROLL = 0x115,
            //WM_VSCROLLCLIPBOARD = 0x30A,
            //WM_WINDOWPOSCHANGED = 0x47,
            //WM_WINDOWPOSCHANGING = 0x46,
            //WM_WININICHANGE = 0x1A,
            //WM_XBUTTONDBLCLK = 0x20D,
            //WM_XBUTTONDOWN = 0x20B,
            //WM_XBUTTONUP = 0x20C
        }

        /// <summary>
        /// Virtual Keys
        /// </summary>
        public enum VirtualKeys : int
        {
            //VK_LBUTTON = 0x01, //Left mouse button
            //VK_RBUTTON = 0x02, //Right mouse button
            //VK_CANCEL = 0x03, //Control-break processing
            //VK_MBUTTON = 0x04, //Middle mouse button (three-button mouse)
            //VK_BACK = 0x08, //BACKSPACE key
            //VK_TAB = 0x09, //TAB key
            //VK_CLEAR = 0x0C, //CLEAR key
            //VK_RETURN = 0x0D, //ENTER key
            //VK_SHIFT = 0x10, //SHIFT key
            //VK_CONTROL = 0x11, //CTRL key
            //VK_MENU = 0x12, //ALT key
            //VK_PAUSE = 0x13, //PAUSE key
            //VK_CAPITAL = 0x14, //CAPS LOCK key
            //VK_ESCAPE = 0x1B, //ESC key
            //VK_SPACE = 0x20, //SPACEBAR
            //VK_PRIOR = 0x21, //PAGE UP key
            //VK_NEXT = 0x22, //PAGE DOWN key
            //VK_END = 0x23, //END key
            //VK_HOME = 0x24, //HOME key
            //VK_LEFT = 0x25, //LEFT ARROW key
            //VK_UP = 0x26, //UP ARROW key
            //VK_RIGHT = 0x27, //RIGHT ARROW key
            //VK_DOWN = 0x28, //DOWN ARROW key
            //VK_SELECT = 0x29, //SELECT key
            VK_PRINT = 0x2A, //PRINT key

            //VK_EXECUTE = 0x2B, //EXECUTE key
            VK_SNAPSHOT = 0x2C, //PRINT SCREEN key

            //VK_INSERT = 0x2D, //INS key
            //VK_DELETE = 0x2E, //DEL key
            //VK_HELP = 0x2F, //HELP key
            //VK_0 = 0x30, //0 key
            //VK_1 = 0x31, //1 key
            //VK_2 = 0x32, //2 key
            //VK_3 = 0x33, //3 key
            //VK_4 = 0x34, //4 key
            //VK_5 = 0x35, //5 key
            //VK_6 = 0x36, //6 key
            //VK_7 = 0x37, //7 key
            //VK_8 = 0x38, //8 key
            //VK_9 = 0x39, //9 key
            //VK_A = 0x41, //A key
            //VK_B = 0x42, //B key
            //VK_C = 0x43, //C key
            //VK_D = 0x44, //D key
            //VK_E = 0x45, //E key
            //VK_F = 0x46, //F key
            //VK_G = 0x47, //G key
            //VK_H = 0x48, //H key
            //VK_I = 0x49, //I key
            //VK_J = 0x4A, //J key
            //VK_K = 0x4B, //K key
            //VK_L = 0x4C, //L key
            //VK_M = 0x4D, //M key
            //VK_N = 0x4E, //N key
            //VK_O = 0x4F, //O key
            //VK_P = 0x50, //P key
            //VK_Q = 0x51, //Q key
            //VK_R = 0x52, //R key
            //VK_S = 0x53, //S key
            //VK_T = 0x54, //T key
            //VK_U = 0x55, //U key
            //VK_V = 0x56, //V key
            //VK_W = 0x57, //W key
            //VK_X = 0x58, //X key
            //VK_Y = 0x59, //Y key
            //VK_Z = 0x5A, //Z key
            //VK_NUMPAD0 = 0x60, //Numeric keypad 0 key
            //VK_NUMPAD1 = 0x61, //Numeric keypad 1 key
            //VK_NUMPAD2 = 0x62, //Numeric keypad 2 key
            //VK_NUMPAD3 = 0x63, //Numeric keypad 3 key
            //VK_NUMPAD4 = 0x64, //Numeric keypad 4 key
            //VK_NUMPAD5 = 0x65, //Numeric keypad 5 key
            //VK_NUMPAD6 = 0x66, //Numeric keypad 6 key
            //VK_NUMPAD7 = 0x67, //Numeric keypad 7 key
            //VK_NUMPAD8 = 0x68, //Numeric keypad 8 key
            //VK_NUMPAD9 = 0x69, //Numeric keypad 9 key
            //VK_SEPARATOR = 0x6C, //Separator key
            //VK_SUBTRACT = 0x6D, //Subtract key
            //VK_DECIMAL = 0x6E, //Decimal key
            //VK_DIVIDE = 0x6F, //Divide key
            //VK_F1 = 0x70, //F1 key
            //VK_F2 = 0x71, //F2 key
            //VK_F3 = 0x72, //F3 key
            //VK_F4 = 0x73, //F4 key
            //VK_F5 = 0x74, //F5 key
            //VK_F6 = 0x75, //F6 key
            //VK_F7 = 0x76, //F7 key
            //VK_F8 = 0x77, //F8 key
            //VK_F9 = 0x78, //F9 key
            //VK_F10 = 0x79, //F10 key
            //VK_F11 = 0x7A, //F11 key
            //VK_F12 = 0x7B, //F12 key
            //VK_SCROLL = 0x91, //SCROLL LOCK key
            //VK_LSHIFT = 0xA0, //Left SHIFT key
            //VK_RSHIFT = 0xA1, //Right SHIFT key
            //VK_LCONTROL = 0xA2, //Left CONTROL key
            //VK_RCONTROL = 0xA3, //Right CONTROL key
            //VK_LMENU = 0xA4, //Left MENU key
            //VK_RMENU = 0xA5, //Right MENU key
            //VK_PLAY = 0xFA, //Play key
            //VK_ZOOM = 0xFB, //Zoom key
        }

        // /// <summary>
        // ///
        // /// </summary>
        // [Flags]
        // public enum WindowExStyles : uint
        // {
        // /// <summary>
        // /// Specifies that a window created with this style accepts drag-drop files.
        // /// </summary>
        // WS_EX_ACCEPTFILES = 0x00000010,
        // /// <summary>
        // /// Forces a top-level window onto the taskbar when the window is visible.
        // /// </summary>
        // WS_EX_APPWINDOW = 0x00040000,
        // /// <summary>
        // /// Specifies that a window has a border with a sunken edge.
        // /// </summary>
        // WS_EX_CLIENTEDGE = 0x00000200,
        // /// <summary>
        // /// Windows XP: Paints all descendants of a window in bottom-to-top painting order using double-buffering. For more information, see Remarks. This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
        // /// </summary>
        // WS_EX_COMPOSITED = 0x02000000,
        // /// <summary>
        // /// Includes a question mark in the title bar of the window. When the user clicks the question mark, the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message. The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command. The Help application displays a pop-up window that typically contains help for the child window.
        // /// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
        // /// </summary>
        // WS_EX_CONTEXTHELP = 0x00000400,
        // /// <summary>
        // /// The window itself contains child windows that should take part in dialog box navigation. If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.
        // /// </summary>
        // WS_EX_CONTROLPARENT = 0x00010000,
        // /// <summary>
        // /// Creates a window that has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.
        // /// </summary>
        // WS_EX_DLGMODALFRAME = 0x00000001,
        // /// <summary>
        // /// Windows 2000/XP: Creates a layered window. Note that this cannot be used for child windows. Also, this cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
        // /// </summary>
        // WS_EX_LAYERED = 0x00080000,
        // /// <summary>
        // /// Arabic and Hebrew versions of Windows 98/Me, Windows 2000/XP: Creates a window whose horizontal origin is on the right edge. Increasing horizontal values advance to the left.
        // /// </summary>
        // WS_EX_LAYOUTRTL = 0x00400000,
        // /// <summary>
        // /// Creates a window that has generic left-aligned properties. This is the default.
        // /// </summary>
        // WS_EX_LEFT = 0x00000000,
        // /// <summary>
        // /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.
        // /// </summary>
        // WS_EX_LEFTSCROLLBAR = 0x00004000,
        // /// <summary>
        // /// The window text is displayed using left-to-right reading-order properties. This is the default.
        // /// </summary>
        // WS_EX_LTRREADING = 0x00000000,
        // /// <summary>
        // /// Creates a multiple-document interface (MDI) child window.
        // /// </summary>
        // WS_EX_MDICHILD = 0x00000040,
        // /// <summary>
        // /// Windows 2000/XP: A top-level window created with this style does not become the foreground window when the user clicks it. The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
        // /// To activate the window, use the SetActiveWindow or SetForegroundWindow function.
        // /// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
        // /// </summary>
        // WS_EX_NOACTIVATE = 0x08000000,
        // /// <summary>
        // /// Windows 2000/XP: A window created with this style does not pass its window layout to its child windows.
        // /// </summary>
        // WS_EX_NOINHERITLAYOUT = 0x00100000,
        // /// <summary>
        // /// Specifies that a child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
        // /// </summary>
        // WS_EX_NOPARENTNOTIFY = 0x00000004,
        // /// <summary>
        // /// Combines the WS_EX_CLIENTEDGE and WS_EX_WINDOWEDGE styles.
        // /// </summary>
        // WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
        // /// <summary>
        // /// Combines the WS_EX_WINDOWEDGE, WS_EX_TOOLWINDOW, and WS_EX_TOPMOST styles.
        // /// </summary>
        // WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
        // /// <summary>
        // /// The window has generic "right-aligned" properties. This depends on the window class. This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.
        // /// Using the WS_EX_RIGHT style for static or edit controls has the same effect as using the SS_RIGHT or ES_RIGHT style, respectively. Using this style with button controls has the same effect as using BS_RIGHT and BS_RIGHTBUTTON styles.
        // /// </summary>
        // WS_EX_RIGHT = 0x00001000,
        // /// <summary>
        // /// Vertical scroll bar (if present) is to the right of the client area. This is the default.
        // /// </summary>
        // WS_EX_RIGHTSCROLLBAR = 0x00000000,
        // /// <summary>
        // /// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using right-to-left reading-order properties. For other languages, the style is ignored.
        // /// </summary>
        // WS_EX_RTLREADING = 0x00002000,
        // /// <summary>
        // /// Creates a window with a three-dimensional border style intended to be used for items that do not accept user input.
        // /// </summary>
        // WS_EX_STATICEDGE = 0x00020000,
        // /// <summary>
        // /// Creates a tool window; that is, a window intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE.
        // /// </summary>
        // WS_EX_TOOLWINDOW = 0x00000080,
        // /// <summary>
        // /// Specifies that a window created with this style should be placed above all non-topmost windows and should stay above them, even when the window is deactivated. To add or remove this style, use the SetWindowPos function.
        // /// </summary>
        // WS_EX_TOPMOST = 0x00000008,
        // /// <summary>
        // /// Specifies that a window created with this style should not be painted until siblings beneath the window (that were created by the same thread) have been painted. The window appears transparent because the bits of underlying sibling windows have already been painted.
        // /// To achieve transparency without these restrictions, use the SetWindowRgn function.
        // /// </summary>
        // WS_EX_TRANSPARENT = 0x00000020,
        // /// <summary>
        // /// Specifies that a window has a border with a raised edge.
        // /// </summary>
        // WS_EX_WINDOWEDGE = 0x00000100
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // [Flags]
        // public enum WindowStyles : uint
        // {
        // WS_TOOLTIP = WS_POPUP | TTS_BALLOON | TTS_NOPREFIX | TTS_ALWAYSTIP,
        // TTS_ALWAYSTIP = 0x01,
        // TTS_NOPREFIX = 0x02,
        // TTS_BALLOON = 0x40,
        // WS_OVERLAPPED = 0x00000000,
        // WS_POPUP = 0x80000000,
        // WS_CHILD = 0x40000000,
        // WS_MINIMIZE = 0x20000000,
        // WS_VISIBLE = 0x10000000,
        // WS_DISABLED = 0x08000000,
        // WS_CLIPSIBLINGS = 0x04000000,
        // WS_CLIPCHILDREN = 0x02000000,
        // WS_MAXIMIZE = 0x01000000,
        // WS_BORDER = 0x00800000,
        // WS_DLGFRAME = 0x00400000,
        // WS_VSCROLL = 0x00200000,
        // WS_HSCROLL = 0x00100000,
        // WS_SYSMENU = 0x00080000,
        // WS_THICKFRAME = 0x00040000,
        // WS_GROUP = 0x00020000,
        // WS_TABSTOP = 0x00010000,
        // WS_MINIMIZEBOX = 0x00020000,
        // WS_MAXIMIZEBOX = 0x00010000,
        // WS_CAPTION = WS_BORDER | WS_DLGFRAME,
        // WS_TILED = WS_OVERLAPPED,
        // WS_ICONIC = WS_MINIMIZE,
        // WS_SIZEBOX = WS_THICKFRAME,
        // WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,
        // WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
        // WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
        // WS_CHILDWINDOW = WS_CHILD,
        // DS_MODALFRAME = 0x80,
        // WS_DIALOG = WS_DLGFRAME | DS_MODALFRAME | WS_POPUP
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // [Flags]
        // public enum WindowLong : int
        // {
        // GWL_ID = (-12),
        // GWL_STYLE = (-16),
        // GWL_EXSTYLE = (-20)
        // }
        // /// <summary>
        // ///
        // /// </summary>
        // [Flags]
        // public enum SetWindow : uint
        // {
        // SWP_NOSIZE = 0x0001,
        // SWP_NOMOVE = 0x0002,
        // SWP_NOZORDER = 0x0004,
        // SWP_NOREDRAW = 0x0008,
        // SWP_NOACTIVATE = 0x0010,
        // SWP_FRAMECHANGED = 0x0020, /* The frame changed: send WM_NCCALCSIZE */
        // SWP_SHOWWINDOW = 0x0040,
        // SWP_HIDEWINDOW = 0x0080,
        // SWP_NOCOPYBITS = 0x0100,
        // SWP_NOOWNERZORDER = 0x0200, /* Don't do owner Z ordering */
        // SWP_NOSENDCHANGING = 0x0400, /* Don't send WM_WINDOWPOSCHANGING */
        // SWP_POPUP = SWP_NOACTIVATE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER | SWP_FRAMECHANGED
        // }

        #endregion Enumerations
    }
}