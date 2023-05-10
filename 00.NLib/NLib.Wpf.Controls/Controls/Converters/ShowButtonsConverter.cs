#region Using

using System;
using System.Windows;
using System.Windows.Data;

#endregion

namespace NLib.Wpf.Controls.Converters
{
    #region ShowButtonsConverter

    /// <summary>
    /// The ShowButtonsConverter class.
    /// </summary>
    public class ShowButtonsConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool flag = false;
            FontAwesomeButtons pVal = FontAwesomeButtons.None;
            try
            {
                // Paramter Value
                if (null != parameter && parameter is FontAwesomeIcon)
                {
                    FontAwesomeIcon icon = (FontAwesomeIcon)parameter;
                    pVal = (FontAwesomeButtons)Enum.Parse(typeof(FontAwesomeButtons),
                        icon.ToString());
                }
                // Flags Value.
                if (value is FontAwesomeButtons)
                {
                    FontAwesomeButtons oVal = (FontAwesomeButtons)value;
                    flag = oVal.HasFlag(pVal);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                flag = false;
            }

            return (flag) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    #endregion
}
