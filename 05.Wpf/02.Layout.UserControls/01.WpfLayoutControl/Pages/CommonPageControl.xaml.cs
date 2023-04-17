#region Using

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
using WpfLayoutControl.Controls;

#endregion

namespace WpfLayoutControl.Pages
{
    /// <summary>
    /// Interaction logic for CommonPageControl.xaml
    /// </summary>
    [ContentProperty("WorkArea")]
    public partial class CommonPageControl : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CommonPageControl()
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

        public static readonly DependencyProperty PageTitleProperty =
            DependencyProperty.Register("PageTitle", typeof(string), typeof(CommonPageControl));

        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        #endregion

        #region WorkArea

        public static readonly DependencyProperty WorkAreaProperty =
            DependencyProperty.Register("WorkArea", typeof(object), typeof(CommonPageControl));

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
            DependencyProperty.Register("ShowButtons", typeof(FontAwesomeButtons), typeof(CommonPageControl));
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
                    "NavigatorButtonClick", RoutingStrategy.Bubble, typeof(NavigatorButtonEventHandler), typeof(CommonPageControl));
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
