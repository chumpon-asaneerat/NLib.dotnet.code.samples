#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace WpfLayoutControl.Controls
{
    /// <summary>
    /// Interaction logic for NavigateButtons.xaml
    /// </summary>
    public partial class NavigateButtons : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NavigateButtons()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        #region Dispatcher helper method (invoke event)

        private void InvokeAction(Action action)
        {
            try
            {
                if (null == action) return;
                if (null != Application.Current.Dispatcher)
                {
                    Application.Current.Dispatcher.BeginInvoke(action);
                }
                else
                {
                    action();
                }
            }
            catch { }
        }

        #endregion

        #region Button Handlers

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// The ShowButtonsProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowButtonsProperty =
            DependencyProperty.Register("ShowButtons", typeof(FontAwesomeButtons), typeof(NavigateButtons), new PropertyMetadata(OnShowButtonsChangedCallback));
        /// <summary>
        /// Gets or sets Inline Button Icon.
        /// </summary>
        [TypeConverter(typeof(EnumConverter))]
        public FontAwesomeButtons ShowButtons
        {
            get { return (FontAwesomeButtons)GetValue(ShowButtonsProperty); }
            set { SetValue(ShowButtonsProperty, value); }
        }

        private static void OnShowButtonsChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (0 == (uint)e.NewValue)
            {
                (sender as NavigateButtons).Visibility = Visibility.Collapsed;
            }
            else (sender as NavigateButtons).Visibility = Visibility.Visible;
        }

        #endregion
    }
}
