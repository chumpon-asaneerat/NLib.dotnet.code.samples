#region Using

using NLib.Services;
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

namespace Wpf.NInpc.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Closing

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitItems();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void InitItems()
        {
            lv.ItemsSource = Demo.GetItems();
        }

        #endregion
    }

    public class Demo : NLib.NInpc
    {
        public int Id { get; set; }

        public static List<Demo> GetItems(int iMax = 10)
        {
            var rets = new List<Demo>();

            for (int i = 0; i < iMax; ++i)
            {
                rets.Add(new Demo() { Id = i });
            }

            return rets;
        }
    }
}
