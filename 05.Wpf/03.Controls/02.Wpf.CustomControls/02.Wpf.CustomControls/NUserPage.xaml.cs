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
using static System.Net.Mime.MediaTypeNames;

#endregion

namespace Wpf.Controls
{
    /// <summary>
    /// Interaction logic for NUserPage.xaml
    /// </summary>
    public partial class NUserPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NUserPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        #region PageTitle

        /// <summary>
        /// The PageTitleProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty PageTitleProperty =
            DependencyProperty.Register("PageTitle", typeof(string), typeof(NUserPage));
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
            DependencyProperty.Register("WorkArea", typeof(object), typeof(NUserPage));
        /// <summary>
        /// Gets or sets WorkArea Content.
        /// </summary>
        public object WorkArea
        {
            get { return (object)GetValue(WorkAreaProperty); }
            set { SetValue(WorkAreaProperty, value); }
        }

        #endregion

        #endregion
    }
}
