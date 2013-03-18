using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using wScreenshot.Model;

namespace wScreenshot.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region internal properties

        #endregion internal properties

        #region public properties

        public System.IntPtr Handle { get; set; }

        public wScreenshotModel Model
        {
            get
            {
                return this.DataContext as wScreenshotModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        #endregion public properties

        #region constructors

        public MainWindow()
        {
            InitializeComponent();
            this.StateChanged += new System.EventHandler(MainWindow_StateChanged);
            this.Model = new wScreenshotModel();
            btnCoolHole.AddHandler(Window.MouseMoveEvent, new MouseEventHandler(borderHole_MouseMove), true);
        }

        #endregion constructors

        #region private methods

        private void PrivateTest()
        {
            TimeSpan dur = new TimeSpan(0, 0, 0, 10);
            DateTime init = DateTime.Now;
            int i = 0;
            for (i = 0; (DateTime.Now - init) <= dur; i++)
            {
                new Random().Next();
            }
            int length = int.MaxValue / 1;
            Dictionary<string, TimeSpan> dict = new Dictionary<string, TimeSpan>();
            List<Action> tests = new List<Action>();
            tests.Add(delegate
            {
                int one = 0;
                double d = 0;
                object eha = 42;
                DateTime last = DateTime.Now;
                last = DateTime.Now;
                while (one++ < length)
                {
                    Type t = eha.GetType();
                    if (t == typeof(byte) ||
                    t == typeof(sbyte) ||
                    t == typeof(ushort) ||
                    t == typeof(short) ||
                    t == typeof(uint) ||
                    t == typeof(int) ||
                    t == typeof(float) ||
                    t == typeof(double))
                    {
                        d = (double)(int)eha;
                    }
                }
            });
            tests.Add(delegate
            {
                int one = 0;
                double d = 0;
                object eha = 42;
                DateTime last = DateTime.Now;
                last = DateTime.Now;
                List<int> hashes = new List<int>()
                 {
                 typeof(byte).GetHashCode(),
                 typeof(sbyte).GetHashCode(),
                 typeof(ushort).GetHashCode(),
                 typeof(short).GetHashCode(),
                 typeof(uint).GetHashCode(),
                 typeof(int).GetHashCode(),
                 typeof(float).GetHashCode(),
                 typeof(double).GetHashCode(),
                 };
                while (one++ < length)
                {
                    int h = eha.GetType().GetHashCode();
                    if (hashes.Contains(h))
                    {
                        d = (double)(int)eha;
                    }
                }
            });
            i = 0;
            foreach (var t in tests)
            {
                DateTime last = DateTime.Now;
                t.Invoke();
                dict.Add(i++.ToString(), DateTime.Now - last);
            }
            string outp = string.Join("\n", from dval in dict select dval.Key + ":\t" + dval.Value);
            var r = from asd in dict
                    select new
                    {
                        perc = (from k in dict
                                select k).Min(x => x.Value).TotalMilliseconds / asd.Value.TotalMilliseconds,
                        str = asd.Key + "/" + (from k in dict
                                               select k).OrderBy(x => x.Value).First().Key
                    };
            outp += "\n" + string.Join("\n", from dval in r
                                             select dval.str + ":\t" + dval.perc.ToString());
            MessageBox.Show(outp);
        }

        private void StartTheEnd()
        {
            wScreenshot.View.Options.CurrentOptions.Close();
            this.Close();
        }

        #endregion private methods

        #region eventhandlers

        private void MainWindow_StateChanged(object sender, System.EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.SizeToContent = System.Windows.SizeToContent.Manual;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            StartTheEnd();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Maximized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SizeToContent = System.Windows.SizeToContent.Manual;

            if ((Handle = new WindowInteropHelper(this).Handle) == IntPtr.Zero)
            {
                throw new InvalidOperationException("Could not get window handle for the main window.");
            }
        }

        private bool moving = false;

        private void mainBorder_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!moving && e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                moving = true;
                if (this.WindowState == System.Windows.WindowState.Maximized)
                {
                    var _lastMouseDown = e.GetPosition(this);
                    var _lastWindowPos = this.PointToScreen(new Point(0, 0));
                    var _lastHeight = this.ActualHeight;
                    var _lastWidth = this.ActualWidth;
                    this.WindowState = System.Windows.WindowState.Normal;
                    Left = _lastMouseDown.X + _lastWindowPos.X - (this.ActualWidth / _lastWidth) * _lastMouseDown.X;
                    Top = _lastMouseDown.Y + _lastWindowPos.Y - (this.ActualHeight / _lastHeight) * _lastMouseDown.Y;
                }

                this.WindowState = System.Windows.WindowState.Normal;
                this.DragMove();
                moving = false;
            }
        }

        private void borderHole_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (moving)
            {
                if (e.LeftButton == System.Windows.Input.MouseButtonState.Released)
                {
                    moving = false;

                    //borderHole.DataContext = false;
                    btHae.Content = "Up";
                    return;
                }
                btHae.Content = "Move";
                var pos = e.GetPosition(borderHole);
                var delta = new Point(pos.X - holePos.X, pos.Y - holePos.Y);
                if (delta.X > 0)
                {
                    btHoleCol1.Width = new GridLength(Math.Max(holePos.X, 0));
                    btHoleCol11.Width = new GridLength(Math.Max(holePos.X, 0));
                    btHoleCol2.Width = new GridLength(Math.Max(delta.X + 1, 0));
                    btHoleCol22.Width = new GridLength(Math.Max(delta.X - 1, 0));
                }
                else
                {
                    btHoleCol1.Width = new GridLength(Math.Max(pos.X + 1, 0));
                    btHoleCol11.Width = new GridLength(Math.Max(pos.X, 0));
                    btHoleCol2.Width = new GridLength(Math.Max(holePos.X - pos.X, 0));
                    btHoleCol22.Width = new GridLength(Math.Max(holePos.X - pos.X, 0));
                }
                if (delta.Y > 0)
                {
                    btHoleRow1.Height = new GridLength(Math.Max(holePos.Y, 0));
                    btHoleRow11.Height = new GridLength(Math.Max(holePos.Y - 1, 0));
                    btHoleRow2.Height = new GridLength(Math.Max(delta.Y, 0));
                    btHoleRow22.Height = new GridLength(Math.Max(delta.Y, 0));
                }
                else
                {
                    btHoleRow1.Height = new GridLength(Math.Max(pos.Y + 1, 0));
                    btHoleRow11.Height = new GridLength(Math.Max(pos.Y, 0));
                    btHoleRow2.Height = new GridLength(Math.Max(holePos.Y - pos.Y, 0));
                    btHoleRow22.Height = new GridLength(Math.Max(holePos.Y - pos.Y, 0));
                }
            }
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Native.User32.ReleaseCapture();
            Native.User32.SendMessage(this.Handle, Native.Win32.WindowsMessages.WM_SYSCOMMAND, Native.Win32.SystemCommands.SC_DRAGSIZE_SW, 0);
        }

        private Point holePos;

        private void btnCoolHole_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //e.Handled = true;
            moving = true;
            btHae.Content = "Down";
            holePos = e.GetPosition(borderHole);
        }

        private void btnCoolHole_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            moving = false;
            Model.IsSpecialWhiteButtonDown = false;

            //borderHole.DataContext = false;
            btHae.Content = "Up";
        }

        #endregion eventhandlers

        private void ShowOptionsCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ShowOptionsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            new Configuration().ShowDialog();
            new View.Options().ShowDialog();
        }

        private void DoUploadCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DoUploadCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        }
    }
}