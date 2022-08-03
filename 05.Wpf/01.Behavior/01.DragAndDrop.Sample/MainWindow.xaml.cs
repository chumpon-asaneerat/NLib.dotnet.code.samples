#region Using

using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DragAndDropSample
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //imgSource.AllowDrop = true;
        }

        #region Drag/Drop Handlers


        /// <summary>
        /// We have to override this to allow drop functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgSource_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
        /// <summary>
        /// Evaluates the Data and performs the DragDropEffect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgSource_PreviewDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }
        /// <summary>
        /// The drop activity on the textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgSource_Drop(object sender, DragEventArgs e)
        {
            MessageBox.Show("Drop");
            // Get data object
            var dataObject = e.Data as DataObject;

            // Check for file list
            if (dataObject.ContainsFileDropList())
            {
                /*
                // Clear values
                TextBox1.Text = string.Empty;

                // Process file names
                StringCollection fileNames = dataObject.GetFileDropList();
                StringBuilder bd = new StringBuilder();
                foreach (var fileName in fileNames)
                {
                    bd.Append(fileName + "\n");
                }

                // Set text
                TextBox1.Text = bd.ToString();
                */
            }
        }

        #endregion

        private void cmdOpenExplorer_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", @"C:\Users");
        }
    }
}
