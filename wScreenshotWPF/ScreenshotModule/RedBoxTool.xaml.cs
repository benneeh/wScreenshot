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
using wScreenshot.Hooks;
namespace wScreenshot.ScreenshotModule
{
    /// <summary>
    /// Interaction logic for RedBoxTool.xaml
    /// </summary>
    public partial class RedBoxTool : Window
    {
        public RedBoxTool()
        {
            InitializeComponent();
            m = new MouseHook();
        }
        MouseHook m;
        int DownX;
        int DownY;
        bool IsMoving = false;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            m.IsHooked = true;
            m.MouseMove += m_MouseMove;
            m.MouseUp += m_MouseUp;
            m.MouseDown += m_MouseDown;
        }

        void m_MouseDown(object sender, MouseHookEventArgs e)
        {
            e.Handled = true;
            IsMoving = true;
            this.DownX = e.X;
            this.DownY = e.Y;
            this.MinWidth = 1;
            this.MinHeight = 1;

            SetBounds(Math.Min(e.X, DownX), Math.Min(e.Y, DownY), Math.Abs(e.X - DownX), Math.Abs(e.Y - DownY));
        }

        void m_MouseUp(object sender, MouseHookEventArgs e)
        {
            IsMoving = false;
        }

        void m_MouseMove(object sender, MouseHookEventArgs e)
        {
            if (IsMoving)
            {
                SetBounds(Math.Min(e.X, DownX), Math.Min(e.Y, DownY), Math.Abs(e.X - DownX), Math.Abs(e.Y - DownY));
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        private IntPtr _handle;
        private void SetBounds(int left, int top, int width, int height)
        {
            if (_handle == IntPtr.Zero)
                _handle = new WindowInteropHelper(this).Handle;

            SetWindowPos(_handle, IntPtr.Zero, left, top, width, height, 0);
        }

        [DllImport("user32")]
        static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int x,
            int y,
            int cx,
            int cy,
            uint uFlags);
        public Point Down { get; set; }
    }
}
