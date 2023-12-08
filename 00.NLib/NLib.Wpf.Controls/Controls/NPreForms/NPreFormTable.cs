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
    /// The NPreFormTable Control.
    /// </summary>
    [ContentProperty(nameof(Columns))]
    public class NPreFormTable : Control
    {
        #region Constructor

        static NPreFormTable()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NPreFormTable), new FrameworkPropertyMetadata(typeof(NPreFormTable)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NPreFormTable()
        {
            // init
            Columns = new ObservableCollection<NPreFormColumn>();
        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region Public Properties

        #region Columns

        /// <summary>
        /// The Columns Dependency property.
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register(nameof(Columns), typeof(ObservableCollection<NPreFormColumn>), typeof(NPreFormTable));
        /// <summary>
        /// Gets or sets Columns.
        /// </summary>
        public ObservableCollection<NPreFormColumn> Columns
        {
            get { return (ObservableCollection<NPreFormColumn>)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        #endregion

        #endregion
    }
}
