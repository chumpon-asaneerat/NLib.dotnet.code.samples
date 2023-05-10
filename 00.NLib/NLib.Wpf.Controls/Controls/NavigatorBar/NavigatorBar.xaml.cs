#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace NLib.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for NavigatorBar.xaml
    /// </summary>
    public partial class NavigatorBar : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NavigatorBar()
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

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            RaiseNavigatorButtonClickEvent(FontAwesomeIcon.Add);
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            RaiseNavigatorButtonClickEvent(FontAwesomeIcon.Delete);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            RaiseNavigatorButtonClickEvent(FontAwesomeIcon.Save);
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            RaiseNavigatorButtonClickEvent(FontAwesomeIcon.Print);
        }

        private void cmdExport_Click(object sender, RoutedEventArgs e)
        {
            RaiseNavigatorButtonClickEvent(FontAwesomeIcon.Export);
        }

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            RaiseNavigatorButtonClickEvent(FontAwesomeIcon.Home);
        }

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            RaiseNavigatorButtonClickEvent(FontAwesomeIcon.Back);
        }

        #endregion

        #endregion

        #region Public Properties

        #region ShowButtons

        /// <summary>
        /// The ShowButtonsProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowButtonsProperty =
            DependencyProperty.Register("ShowButtons", typeof(FontAwesomeButtons), typeof(NavigatorBar), new PropertyMetadata(OnShowButtonsChangedCallback));
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
                (sender as NavigatorBar).Visibility = Visibility.Collapsed;
            }
            else (sender as NavigatorBar).Visibility = Visibility.Visible;
        }

        #endregion

        #endregion

        #region Public Events

        #region NavigatorButtonClick

        /// <summary>
        /// Raise NavigatorButtonClick Event.
        /// </summary>
        /// <param name="icon"></param>
        protected virtual void RaiseNavigatorButtonClickEvent(FontAwesomeIcon icon)
        {
            NavigatorButtonEventArgs args = new NavigatorButtonEventArgs(NavigatorButtonClickEvent, icon);
            RaiseEvent(args);
        }
        /// <summary>
        /// The NavigatorButtonClickEvent RouteEvent.
        /// </summary>
        public static readonly RoutedEvent NavigatorButtonClickEvent =
                EventManager.RegisterRoutedEvent(
                    "NavigatorButtonClick", RoutingStrategy.Bubble, typeof(NavigatorButtonEventHandler), typeof(NavigatorBar));
        /// <summary>
        /// Add or Remove NavigatorButtonClick event.
        /// </summary>
        public event NavigatorButtonEventHandler NavigatorButtonClick
        {
            add { AddHandler(NavigatorButtonClickEvent, value); }
            remove { RemoveHandler(NavigatorButtonClickEvent, value); }
        }

        #endregion

        #endregion
    }
}
