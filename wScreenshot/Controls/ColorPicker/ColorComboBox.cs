using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace wScreenshot.Controls
{
    public class ColorComboBox : Control
    {
        #region Public Methods

        static ColorComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ColorComboBox),
                new FrameworkPropertyMetadata(typeof (ColorComboBox)));

            EventManager.RegisterClassHandler(typeof (ColorComboBox), ColorPicker.SelectedColorChangedEvent,
                new RoutedPropertyChangedEventHandler<Color>(OnColorPickerSelectedColorChanged));
        }

        #endregion Public Methods

        #region Dependency Properties

        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof (bool), typeof (ColorComboBox),
                new UIPropertyMetadata(false, OnIsDropDownOpenChanged));

        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof (Color), typeof (ColorComboBox),
                new UIPropertyMetadata(Colors.Transparent, OnSelectedColorPropertyChanged));

        public bool IsDropDownOpen
        {
            get { return (bool) GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        public Color SelectedColor
        {
            get { return (Color) GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        #endregion Dependency Properties

        #region Handling Events

        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorComboBox = d as ColorComboBox;
            var newValue = (bool) e.NewValue;

            // Mask HistTest visibility of toggle button otherwise when pressing it
            // and popup is open the popup is closed (since StaysOpen is false)
            // and reopens immediately
            if (colorComboBox.m_toggleButton != null)
            {
                colorComboBox.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(
                        delegate { colorComboBox.m_toggleButton.IsHitTestVisible = !newValue; }
                        ));
            }
            //Console.WriteLine("OnIsDropDownOpenChanged - Popup Focused {0} {1}",
            // colorComboBox.m_popup.IsFocused, colorComboBox.m_popup.IsKeyboardFocused);
        }

        private static void OnSelectedColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorComboBox = d as ColorComboBox;

            if (colorComboBox.m_withinChange)
                return;

            colorComboBox.m_withinChange = true;
            if (colorComboBox.m_colorPicker != null)
            {
                colorComboBox.m_colorPicker.SelectedColor = colorComboBox.SelectedColor;
            }
            colorComboBox.m_withinChange = false;
        }

        private static void OnColorPickerSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            var colorComboBox = sender as ColorComboBox;

            if (colorComboBox.m_withinChange)
                return;

            colorComboBox.m_withinChange = true;
            if (colorComboBox.m_colorPicker != null)
            {
                colorComboBox.SelectedColor = colorComboBox.m_colorPicker.SelectedColor;
            }
            colorComboBox.m_withinChange = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            m_popup = GetTemplateChild("PART_Popup") as UIElement;
            m_colorPicker = GetTemplateChild("PART_ColorPicker") as ColorPicker;
            m_toggleButton = GetTemplateChild("PART_ToggleButton") as ToggleButton;

            if (m_colorPicker != null)
            {
                m_colorPicker.SelectedColor = SelectedColor;
            }
        }

        #endregion Handling Events

        #region Private Members

        private ColorPicker m_colorPicker;
        private UIElement m_popup;
        private ToggleButton m_toggleButton;
        private bool m_withinChange;

        #endregion Private Members
    }
}