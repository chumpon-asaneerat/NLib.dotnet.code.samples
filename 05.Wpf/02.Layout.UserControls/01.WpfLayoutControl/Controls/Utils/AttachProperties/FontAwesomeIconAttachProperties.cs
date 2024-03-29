﻿#region Using

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

using WpfLayoutControl.Controls;

#endregion

namespace WpfLayoutControl.Utils
{
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

                Style style = null;
                switch (val)
                {
                    case FontAwesomeIcon.Home:
                        style = (Style)Application.Current.Resources["fa-home"];
                        break;
                    case FontAwesomeIcon.Close:
                        style = (Style)Application.Current.Resources["fa-close"];
                        break;

                    case FontAwesomeIcon.Add:
                        style = (Style)Application.Current.Resources["fa-addnew"];
                        break;
                    case FontAwesomeIcon.Edit:
                        style = (Style)Application.Current.Resources["fa-edit"];
                        break;
                    case FontAwesomeIcon.Save:
                        style = (Style)Application.Current.Resources["fa-save"];
                        break;
                    case FontAwesomeIcon.Delete:
                        style = (Style)Application.Current.Resources["fa-remove"];
                        break;

                    case FontAwesomeIcon.Import:
                        style = (Style)Application.Current.Resources["fa-import"];
                        break;
                    case FontAwesomeIcon.Export:
                        style = (Style)Application.Current.Resources["fa-export"];
                        break;

                    case FontAwesomeIcon.Search:
                        style = (Style)Application.Current.Resources["fa-search"];
                        break;
                    case FontAwesomeIcon.Scan:
                        style = (Style)Application.Current.Resources["fa-scan"];
                        break;
                    case FontAwesomeIcon.Refresh:
                        style = (Style)Application.Current.Resources["fa-refresh"];
                        break;

                    case FontAwesomeIcon.Copy:
                        style = (Style)Application.Current.Resources["fa-copy"];
                        break;

                    case FontAwesomeIcon.Print:
                        style = (Style)Application.Current.Resources["fa-print"];
                        break;
                    case FontAwesomeIcon.Preview:
                        style = (Style)Application.Current.Resources["fa-home"];
                        break;

                    case FontAwesomeIcon.Ok:
                        style = (Style)Application.Current.Resources["fa-ok"];
                        break;
                    case FontAwesomeIcon.Cancel:
                        style = (Style)Application.Current.Resources["fa-cancel"];
                        break;
                    default:
                        {
                            // FontAwesomeIcon.None
                        }
                        break;
                }
                // Apply style
                if (null != style)
                {
                    ctrl.Style = style;
                }
            }
        }

        #endregion
    }

    #endregion
}
