using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using wScreenshot.Annotations;
using wScreenshot.DataObject;
using wScreenshot.Helper;
using wScreenshot.Hooks;

namespace wScreenshot.ScreenshotModule
{
    /// <summary>
    ///     Interaction logic for RedBoxTool.xaml
    /// </summary>
    public partial class RedBoxTool : Window, INotifyPropertyChanged
    {
        private readonly MouseHook _m;

        private readonly ObservableCollection<AnnoyingRectangle> borderCollectionSource =
            new ObservableCollection<AnnoyingRectangle>();

        private IntPtr _handle;
        private bool _isAltDown;
        private bool _isMoving;
        private KeyboardHook _k;

        private AnnoyingRectangle currentBorder;
        private bool lastMouseDownHandled;

        public RedBoxTool()
        {
            InitializeComponent();
            _m = new MouseHook();
            _k = new KeyboardHook();
            BorderContainer.ItemsSource = borderCollectionSource;
        }

        public bool IsAltDown
        {
            get { return _isAltDown; }
            set
            {
                if (value.Equals(_isAltDown)) return;
                _isAltDown = value;
                OnPropertyChanged("IsAltDown");
            }
        }

        public Point Down { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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

            _k.IsHooked = true;
            _k.KeyDown += (s, ev) =>
            {
                if (ev.Key == Key.System || ev.Key == Key.LeftAlt || ev.Key == Key.RightAlt)
                {
                    IsAltDown = true;
                    e.Handled = true;
                }
            };
            _k.KeyUp += (s, ev) =>
            {
                if (ev.Key == Key.System || ev.Key == Key.LeftAlt || ev.Key == Key.RightAlt)
                {
                    IsAltDown = false;
                    e.Handled = true;
                }
            };
        }

        private void m_MouseDown(object sender, MouseHookEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt)) return;
            e.Handled = true;
            _isMoving = true;

            currentBorder = new AnnoyingRectangle();
            currentBorder.Background = new LinearGradientBrush(
                Color.FromArgb(20, 255, 0, 0),
                Color.FromArgb(20, 255, 0, 0),
                90.0);
            borderCollectionSource.Add(currentBorder);
            e.Handled = lastMouseDownHandled = true;

            Down = new Point(e.X, e.Y);
        }

        private void m_MouseUp(object sender, MouseHookEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt)) return;
            _isMoving = false;
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

        private void m_MouseMove(object sender, MouseHookEventArgs e)
        {
            if (_isMoving)
            {
                SetBounds(Math.Min(e.X, (int)Down.X), Math.Min(e.Y, (int)Down.Y), Math.Abs(e.X - (int)Down.X),
                    Math.Abs(e.Y - (int)Down.Y));
            }
        }

        private void SetBounds(int left, int top, int width, int height)
        {
            if (_handle == IntPtr.Zero)
            {
                _handle = new WindowInteropHelper(this).Handle;
            }

            currentBorder.X = left;
            currentBorder.Width = width;
            currentBorder.Y = top;
            currentBorder.Height = height;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _m.IsHooked = false;
        }

        private void ResizerSW_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void ResizerSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void ResizerNW_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void ResizerNE_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}