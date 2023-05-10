﻿#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Markup;

#endregion

namespace NLib.Wpf.Controls.Converters
{
    #region IsLastItemConverter

    /// <summary>
    /// IsLastItemConverter class.
    /// </summary>
    public class IsLastItemConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ContentPresenter contentPresenter = value as ContentPresenter;
            ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(contentPresenter);
            int index = itemsControl.ItemContainerGenerator.IndexFromContainer(contentPresenter);
            return (index == (itemsControl.Items.Count - 1));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public IsLastItemConverter() { }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    #endregion
}
