using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using wScreenshot.Model;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;

namespace wScreenshot.View
{
    /// <summary>
    ///     Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        private static Options _CurrentOptions;
        private bool CloseReally;

        public Options()
        {
            InitializeComponent();
        }

        public static Options CurrentOptions
        {
            get
            {
                if (_CurrentOptions == null)
                {
                    _CurrentOptions = new Options();
                }
                return _CurrentOptions;
            }
        }

        public wScreenshotOptionsModel Model
        {
            get { return DataContext as wScreenshotOptionsModel; }
            set { DataContext = value; }
        }

        [DllImport("DwmApi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(
            IntPtr hwnd,
            ref MARGINS pMarInset);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtain the window handle for WPF application
                IntPtr mainWindowPtr = new WindowInteropHelper(this).Handle;
                HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
                mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

                // Get System Dpi
                Graphics desktop = Graphics.FromHwnd(mainWindowPtr);
                float DesktopDpiX = desktop.DpiX;
                float DesktopDpiY = desktop.DpiY;

                // Set Margins
                var margins = new MARGINS();

                // Extend glass frame into client area
                // Note that the default desktop Dpi is 96dpi. The  margins are
                // adjusted for the system Dpi.
                margins.cxLeftWidth = Convert.ToInt32(5*(DesktopDpiX/96));
                margins.cxRightWidth = Convert.ToInt32(5*(DesktopDpiX/96));
                margins.cyTopHeight = Convert.ToInt32((50 + 5)*(DesktopDpiX/96));
                margins.cyBottomHeight = Convert.ToInt32((bottomMarginIndicator.ActualHeight + 5)*(DesktopDpiX/96));

                int hr = DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);

                //
                if (hr < 0)
                {
                    //DwmExtendFrameIntoClientArea Failed
                }
            }

                // If not Vista, paint background white.
            catch (DllNotFoundException)
            {
                Application.Current.MainWindow.Background = Brushes.White;
            }
        }

        public new void Close()
        {
            CloseReally = true;
            base.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (CloseReally)
            {
            }
            else
            {
                e.Cancel = true;

                //this.Visibility = System.Windows.Visibility.Hidden;
                Hide();
            }
        }

        public struct MARGINS
        {
            public int cxLeftWidth; // width of left border that retains its size
            public int cxRightWidth; // width of right border that retains its size
            public int cyBottomHeight; // height of bottom border that retains its size
            public int cyTopHeight; // height of top border that retains its size
        };
    }
}