#region Using

using System.Windows;

#endregion

namespace NLib.Wpf.Controls
{
    /// <summary>
    /// The Navigator Button Event Handler Delegate.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void NavigatorButtonEventHandler(object sender, NavigatorButtonEventArgs e);
    /// <summary>
    /// The Navigator Button EventArgs class.
    /// </summary>
    public class NavigatorButtonEventArgs : RoutedEventArgs
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="routedEvent"></param>
        /// <param name="icon"></param>
        public NavigatorButtonEventArgs(RoutedEvent routedEvent, FontAwesomeIcon icon) : base(routedEvent)
        {
            this.Icon = icon;

        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or set Icon.
        /// </summary>
        public FontAwesomeIcon Icon { get; set; }

        #endregion
    }
}
