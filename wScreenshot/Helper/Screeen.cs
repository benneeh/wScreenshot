using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Windows;
using wScreenshot.Native;

namespace wScreenshot.Helper
{
    internal class ScreenHelper
    {
        #region Dll imports

        private const int MonitorinfofPrimary = 0x00000001;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        private static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] MonitorInfoEx info);

        [DllImport("user32.dll", ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        private static extern bool EnumDisplayMonitors(HandleRef hdc, IntPtr rcClip, MonitorEnumProc lpfnEnum,
            IntPtr dwData);

        private delegate bool MonitorEnumProc(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        private class MonitorInfoEx
        {
            internal int cbSize = Marshal.SizeOf(typeof (MonitorInfoEx));
            internal Win32.RECT rcMonitor = new Win32.RECT();
            internal Win32.RECT rcWork = new Win32.RECT();
            internal int dwFlags = 0;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] internal readonly char[] szDevice = new char[32];
        }

        #endregion Dll imports

        public static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

        private ScreenHelper(IntPtr monitor, IntPtr hdc)
        {
            var info = new MonitorInfoEx();
            GetMonitorInfo(new HandleRef(null, monitor), info);

            Bounds = info.rcMonitor.Rect;
            WorkingArea = info.rcWork.Rect;
            IsPrimary = ((info.dwFlags & MonitorinfofPrimary) != 0);
            Name = new string(info.szDevice).TrimEnd((char) 0);
        }

        public Rect Bounds { get; private set; }

        public Rect WorkingArea { get; private set; }

        public string Name { get; private set; }

        public bool IsPrimary { get; private set; }

        public static IEnumerable<ScreenHelper> AllScreens
        {
            get
            {
                var closure = new MonitorEnumCallback();
                var proc = new MonitorEnumProc(closure.Callback);
                EnumDisplayMonitors(NullHandleRef, IntPtr.Zero, proc, IntPtr.Zero);
                return closure.Screens.Cast<ScreenHelper>();
            }
        }

        private class MonitorEnumCallback
        {
            public MonitorEnumCallback()
            {
                Screens = new ArrayList();
            }

            public ArrayList Screens { get; private set; }

            public bool Callback(IntPtr screen, IntPtr hdc,
                IntPtr lprcMonitor, IntPtr lparam)
            {
                Screens.Add(new ScreenHelper(screen, hdc));
                return true;
            }
        }
    }
}