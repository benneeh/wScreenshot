using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace wScreenshot.Adorners
{
    public class RotateChrome : Control
    {
        static RotateChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(RotateChrome), new FrameworkPropertyMetadata(typeof(RotateChrome)));
        }
    }
}