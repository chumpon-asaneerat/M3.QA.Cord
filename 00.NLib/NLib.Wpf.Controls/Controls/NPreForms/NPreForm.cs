#region Using

using NLib.Wpf.Controls;
using NLib.Wpf.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// The NPreForm Control.
    /// </summary>
    [TemplatePart(Name = "PART_Header", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "PART_Body", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "PART_Footer", Type = typeof(ContentPresenter))]
    [ContentProperty(nameof(Body))]
    public class NPreForm : Control
    {
        #region Constructor

        static NPreForm()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NPreForm), new FrameworkPropertyMetadata(typeof(NPreForm)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NPreForm()
        {
            // init
            //Items = new ObservableCollection<NGroupMenuItem>();
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
                typeof(NPreForm));
        /// <summary>
        /// Gets or sets Header Content.
        /// </summary>
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        #endregion

        #region Body

        /// <summary>
        /// The BodyProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty BodyProperty =
            DependencyProperty.Register(
                nameof(Body),
                typeof(object),
                typeof(NPreForm));
        /// <summary>
        /// Gets or sets Body Content.
        /// </summary>
        public object Body
        {
            get { return (object)GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }

        #endregion

        #region Footer

        /// <summary>
        /// The FooterProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty FooterProperty =
            DependencyProperty.Register(
                nameof(Footer),
                typeof(object),
                typeof(NPreForm));
        /// <summary>
        /// Gets or sets Footer Content.
        /// </summary>
        public object Footer
        {
            get { return (object)GetValue(FooterProperty); }
            set { SetValue(FooterProperty, value); }
        }

        #endregion

        #endregion
    }
}
