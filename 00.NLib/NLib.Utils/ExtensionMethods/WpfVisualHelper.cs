﻿#region Using

using System.Windows;
using System.Windows.Media;

#endregion

namespace NLib
{
    /// <summary>
    /// WpfVisualHelper Extension Methods.
    /// </summary>
    public static class WpfVisualHelper
    {
        public static T GetChildOfType<T>(this DependencyObject depObj)
            where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }
    }
}
