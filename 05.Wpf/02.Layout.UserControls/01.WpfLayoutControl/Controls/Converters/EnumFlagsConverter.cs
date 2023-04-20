#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

#endregion

namespace WpfLayoutControl.Converters
{
    public class EnumBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            return Enum.Parse(targetType, parameterString);
        }

        #endregion
    }

    public class HasFlagConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            return Enum.Parse(targetType, parameterString);
        }

        #endregion
    }

    public class ShowButtonsConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool flag = false;

            Controls.FontAwesomeButtons pVal = Controls.FontAwesomeButtons.None;

            // Paramter Value
            if (null != parameter && parameter is Controls.FontAwesomeIcon)
            {
                Controls.FontAwesomeIcon icon = (Controls.FontAwesomeIcon)parameter;

                pVal = (Controls.FontAwesomeButtons)Enum.Parse(typeof(Controls.FontAwesomeButtons), 
                    icon.ToString());

                if (pVal == Controls.FontAwesomeButtons.Save)
                {
                    Console.WriteLine("Save");
                }
                else if (pVal == Controls.FontAwesomeButtons.Delete)
                {
                    Console.WriteLine("Delete");
                }
            }

            // Flags Value.
            if (value is Controls.FontAwesomeButtons)
            {
                Controls.FontAwesomeButtons oVal = (Controls.FontAwesomeButtons)value;
                flag = oVal.HasFlag(pVal);
            }
            /*
            string parameterString = parameter as string;
            if (parameterString == null)
                return Visibility.Collapsed;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return Visibility.Collapsed;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);
            bool hasFlag = parameterValue.Equals(value);

            */
            return (flag) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}
