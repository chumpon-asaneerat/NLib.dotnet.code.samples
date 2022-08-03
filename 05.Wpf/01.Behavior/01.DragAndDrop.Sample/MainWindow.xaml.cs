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

        #region Drag/Drop Handlers

        private void imgSource_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void imgSource_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        private void imgSource_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void imgSource_DragOver(object sender, DragEventArgs e)
        {

        }

        private void imgSource_DragLeave(object sender, DragEventArgs e)
        {

        }

        private void imgSource_Drop(object sender, DragEventArgs e)
        {
            /*
            var droppedData = e.Data.GetData(typeof(Card)) as Card;
            var target = (sender as ListBoxItem).DataContext as Card;

            int targetIndex = CardListControl.Items.IndexOf(target);

            droppedData.Effect = null;
            droppedData.RenderTransform = null;

            Items.Remove(droppedData);
            Items.Insert(targetIndex, droppedData);

            // remove the visual feedback drag and drop item
            if (this._dragdropWindow != null)
            {
                this._dragdropWindow.Close();
                this._dragdropWindow = null;
            }
            */
        }

        private void imgSource_PreviewDrop(object sender, DragEventArgs e)
        {

        }

        #endregion
    }
}
