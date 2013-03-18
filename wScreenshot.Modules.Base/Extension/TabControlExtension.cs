using System.Windows;
using System.Windows.Media;

namespace wScreenshot.Modules.Base.Extension
{
    public static class TabControlExtension
    {
        #region HeaderBackground dependency property

        /// <summary>
        /// An attached dependency property which provides an
        /// <see cref="Brush" /> for arbitrary WPF elements.
        /// </summary>
        public static readonly DependencyProperty HeaderBackgroundProperty;

        /// <summary>
        /// Gets the <see cref="HeaderBackgroundProperty"/> for a given
        /// <see cref="DependencyObject"/>, which provides an
        /// <see cref="Brush" /> for arbitrary WPF elements.
        /// </summary>
        public static Brush GetHeaderBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HeaderBackgroundProperty);
        }

        /// <summary>
        /// Sets the attached <see cref="HeaderBackgroundProperty"/> for a given
        /// <see cref="DependencyObject"/>, which provides an
        /// <see cref="Brush" /> for arbitrary WPF elements.
        /// </summary>
        public static void SetHeaderBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HeaderBackgroundProperty, value);
        }

        #endregion HeaderBackground dependency property

        static TabControlExtension()
        {
            //register attached dependency property
            var metadata = new FrameworkPropertyMetadata((Brush)new SolidColorBrush(Colors.Red));
            HeaderBackgroundProperty = DependencyProperty.RegisterAttached("HeaderBackground",
            typeof(Brush),
            typeof(TabControlExtension), metadata);
        }
    }
}