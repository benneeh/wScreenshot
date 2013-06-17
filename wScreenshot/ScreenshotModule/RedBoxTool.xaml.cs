using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using wScreenshot.Helper;
using wScreenshot.Hooks;

namespace wScreenshot.ScreenshotModule
{
    /// <summary>
    ///     Interaction logic for RedBoxTool.xaml
    /// </summary>
    public partial class RedBoxTool : Window
    {
        private readonly MouseHook _m;

        private int _downX;
        private int _downY;
        private IntPtr _handle;
        private bool _isMoving;

        public RedBoxTool()
        {
            InitializeComponent();
            _m = new MouseHook();
        }

        public Point Down { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var r = new Rect();
            ScreenHelper.AllScreens.Select(x => x.Bounds).ToList().ForEach(r.Union);
            Left = r.Left;
            Top = r.Top;
            Width = r.Width;
            Height = r.Height;

            _m.IsHooked = true;
            _m.MouseMove += m_MouseMove;
            _m.MouseUp += m_MouseUp;
            _m.MouseDown += m_MouseDown;
        }

        private void m_MouseDown(object sender, MouseHookEventArgs e)
        {
            e.Handled = true;
            _isMoving = true;
            _downX = e.X;
            _downY = e.Y;
            MinWidth = 1;
            MinHeight = 1;

            SetBounds(Math.Min(e.X, _downX), Math.Min(e.Y, _downY), Math.Abs(e.X - _downX), Math.Abs(e.Y - _downY));
        }

        private void m_MouseUp(object sender, MouseHookEventArgs e)
        {
            _isMoving = false;
        }

        private void m_MouseMove(object sender, MouseHookEventArgs e)
        {
            if (_isMoving)
            {
                SetBounds(Math.Min(e.X, _downX), Math.Min(e.Y, _downY), Math.Abs(e.X - _downX), Math.Abs(e.Y - _downY));
            }
        }

        private void SetBounds(int left, int top, int width, int height)
        {
            if (_handle == IntPtr.Zero)
            {
                _handle = new WindowInteropHelper(this).Handle;
            }

            //SetWindowPos(_handle, IntPtr.Zero, left, top, width, height, 0);
            redBord.SetValue(Canvas.LeftProperty, (double) left);
            redBord.SetValue(WidthProperty, (double) width);
            redBord.SetValue(Canvas.TopProperty, (double) top);
            redBord.SetValue(HeightProperty, (double) height);
        }

        [DllImport("user32")]
        private static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int x,
            int y,
            int cx,
            int cy,
            uint uFlags);

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _m.IsHooked = false;
        }
    }
}