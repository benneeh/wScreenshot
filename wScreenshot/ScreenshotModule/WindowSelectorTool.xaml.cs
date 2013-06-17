using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using wScreenshot.DataObject;
using wScreenshot.Helper;
using wScreenshot.Hooks;
using wScreenshot.Native;

namespace wScreenshot.ScreenshotModule
{
    /// <summary>
    ///     Interaction logic for RedBoxTool.xaml
    /// </summary>
    public partial class WindowSelectorTool : Window
    {
        private readonly ObservableCollection<AnnoyingRectangle> borderCollectionSource =
            new ObservableCollection<AnnoyingRectangle>();

        private readonly MouseHook m;
        private AnnoyingRectangle currentBorder;
        private bool lastMouseDownHandled;

        public WindowSelectorTool()
        {
            InitializeComponent();
            m = new MouseHook();
            BorderContainer.ItemsSource = borderCollectionSource;
            currentBorder = new AnnoyingRectangle();
            borderCollectionSource.Add(currentBorder);
        }

        public IntPtr _handle { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var r = new Rect();
            ScreenHelper.AllScreens.Select(x => x.Bounds).ToList().ForEach(x => r.Union(x));
            Left = r.Left;
            Top = r.Top;
            Width = r.Width;
            Height = r.Height;

            m.IsHooked = true;
            m.MouseMove += m_MouseMove;
            m.MouseUp += m_MouseUp;
            m.MouseDown += m_MouseDown;
        }

        private void m_MouseDown(object sender, MouseHookEventArgs e)
        {
            lastMouseDownHandled = false;
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                currentBorder.Background = new LinearGradientBrush(
                    Color.FromArgb(20, 255, 0, 0),
                    Color.FromArgb(20, 255, 0, 0),
                    90.0);
                currentBorder = new AnnoyingRectangle();
                borderCollectionSource.Add(currentBorder);
                e.Handled = lastMouseDownHandled = true;
            }
        }

        private void m_MouseUp(object sender, MouseHookEventArgs e)
        {
            e.Handled = lastMouseDownHandled;

            if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
            {
                var r = new Rect();
                borderCollectionSource.ToList()
                    .ForEach(x => r.Union(new Rect((double) x.X, (double) x.Y, (double) x.Width, (double) x.Height)));

                //screenshot
            }
        }

        private void m_MouseMove(object sender, MouseHookEventArgs e)
        {
            if (_handle == IntPtr.Zero)
                _handle = new WindowInteropHelper(this).Handle;

            IntPtr hwnd = Win32.WindowFromPoint(new Win32.POINT(e.X, e.Y));

            if (_handle != hwnd)
            {
                var re = new Win32.RECT();
                Win32.GetWindowRect(hwnd, out re);

                currentBorder.X = re.m_nLeft;
                currentBorder.Y = re.m_nTop;
                currentBorder.Width = re.Width;
                currentBorder.Height = re.Height;

                //currentBorder.SetValue(Canvas.LeftProperty, (double)re.m_nLeft);
                //currentBorder.SetValue(Canvas.WidthProperty, (double)re.Width);
                //currentBorder.SetValue(Canvas.TopProperty, (double)re.m_nTop);
                //currentBorder.SetValue(Canvas.HeightProperty, (double)re.Height);
            }
        }
    }
}