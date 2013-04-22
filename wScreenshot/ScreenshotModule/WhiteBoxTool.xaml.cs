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
    public partial class WhiteBoxTool : Window
    {
        public WhiteBoxTool()
        {
            InitializeComponent();
        }

        private int DownX;
        private int DownY;
        private bool IsMoving = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Rect r = new Rect();
            ScreenHelper.AllScreens.Select(x => x.Bounds).ToList().ForEach(x => r.Union(x));
            this.Left = r.Left;
            this.Top = r.Top;
            this.Width = r.Width;
            this.Height = r.Height;
        }

        private Point holePos;

        public Point Down { get; set; }

        private void borderHole_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(borderHole);
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