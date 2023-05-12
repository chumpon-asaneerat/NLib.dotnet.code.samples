#region Using

using NLib;
using NLib.Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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

        #region ListView Handlers

        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            entry.DataContext = lv.SelectedItem;
        }

        #endregion

        #region Button Handlers

        private void cmdChange_Click(object sender, RoutedEventArgs e)
        {
            if (null != lv.SelectedItem)
            {
                var item = lv.SelectedItem as Demo;
                if (null != item)
                {
                    item.ResetTime();
                    item.Raise(() => item.ChangeTime);
                }
            }
        }

        #endregion

        #region Private Methods

        private void InitItems()
        {
            lv.ItemsSource = Demo.GetItems();

            Demo.Test();
        }

        #endregion
    }

    public class Demo : NLib.NInpc
    {
        #region Public Properties

        public int Id 
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        public string Description
        {
            get { return Get<string>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateTime();
                });
            }
        }

        private void UpdateTime()
        {
            _changeTime = DateTime.Now;
            Raise(() => ChangeTime);
        }

        public void ResetTime()
        {
            _changeTime = DateTime.MinValue;
        }

        private DateTime _changeTime = DateTime.Now;
        public string ChangeTime
        {
            get 
            {
                return _changeTime.ToString("HH:mm:ss.fff");
            }
            set { }
        }

        #endregion

        #region Static Methods

        public static List<Demo> GetItems(int iMax = 10000)
        {
            var rets = new List<Demo>();

            DateTime dt = DateTime.Now;

            for (int i = 0; i < iMax; ++i)
            {
                rets.Add(new Demo() { Id = i, Description = string.Format("Desc {0}", i) });
            }

            var ts = DateTime.Now - dt;

            Console.WriteLine("Total times: {0:n3}", ts.TotalMilliseconds);

            return rets;
        }

        public static void Test()
        {

        }

        #endregion
    }
}
