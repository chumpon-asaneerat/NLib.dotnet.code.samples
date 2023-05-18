﻿#region Using

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

using Wpf.Models;

namespace Wpf.Controls
{
    /// <summary>
    /// Interaction logic for WrapListBox.xaml
    /// </summary>
    public partial class WrapListBox : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WrapListBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            lst.ItemsSource = Product.Gets();
        }

        #endregion
    }
}
