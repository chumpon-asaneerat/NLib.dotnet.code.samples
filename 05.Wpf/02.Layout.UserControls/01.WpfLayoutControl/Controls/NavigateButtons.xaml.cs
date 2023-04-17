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
                    "NavigatorButtonClick", RoutingStrategy.Bubble, typeof(NavigatorButtonEventHandler), typeof(NavigateButtons));
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

    #region NavigatorButtonEvent Delegate and EventArgs

    /// <summary>
    /// The Navigator Button Event Handler Delegate.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void NavigatorButtonEventHandler(object sender, NavigatorButtonEventArgs e);
    /// <summary>
    /// The Navigator Button EventArgs class.
    /// </summary>
    public class NavigatorButtonEventArgs : RoutedEventArgs
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="routedEvent"></param>
        /// <param name="icon"></param>
        public NavigatorButtonEventArgs(RoutedEvent routedEvent, FontAwesomeIcon icon) : base(routedEvent)
        {
            this.Icon = icon;

        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or set Icon.
        /// </summary>
        public FontAwesomeIcon Icon { get; set; }

        #endregion
    }

    #endregion
}
