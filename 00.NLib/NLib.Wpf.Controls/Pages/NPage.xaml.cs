#region Using

using NLib.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#endregion

namespace NLib.Wpf.Pages
{
    /// <summary>
    /// Interaction logic for NPage.xaml
    /// </summary>
    [ContentProperty("WorkArea")]
    public partial class NPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NPage()
        {
            InitializeComponent();
        }

        #endregion

        #region NavigateButtons Event Handlers

        private void nav_NavigatorButtonClick(object sender, NavigatorButtonEventArgs e)
        {
            RaiseNavigatorButtonClickEvent(e.Icon);
        }

        #endregion

        #region Public Properties

        #region PageTitle

        /// <summary>
        /// The PageTitleProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty PageTitleProperty =
            DependencyProperty.Register("PageTitle", typeof(string), typeof(NPage));
        /// <summary>
        /// Gets or sets Page Title.
        /// </summary>
        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        #endregion

        #region WorkArea

        /// <summary>
        /// The WorkAreaProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty WorkAreaProperty =
            DependencyProperty.Register("WorkArea", typeof(object), typeof(NPage));
        /// <summary>
        /// Gets or sets WorkArea Content.
        /// </summary>
        public object WorkArea
        {
            get { return (object)GetValue(WorkAreaProperty); }
            set { SetValue(WorkAreaProperty, value); }
        }

        #endregion

        #region ShowButtons

        /// <summary>
        /// The ShowButtonsProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowButtonsProperty =
            DependencyProperty.Register("ShowButtons", typeof(FontAwesomeButtons), typeof(NPage));
        /// <summary>
        /// Gets or sets Inline Button Icon.
        /// </summary>
        [TypeConverter(typeof(EnumConverter))]
        public FontAwesomeButtons ShowButtons
        {
            get { return (FontAwesomeButtons)GetValue(ShowButtonsProperty); }
            set { SetValue(ShowButtonsProperty, value); }
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
                    "NavigatorButtonClick", RoutingStrategy.Bubble, typeof(NavigatorButtonEventHandler), typeof(NPage));
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
