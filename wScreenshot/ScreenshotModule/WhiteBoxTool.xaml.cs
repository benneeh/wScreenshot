using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using wScreenshot.Helper;

namespace wScreenshot.ScreenshotModule
{
    /// <summary>
    ///     Interaction logic for RedBoxTool.xaml
    /// </summary>
    public partial class WhiteBoxTool : Window
    {
        private bool IsMoving;

        private Point holePos;

        public WhiteBoxTool()
        {
            InitializeComponent();
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
        }

        private void borderHole_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(borderHole);
            if (!IsMoving)
            {
                holePos = pos;
            }
            var delta = new Point(pos.X - holePos.X, pos.Y - holePos.Y);

            btHoleCol1.Width = new GridLength(Math.Min(holePos.X, pos.X) + 1);
            btHoleCol11.Width = new GridLength(Math.Min(holePos.X, pos.X));

            btHoleCol2.Width = new GridLength(Math.Max(Math.Abs(delta.X) - 1, 0));
            btHoleCol22.Width = new GridLength(Math.Abs(delta.X));

            btHoleRow1.Height = new GridLength(Math.Min(holePos.Y, pos.Y) + 1);
            btHoleRow11.Height = new GridLength(Math.Min(holePos.Y, pos.Y));

            btHoleRow2.Height = new GridLength(Math.Max(Math.Abs(delta.Y) - 1, 0));
            btHoleRow22.Height = new GridLength(Math.Abs(delta.Y));
        }

        private void borderHole_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsMoving = true;
            holePos = e.GetPosition(borderHole);
        }

        private void borderHole_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsMoving = false;
        }
    }
}