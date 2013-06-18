using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using wScreenshot.Helper;
using wScreenshot.Hooks;

namespace wScreenshot.ScreenshotModule
{
    /// <summary>
    ///     Interaction logic for RedBoxTool.xaml
    /// </summary>
    public partial class WhiteBoxTool : Window
    {
        private bool _isMoving;

        private Point _holePos;

        private readonly MouseHook _m;

        public WhiteBoxTool()
        {
            InitializeComponent();
            _m = new MouseHook();
        }

        public Point Down { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var r = new Rect();
            ScreenHelper.AllScreens.Select(x => x.Bounds).ToList().ForEach(x => r.Union(x));
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
            _holePos = new Point(e.X, e.Y);
        }

        private void m_MouseUp(object sender, MouseHookEventArgs e)
        {
            e.Handled = true;
            _isMoving = false;
        }

        private void m_MouseMove(object sender, MouseHookEventArgs e)
        {
            var pos = new Point(e.X, e.Y);
            if (!_isMoving)
            {
                _holePos = pos;
            }
            var delta = new Point(pos.X - _holePos.X, pos.Y - _holePos.Y);

            btHoleCol1.Width = new GridLength(Math.Min(_holePos.X, pos.X));
            btHoleCol11.Width = new GridLength(Math.Min(_holePos.X, pos.X));

            btHoleCol2.Width = new GridLength(Math.Max(Math.Abs(delta.X), 0));
            btHoleCol22.Width = new GridLength(Math.Abs(delta.X));

            btHoleRow1.Height = new GridLength(Math.Min(_holePos.Y, pos.Y));
            btHoleRow11.Height = new GridLength(Math.Min(_holePos.Y, pos.Y));

            btHoleRow2.Height = new GridLength(Math.Max(Math.Abs(delta.Y), 0));
            btHoleRow22.Height = new GridLength(Math.Abs(delta.Y));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _m.IsHooked = false;
        }
    }
}