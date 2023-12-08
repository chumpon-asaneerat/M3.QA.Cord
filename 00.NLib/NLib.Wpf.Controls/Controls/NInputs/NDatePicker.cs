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
    /// The NDatePicker Control
    /// </summary>
    public class NDatePicker : NInputControlBase
    {
        #region Constructor

        static NDatePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NDatePicker), 
                new FrameworkPropertyMetadata(typeof(NDatePicker)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NDatePicker() : base() { }

        #endregion

        #region Internal Variables

        private DatePicker ctrl;

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ctrl = null;
            var obj = GetTemplateChild("ctrl");
            if (null != obj && obj is DatePicker)
            {
                ctrl = (DatePicker)obj;
            }
        }
        /// <summary>
        /// Focus internal control.
        /// </summary>
        public override void FocusControl()
        {
            if (null != ctrl) ctrl.Focus();
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
                typeof(NDatePicker),
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

        #region SelectedDate

        /// <summary>
        /// The SelectedDateProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register(
                nameof(SelectedDate),
                typeof(DateTime?),
                typeof(NDatePicker),
                new FrameworkPropertyMetadata(new DateTime?(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Selected Date.
        /// </summary>
        public DateTime? SelectedDate
        {
            get { return (DateTime?)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        #endregion

        #endregion
    }
}
