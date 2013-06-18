using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace wScreenshot.Helper
{
    public static class VisualTreeHelperExtension
    {
        public static T FindChildControl<T>(this DependencyObject control) where T : class
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                if (child != null && child is T)
                {
                    return (T)Convert.ChangeType(child, typeof(T));
                }
                else
                {
                    FindChildControl<T>(child);
                }
            }
            return null;
        }

        public static T FindParentControl<T>(this DependencyObject control) where T : new()
        {
            DependencyObject parent = VisualTreeHelper.GetParent(control);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return (T)Convert.ChangeType(parent, typeof(T));
        }
    }
}