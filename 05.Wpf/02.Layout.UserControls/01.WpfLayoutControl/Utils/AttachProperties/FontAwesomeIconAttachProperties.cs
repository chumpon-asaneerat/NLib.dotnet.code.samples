#region Using

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

#endregion

namespace WpfLayoutControl.Utils
{
    #region FontAwesome Enum

    /// <summary>
    /// The FontAwesomeIcon Enum.
    /// </summary>
    public enum FontAwesomeIcon
    {
        None,
        Home,
        Save,
        Delete,
        DeleteAll,
        Import,
        Export,
        Print,
        Preview,
        Ok,
        Cancel
    }

    #endregion

    #region FontAwesomeOptions

    /// <summary>
    /// The FontAwesomeOptions class.
    /// </summary>
    public class FontAwesomeOptions
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(FontAwesomeIcon),
            typeof(FontAwesomeOptions),
            new PropertyMetadata(FontAwesomeIcon.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static FontAwesomeIcon GetIconType(DependencyObject obj)
        {
            return (FontAwesomeIcon)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, FontAwesomeIcon value)
        {
            obj.SetValue(IconTypeProperty, value);
        }

        #endregion

        #region IconType Changed Handler

        private static void IconTypePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (null != obj && obj is TextBlock)
            {
                TextBlock ctrl = obj as TextBlock;
                string sVal = (null != e.NewValue) ? e.NewValue.ToString() : null;
                FontAwesomeIcon val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? FontAwesomeIcon.None :
                        (FontAwesomeIcon)Enum.Parse(typeof(FontAwesomeIcon), sVal);
                }
                catch (Exception) 
                { 
                    val = FontAwesomeIcon.None; 
                }

                switch (val)
                {
                    case FontAwesomeIcon.Home:
                        {
                            var style = (Style)Application.Current.Resources["fa-home"];
                            ctrl.Style = style;
                        }
                        break;
                    case FontAwesomeIcon.Save:
                        {

                        }
                        break;
                    case FontAwesomeIcon.Delete:
                        {

                        }
                        break;
                    case FontAwesomeIcon.DeleteAll:
                        {

                        }
                        break;
                    case FontAwesomeIcon.Import:
                        {

                        }
                        break;
                    case FontAwesomeIcon.Export:
                        {

                        }
                        break;
                    case FontAwesomeIcon.Print:
                        {

                        }
                        break;
                    case FontAwesomeIcon.Preview:
                        {

                        }
                        break;
                    case FontAwesomeIcon.Ok:
                        {

                        }
                        break;
                    case FontAwesomeIcon.Cancel:
                        {

                        }
                        break;
                    default:
                        {
                            // FontAwesomeIcon.None
                        }
                        break;
                }

                if (val == FontAwesomeIcon.None)
                {

                }
                else
                {

                }
            }
        }

        #endregion
    }

    #endregion
}
