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
    /// The NGroupMenu Control
    /// </summary>
    [ContentProperty(nameof(Items))]
    public class NGroupMenu : Control
    {
        #region Constructor

        static NGroupMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NGroupMenu), new FrameworkPropertyMetadata(typeof(NGroupMenu)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NGroupMenu()
        {
            // init
            Items = new ObservableCollection<NGroupMenuItem>();
        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region Public Properties

        #region Items

        /// <summary>
        /// The Items Dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(nameof(Items), typeof(ObservableCollection<NGroupMenuItem>), typeof(NGroupMenu));
        /// <summary>
        /// Gets or sets Items.
        /// </summary>
        public ObservableCollection<NGroupMenuItem> Items
        {
            get { return (ObservableCollection<NGroupMenuItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        #endregion

        #endregion
    }
}
