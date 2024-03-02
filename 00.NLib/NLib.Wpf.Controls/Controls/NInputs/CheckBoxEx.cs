#region Using

using NLib.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace NLib.Wpf.Controls
{
    /// <summary>
    /// The CheckBox Ex Control.
    /// </summary>
    public class CheckBoxEx : CheckBox
    {
        #region Static Constructor

        static CheckBoxEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckBoxEx), new FrameworkPropertyMetadata(typeof(CheckBoxEx)));
        }

        #endregion

        #region Dependentcy Properties

        public bool InvertCheckStateOrder
        {
            get { return (bool)GetValue(InvertCheckStateOrderProperty); }
            set { SetValue(InvertCheckStateOrderProperty, value); }
        }

        public static readonly DependencyProperty InvertCheckStateOrderProperty =
            DependencyProperty.Register("InvertCheckStateOrder", typeof(bool), typeof(CheckBoxEx), new UIPropertyMetadata(false));

        #endregion

        #region Override Methods

        protected override void OnToggle()
        {
            if (this.InvertCheckStateOrder)
            {
                if (this.IsChecked == true)
                {
                    this.IsChecked = false;
                }
                else if (this.IsChecked == false)
                {
                    this.IsChecked = this.IsThreeState ? null : (bool?)true;
                }
                else
                {
                    this.IsChecked = true;
                }
            }
            else
            {
                base.OnToggle();
            }
        }

        #endregion
    }
}
