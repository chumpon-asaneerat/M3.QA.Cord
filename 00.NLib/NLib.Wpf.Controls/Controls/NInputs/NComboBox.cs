#region Using

using NLib.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#endregion

namespace NLib.Wpf.Controls
{
    /// <summary>
    /// The NComboBox Control
    /// </summary>
    public class NComboBox : NInputControlBase
    {
        #region Constructor

        static NComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NComboBox),
                new FrameworkPropertyMetadata(typeof(NComboBox)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NComboBox() : base() { }

        #endregion

        #region Internal Variables

        private ComboBox ctrl;

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (null != ctrl)
            {
                ctrl.SelectionChanged -= Ctrl_SelectionChanged;
            }
            ctrl = null;

            var obj = GetTemplateChild("ctrl");
            if (null != obj && obj is ComboBox)
            {
                ctrl = (ComboBox)obj;
            }
            if (null != ctrl)
            {
                ctrl.SelectionChanged += Ctrl_SelectionChanged;
            }
        }
        /// <summary>
        /// Focus internal control.
        /// </summary>
        public override void FocusControl()
        {
            if (null != ctrl) ctrl.Focus();
        }

        private void Ctrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RaiseSelectionChanged();
        }

        #endregion

        #region Private Methods

        private void RaiseSelectionChanged()
        {
            var remove = new List<object> { };
            var add = new List<object> { };
            //var selItem = ctrl.SelectedItem;
            //if (null != selItem) add.Add(selItem);
            var e = new SelectionChangedEventArgs(SelectionChangedEvent, remove, add);
            RaiseEvent(e);
        }

        #endregion

        #region Public Properties

        #region InputForeground

        /// <summary>
        /// The InputForegroundProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty InputForegroundProperty =
            DependencyProperty.Register(
                nameof(InputForeground),
                typeof(Brush),
                typeof(NComboBox),
                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets InputForeground.
        /// </summary>
        public Brush InputForeground
        {
            get { return (Brush)GetValue(InputForegroundProperty); }
            set { SetValue(InputForegroundProperty, value); }
        }

        #endregion

        #region IsEditable (for enable editable search)

        /// <summary>
        /// The IsEditableProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register(
                nameof(IsEditable),
                typeof(bool),
                typeof(NComboBox),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets IsEditable.
        /// </summary>
        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        #endregion

        #region ItemsSource (for datasoure binding info)

        /// <summary>
        /// The ItemsSourceProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                nameof(ItemsSource),
                typeof(object),
                typeof(NComboBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets ItemsSource.
        /// </summary>
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        #endregion

        #region DisplayMemberPath (for datasoure binding info)

        /// <summary>
        /// The DisplayMemberPathProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register(
                nameof(DisplayMemberPath),
                typeof(string),
                typeof(NComboBox),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets DisplayMemberPath.
        /// </summary>
        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        #endregion

        #region SelectedValuePath (for datasoure binding info)

        /// <summary>
        /// The SelectedValuePathProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register(
                nameof(SelectedValuePath),
                typeof(string),
                typeof(NComboBox),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets SelectedValuePath.
        /// </summary>
        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }

        #endregion

        #region SelectedValue (for datacontext binding info)

        /// <summary>
        /// The SelectedValueProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register(
                nameof(SelectedValue),
                typeof(object),
                typeof(NComboBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets SelectedValue.
        /// </summary>
        public object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        #endregion

        #region SelectedItem (for datacontext binding info access via coding)

        /// <summary>
        /// The SelectedItemProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                nameof(SelectedItem),
                typeof(object),
                typeof(NComboBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets Selected Item.
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        #endregion

        #region SelectedIndex

        /// <summary>
        /// Gets or sets SelectedIndex.
        /// </summary>
        public int SelectedIndex
        {
            get { return (null != ctrl) ? ctrl.SelectedIndex : -1; } 
            set 
            { 
                if (null != ctrl) ctrl.SelectedIndex = value; 
            }
        }

        #endregion

        #endregion

        #region Event Handlers

        /// <summary>
        /// The SelectionChangedEvent Route Event.
        /// </summary>
        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectionChanged", 
            RoutingStrategy.Bubble, 
            typeof(SelectionChangedEventHandler), 
            typeof(NComboBox));
        /// <summary>
        /// The SelectionChanged event Handler.
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        #endregion
    }
}
