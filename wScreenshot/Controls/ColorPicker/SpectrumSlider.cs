using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace wScreenshot.Controls
{
    public class SpectrumSlider : Slider
    {
        #region Public Methods

        static SpectrumSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (SpectrumSlider),
                new FrameworkPropertyMetadata(typeof (SpectrumSlider)));
        }

        public SpectrumSlider()
        {
            SetBackground();

            //Binding binding = new Binding();
            //binding.Path = new PropertyPath("Value");
            //binding.RelativeSource = new System.Windows.Data.RelativeSource(RelativeSourceMode.Self);
            //binding.Mode = BindingMode.TwoWay;
            //binding.Converter = new ValueToSelectedColorConverter();

            //BindingOperations.SetBinding(this, SelectedColorProperty, binding);
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            if (!m_withinChanging && !BindingOperations.IsDataBound(this, HueProperty))
            {
                m_withinChanging = true;
                Hue = 360 - newValue;
                m_withinChanging = false;
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void SetBackground()
        {
            var backgroundBrush = new LinearGradientBrush();
            backgroundBrush.StartPoint = new Point(0.5, 0);
            backgroundBrush.EndPoint = new Point(0.5, 1);

            const int spectrumColorCount = 30;

            Color[] spectrumColors = ColorUtils.GetSpectrumColors(spectrumColorCount);
            for (int i = 0; i < spectrumColorCount; ++i)
            {
                double offset = i*1.0/spectrumColorCount;
                var gradientStop = new GradientStop(spectrumColors[i], offset);
                backgroundBrush.GradientStops.Add(gradientStop);
            }
            Background = backgroundBrush;
        }

        private static void OnHuePropertyChanged(
            DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            var spectrumSlider = relatedObject as SpectrumSlider;
            if (spectrumSlider != null && !spectrumSlider.m_withinChanging)
            {
                spectrumSlider.m_withinChanging = true;

                var hue = (double) e.NewValue;
                spectrumSlider.Value = 360 - hue;

                spectrumSlider.m_withinChanging = false;
            }
        }

        #endregion Private Methods

        #region Dependency Properties

        public static readonly DependencyProperty HueProperty =
            DependencyProperty.Register("Hue", typeof (double), typeof (SpectrumSlider),
                new UIPropertyMetadata((double) 0, OnHuePropertyChanged));

        public double Hue
        {
            get { return (double) GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }

        #endregion Dependency Properties

        #region Private Members

        private bool m_withinChanging;

        #endregion Private Members
    }
}