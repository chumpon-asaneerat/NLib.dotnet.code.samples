#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Wpf.Models
{
    public class Product
    {
        public string Name { get; set; }

        public static List<Product> Gets()
        {
            var rets = new List<Product>()
            {
                new Product() { Name = "Notebook" },
                new Product() { Name = "PC" },
                new Product() { Name = "Printer" },
                new Product() { Name = "Camera" },
                new Product() { Name = "Microphone" },
                new Product() { Name = "Cable" },
                new Product() { Name = "Monitor" }
            };

            return rets;
        }
    }
}
