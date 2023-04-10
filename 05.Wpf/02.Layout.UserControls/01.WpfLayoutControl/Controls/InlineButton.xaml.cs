#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfLayoutControl.Pages;

#endregion

namespace WpfLayoutControl.Controls
{
    /// <summary>
    /// Interaction logic for InlineButton.xaml
    /// </summary>
    public partial class InlineButton : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public InlineButton()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        #region IconType

        /// <summary>
        /// The IconTypeProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IconTypeProperty =
            DependencyProperty.Register("IconType", typeof(FontAwesomeIcon), typeof(InlineButton));
        /// <summary>
        /// Gets or sets Inline Button Icon.
        /// </summary>
        public FontAwesomeIcon IconType
        {
            get { return (FontAwesomeIcon)GetValue(IconTypeProperty); }
            set { SetValue(IconTypeProperty, value); }
        }

        #endregion

        #region Text

        /// <summary>
        /// The TextProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(InlineButton));
        /// <summary>
        /// Gets or sets Inline Button Text.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        #endregion

        #region Public Events

        #endregion
    }
}
