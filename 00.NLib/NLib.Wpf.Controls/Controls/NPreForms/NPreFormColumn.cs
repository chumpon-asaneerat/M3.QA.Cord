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
    /// The NPreFormColumn Control.
    /// </summary>
    [ContentProperty(nameof(Items))]
    public class NPreFormColumn : Control
    {
        #region Constructor

        static NPreFormColumn()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NPreFormColumn), new FrameworkPropertyMetadata(typeof(NPreFormColumn)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NPreFormColumn()
        {
            Items = new ObservableCollection<NPreFormItem>();
        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region Public Properties

        #region Header

        /// <summary>
        /// The HeaderProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(
                nameof(Header),
                typeof(object),
                typeof(NPreFormColumn));
        /// <summary>
        /// Gets or sets Header Content.
        /// </summary>
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        #endregion

        #region Items

        /// <summary>
        /// The Items Dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(
                nameof(Items), 
                typeof(ObservableCollection<NPreFormItem>), 
                typeof(NPreFormColumn));
        /// <summary>
        /// Gets or sets Items.
        /// </summary>
        public ObservableCollection<NPreFormItem> Items
        {
            get { return (ObservableCollection<NPreFormItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        #endregion

        #endregion
    }
}
