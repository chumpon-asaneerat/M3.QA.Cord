#region Using

using NLib.Wpf.Controls;
using System;
using System.Collections.Generic;
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

namespace NLib.Wpf.Pages
{
    /// <summary>
    /// The NPage Control
    /// </summary>
    [TemplatePart(Name = "PART_CaptionBorder", Type = typeof(Border))]
    [TemplatePart(Name = "PART_CaptionText", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_WorkAreaContent", Type = typeof(ContentPresenter))]
    [ContentProperty(nameof(WorkArea))]
    public class NPage : Control
    {
        #region Internal Variables

        protected Border CaptionBorder { get; private set; }
        protected TextBlock CaptionTextBlock { get; private set; }
        protected ContentPresenter WorkAreaContentPresenter { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Static Constructor.
        /// </summary>
        static NPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NPage), new FrameworkPropertyMetadata(typeof(NPage)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NPage()
        {

        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            CaptionBorder = Template.FindName("PART_CaptionBorder", this) as Border;
            CaptionTextBlock = Template.FindName("PART_CaptionText", this) as TextBlock;
            WorkAreaContentPresenter = Template.FindName("PART_WorkAreaContent", this) as ContentPresenter;

            base.OnApplyTemplate();
        }

        #endregion

        #region Public Properties

        #region HeaderText

        /// <summary>
        /// The HeaderTextProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(
                nameof(HeaderText), 
                typeof(string), 
                typeof(NPage),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Header Text.
        /// </summary>
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        #endregion

        #region HeaderFontSize

        /// <summary>
        /// The HeaderFontSizeProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register(
                nameof(HeaderFontSize),
                typeof(double),
                typeof(NPage),
                new FrameworkPropertyMetadata((double)14, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Header Font Size.
        /// </summary>
        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        #endregion

        #region HeaderFontWeight

        /// <summary>
        /// The HeaderFontWeightProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderFontWeightProperty =
            DependencyProperty.Register(
                nameof(HeaderFontWeight),
                typeof(FontWeight),
                typeof(NPage),
                new FrameworkPropertyMetadata(FontWeights.Normal, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Header Font Weight.
        /// </summary>
        public FontWeight HeaderFontWeight
        {
            get { return (FontWeight)GetValue(HeaderFontWeightProperty); }
            set { SetValue(HeaderFontWeightProperty, value); }
        }

        #endregion

        #region HeaderBorderBrush

        /// <summary>
        /// The HeaderBorderBrushProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderBorderBrushProperty =
            DependencyProperty.Register(
                nameof(HeaderBorderBrush),
                typeof(Brush), 
                typeof(NPage),
                new FrameworkPropertyMetadata(Brushes.CornflowerBlue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
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
            DependencyProperty.Register(
                nameof(HeaderBackground),
                typeof(Brush), 
                typeof(NPage),
                new FrameworkPropertyMetadata(Brushes.CornflowerBlue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
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
            DependencyProperty.Register(
                nameof(HeaderForeground),
                typeof(Brush), 
                typeof(NPage),
                new FrameworkPropertyMetadata(Brushes.WhiteSmoke, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Header Foreground.
        /// </summary>
        public Brush HeaderForeground
        {
            get { return (Brush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        #endregion

        #region WorkArea

        /// <summary>
        /// The WorkAreaProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty WorkAreaProperty =
            DependencyProperty.Register(
                nameof(WorkArea), 
                typeof(object), 
                typeof(NPage));
        /// <summary>
        /// Gets or sets WorkArea Content.
        /// </summary>
        public object WorkArea
        {
            get { return (object)GetValue(WorkAreaProperty); }
            set { SetValue(WorkAreaProperty, value); }
        }

        #endregion

        #endregion
    }
}
