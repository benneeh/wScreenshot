using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using wScreenshot.Helper;
using wScreenshot.Hooks;

namespace wScreenshot.ScreenshotModule
{
    /// <summary>
    /// Interaction logic for RedBoxTool.xaml
    /// </summary>
    public partial class WindowSelectorTool : Window
    {
        public WindowSelectorTool()
        {
            InitializeComponent();
            m = new MouseHook();
        }

        private MouseHook m;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Rect r = new Rect();
            ScreenHelper.AllScreens.Select(x => x.Bounds).ToList().ForEach(x => r.Union(x));
            this.Left = r.Left;
            this.Top = r.Top;
            this.Width = r.Width;
            this.Height = r.Height;

            m.IsHooked = true;
            m.MouseMove += m_MouseMove;
            m.MouseUp += m_MouseUp;
            m.MouseDown += m_MouseDown;
        }

        private void m_MouseDown(object sender, MouseHookEventArgs e)
        {
        }

        private void m_MouseUp(object sender, MouseHookEventArgs e)
        {
        }

        private void m_MouseMove(object sender, MouseHookEventArgs e)
        {
            if (_handle == IntPtr.Zero)
                _handle = new WindowInteropHelper(this).Handle;

            var hwnd = Native.Win32.WindowFromPoint(new Native.Win32.POINT(e.X, e.Y));

            if (_handle != hwnd)
            {
                Native.Win32.RECT re = new Native.Win32.RECT();
                Native.Win32.GetWindowRect(hwnd, out re);

                redBord.SetValue(Canvas.LeftProperty, (double)re.m_nLeft);
                redBord.SetValue(Canvas.WidthProperty, (double)re.Width);
                redBord.SetValue(Canvas.TopProperty, (double)re.m_nTop);
                redBord.SetValue(Canvas.HeightProperty, (double)re.Height);
            }
        }

        public IntPtr _handle { get; set; }
    }
}