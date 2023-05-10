#region Using

using NLib;
using NLib.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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

    public abstract class NProperty
    {
        protected abstract object GetValue();
        protected abstract void SetValue(object value);

        public override int GetHashCode()
        {
            return string.Format("{0}", PropertyName).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (null == obj)
                return false;
            return obj.GetHashCode() == this.GetHashCode();
        }

        public string PropertyName { get; set; }
    }

    public class NProperty<T> : NProperty
    {
        protected override object GetValue() { return Value; }
        protected override void SetValue(object value) { Value = (T)value; }

        public T Value { get; set; }
    }

    public class NProperties
    {
        private Dictionary<string, NProperty> _properties = new Dictionary<string, NProperty>();

        public T Get<T>(string proopertyName)
        {
            if (!_properties.ContainsKey(proopertyName))
            {
                _properties.Add(proopertyName, new NProperty<T>());
            }

            var inst = _properties[proopertyName] as NProperty<T>;
            return inst.Value;
        }

        public void Set<T>(string proopertyName, T value)
        {
            if (!_properties.ContainsKey(proopertyName))
            {
                _properties.Add(proopertyName, new NProperty<T>() { Value = value });
            }
            else
            {
                var inst = _properties[proopertyName] as NProperty<T>;
                inst.Value = value;
            }
        }
    }


    public class Demo : INotifyPropertyChanged
    {
        #region Internal Variables

        private NProperties _ = new NProperties();

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

        /*
        /// <summary>
        /// Internal Get (Lamda function).
        /// </summary>
        /// <param name="selectorExpression">The Expression function.</param>

        private U Get<T, U>(Expression<Func<T>> selectorExpression)
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
            return _.Get<U>(me.Member.Name);
        }
        /// <summary>
        /// Internal Get (Lamda function).
        /// </summary>
        /// <param name="selectorExpression">The Expression function.</param>
        /// <param name="Value">The Value to set.</param>

        private void Set<T, U>(Expression<Func<T>> selectorExpression, U Value)
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
            _.Set<U>(me.Member.Name, Value);
        }
        */

        #endregion

        #region Public Methods

        /// <summary>
        /// Raise Property Changed event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        public void Raise(string propertyName)
        {
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

        // execute time: 8+ ms @10000 items
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
                }
            }
        }
        */

        // execute time: 10+ to 33+ ms @10000 items
        public int Id 
        {
            get { return _.Get<int>("Id");  }
            set { _.Set<int>("Id", value); }
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
                rets.Add(new Demo() { Id = i });
            }

            var ts = DateTime.Now - dt;

            Console.WriteLine("Total times: {0:n3}", ts.TotalMilliseconds);

            return rets;
        }

        #endregion
    }
}
