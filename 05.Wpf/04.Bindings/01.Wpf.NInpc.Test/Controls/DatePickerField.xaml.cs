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

#endregion

namespace Wpf.NInpc.Test.Controls
{
    /// <summary>
    /// Interaction logic for DatePickerField.xaml
    /// </summary>
    public partial class DatePickerField : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DatePickerField()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        #region Caption

        /// <summary>
        /// The CaptionProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(DatePickerField),
                new FrameworkPropertyMetadata(null));
        /// <summary>
        /// Gets or sets Caption Text.
        /// </summary>
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        #endregion

        #region Value

        /// <summary>
        /// The ValueProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(DateTime?), typeof(DatePickerField), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets the Value which is being displayed or binding.
        /// </summary>
        public DateTime? Value
        {
            get { return (DateTime?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        #endregion

        #endregion
    }
}
