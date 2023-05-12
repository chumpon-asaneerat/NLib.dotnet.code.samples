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
using static Wpf.NInpc.Test.NProperties;

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

    public class NProperties
    {
        #region Internal classes

        abstract class NProperty : IEditableObject
        {
            /// <summary>Begin Edit.</summary>
            public abstract void BeginEdit();
            /// <summary>End Edit.</summary>
            public abstract void EndEdit();
            /// <summary>Cancel Edit.</summary>
            public abstract void CancelEdit();
        }

        class NProperty<T> : NProperty
        {
            #region Internal Variables

            private bool _isEditing = false;
            private T _Value = default;
            private T _Original = default;

            #endregion

            #region Public Methods

            /// <summary>
            /// Begin Edit.
            /// </summary>
            public override void BeginEdit()
            {
                _isEditing = true;
                _Original = _Value;
            }
            /// <summary>
            /// End Edit.
            /// </summary>
            public override void EndEdit()
            {
                if (_isEditing)
                {
                    _Original = _Value;
                }
                _isEditing = false;
            }
            /// <summary>
            /// Cancel Edit.
            /// </summary>
            public override void CancelEdit()
            {
                if (_isEditing)
                {
                    _Value = _Original;
                }
                _isEditing = false;
            }

            #endregion

            #region Public Properties

            /// <summary>Gets or sets Value.</summary>
            public T Value 
            {
                get { return _Value; }
                set
                {
                    _Value = value;
                }
            }

            #endregion
        }

        #endregion

        #region Internal Variables

        private object olock = new object();
        private Dictionary<string, NProperty> _properties = new Dictionary<string, NProperty>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets Value.
        /// </summary>
        /// <typeparam name="T">The target proeprty type.</typeparam>
        /// <param name="proopertyName">The Property Name.</param>
        /// <returns>Returns Property value.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public T Get<T>(string proopertyName)
        {
            if (string.IsNullOrWhiteSpace(proopertyName)) 
                throw new ArgumentNullException(nameof(proopertyName));

            lock (olock)
            {
                if (!_properties.TryGetValue(proopertyName, out var _))
                {
                    var p = new NProperty<T>() { Value = default };
                    _properties.Add(proopertyName, p);
                }

                var inst = _properties[proopertyName] as NProperty<T>;
                return (null != inst) ? inst.Value : default;
            }
        }
        /// <summary>
        /// Set Value.
        /// </summary>
        /// <typeparam name="T">The target proeprty type.</typeparam>
        /// <param name="proopertyName">The Property Name.</param>
        /// <param name="value"></param>
        /// <returns>Returns True if assigned value is not equal to original value.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Set<T>(string proopertyName, T value)
        {
            if (string.IsNullOrWhiteSpace(proopertyName))
                throw new ArgumentNullException(nameof(proopertyName));

            lock (olock)
            {
                bool bChanged = false;
                if (!_properties.TryGetValue(proopertyName, out _))
                {
                    var p = new NProperty<T>() { Value = value };
                    _properties.Add(proopertyName, p);
                    bChanged = true;
                }
                else
                {
                    var inst = _properties[proopertyName] as NProperty<T>;
                    if (null != inst)
                    {
                        if (!inst.Value.Equals(value))
                        {
                            inst.Value = value;
                        }
                        bChanged = true;
                    }
                }
                return bChanged;
            }
        }

        #endregion
    }

    public class Demo : INotifyPropertyChanged
    {
        #region Internal Variables

        private NProperties _properties = new NProperties();

        #endregion

        #region Private Methods

        /// <summary>
        /// Internal Raise Property Changed event (Lamda function).
        /// </summary>
        /// <param name="selectorExpression">The Expression function.</param>

        private void InternalRaise<T>(Expression<Func<T>> selectorExpression)
        {
            if (null == selectorExpression)
            {
                throw new ArgumentNullException("selectorExpression");
                // return;
            }
            var me = selectorExpression.Body as MemberExpression;

            // Nullable properties can be nested inside of a convert function
            if (null == me)
            {
                var ue = selectorExpression.Body as UnaryExpression;
                if (null != ue)
                {
                    me = ue.Operand as MemberExpression;
                }
            }

            if (null == me)
            {
                throw new ArgumentException("The body must be a member expression");
                // return;
            }
            Raise(me.Member.Name);
        }

        private T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));
            return _properties.Get<T>(propertyName);
        }

        private void Set<T>(T Value, 
            Action action = null,
            [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));
            if (_properties.Set<T>(propertyName, Value))
            {
                Raise(propertyName); // Raise PropertyChanged
                if (null != action)
                {
                    // do some extra action.
                    Dispatcher.CurrentDispatcher.Invoke(action); // Dispatcher
                    //action.Call(); // NLib
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Raise Property Changed event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        public void Raise([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                return;
            // raise event.
            PropertyChanged.Call(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Raise Property Changed event (Lamda function).
        /// </summary>
        /// <param name="actions">The array of lamda expression's functions.</param>
        public void Raise(params Expression<Func<object>>[] actions)
        {
            if (null != actions && actions.Length > 0)
            {
                foreach (var item in actions)
                {
                    if (null != item) InternalRaise(item);
                }
            }
        }

        #endregion

        #region Public Properties

        // 50+ to 70+ ms @10000 items with raise events
        /*
        private int _id = 0;
        public int Id 
        {
            get { return _id; }
            set
            {
                if (_id != value) 
                {
                    _id = value;
                    Raise();
                    UpdateTime();
                }
            }
        }
        */

        // execute time: 70+ to 90+ ms @10000 items with raise events
        public int Id 
        {
            get { return Get<int>(); }
            set 
            {
                Set<int>(value, () => 
                {
                    UpdateTime();
                });
            }
        }

        public string Description
        {
            get { return Get<string>(); }
            set
            {
                Set<string>(value, () =>
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

        #region Public Events

        /// <summary>
        /// The PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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
