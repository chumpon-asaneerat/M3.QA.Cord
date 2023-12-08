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
    /// The NGroupMenuItem Control
    /// </summary>
    [ContentProperty(nameof(Items))]
    public class NGroupMenuItem : Control
    {
        #region Constructor

        static NGroupMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NGroupMenuItem), new FrameworkPropertyMetadata(typeof(NGroupMenuItem)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NGroupMenuItem()
        {
            Items = new ObservableCollection<object>();
        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region Public Properties

        #region HeaderText

        /// <summary>
        /// The HeaderTextProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(NGroupMenuItem));
        /// <summary>
        /// Gets or sets Header Text.
        /// </summary>
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        #endregion

        #region HeaderBorderBrush

        /// <summary>
        /// The HeaderBorderBrushProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderBorderBrushProperty =
            DependencyProperty.Register("HeaderBorderBrush", typeof(Brush), typeof(NGroupMenuItem));
        /// <summary>
        /// Gets or sets Header Border Brush.
        /// </summary>
        public Brush HeaderBorderBrush
        {
            get { return (Brush)GetValue(HeaderBorderBrushProperty); }
            set { SetValue(HeaderBorderBrushProperty, value); }
        }

        #endregion

        #region HeaderBackground

        /// <summary>
        /// The HeaderBackgroundProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(NGroupMenuItem));
        /// <summary>
        /// Gets or sets Header Background.
        /// </summary>
        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        #endregion

        #region HeaderForeground

        /// <summary>
        /// The HeaderForegroundProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(NGroupMenuItem));
        /// <summary>
        /// Gets or sets Header Foreground.
        /// </summary>
        public Brush HeaderForeground
        {
            get { return (Brush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        #endregion

        #region Items

        /// <summary>
        /// The Items Dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<object>), typeof(NGroupMenuItem));
        /// <summary>
        /// Gets or sets Items.
        /// </summary>
        public ObservableCollection<object> Items
        {
            get { return (ObservableCollection<object>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        #endregion

        #region GroupWidth

        /// <summary>
        /// The GroupWidthProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty GroupWidthProperty =
            DependencyProperty.Register("GroupWidth", typeof(double), typeof(NGroupMenuItem));
        /// <summary>
        /// Gets or sets Group Width.
        /// </summary>
        public double GroupWidth
        {
            get { return (double)GetValue(GroupWidthProperty); }
            set { SetValue(GroupWidthProperty, value); }
        }


        #endregion

        #endregion
    }
}
