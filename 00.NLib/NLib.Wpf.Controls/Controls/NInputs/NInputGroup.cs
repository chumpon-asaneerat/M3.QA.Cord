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
    /// The NInputGroup Control
    /// </summary>
    [ContentProperty(nameof(Content))]
    public class NInputGroup : NInputControlBase
    {
        #region Constructor

        static NInputGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NInputGroup), 
                new FrameworkPropertyMetadata(typeof(NInputGroup)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NInputGroup() : base()
        {

        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region Public Properties

        #region Content

        /// <summary>
        /// The ContentProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(NInputGroup));
        /// <summary>
        /// Gets or sets Items.
        /// </summary>
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        #endregion

        #endregion
    }
}
