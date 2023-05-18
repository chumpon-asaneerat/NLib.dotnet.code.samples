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

using Wpf.Models;

namespace Wpf.Controls
{
    /// <summary>
    /// Interaction logic for WrapListBox.xaml
    /// </summary>
    public partial class WrapListBox : UserControl
    {
        public WrapListBox()
        {
            InitializeComponent();
        }

        public void Setup()
        {
            List<Product> products = new List<Product>()
            {
                new Product() { Name = "Notebook" },
                new Product() { Name = "PC" },
                new Product() { Name = "Printer" },
                new Product() { Name = "Camera" },
                new Product() { Name = "Microphone" },
                new Product() { Name = "Cable" },
                new Product() { Name = "Monitor" }
            };
            lst.ItemsSource = products;
        }
    }
}

namespace Wpf.Models
{
    public class Product
    {
        public string Name { get; set; }
    }
}
