using System;
using System.Runtime.InteropServices;
using System.Text;
using wScreenshot.Dialog;

namespace wScreenshot.Native
{
    public class User32
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Win32.WindowsMessages Msg, Win32.SystemCommands wParam,
            int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Win32.RECT rect);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        // [DllImport("user32.dll")]
        // public static extern int ScrollWindow(IntPtr hwnd, int cx, int cy, ref Native.Win32.RECT rectScroll, ref Native.Win32.RECT rectClip);
        // [DllImport("user32.dll")]
        // public static extern bool SetWindowText(
        //IntPtr hWnd,
        //string lpString
        //);

        // [DllImport("user32.dll")]
        // public static extern IntPtr GetParent(
        // IntPtr hWnd
        //);
        // [DllImport("user32.dll")]
        // public static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, out SCROLLINFO lpsi);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false,
            ThrowOnUnmappableChar = true)]
        public static extern int LoadString(SafeModuleHandle hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd, Enum.FolderBrowserDialogMessage msg, IntPtr wParam,
            string lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, Enum.FolderBrowserDialogMessage msg, IntPtr wParam,
            IntPtr lParam);
    }
}