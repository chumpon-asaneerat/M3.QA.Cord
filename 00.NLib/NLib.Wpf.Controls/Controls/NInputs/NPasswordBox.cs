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
    /// The NPasswordBox Control
    /// </summary>
    public class NPasswordBox : NInputControlBase
    {
        #region Constructor

        static NPasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NPasswordBox),
                new FrameworkPropertyMetadata(typeof(NPasswordBox)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NPasswordBox() : base() { }

        #endregion

        #region Internal Variables

        private PasswordBox ctrl;

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ctrl = null;
            var obj = GetTemplateChild("ctrl");
            if (null != obj && obj is PasswordBox)
            {
                ctrl = (PasswordBox)obj;
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
                typeof(NPasswordBox),
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

        #region Password

        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string Password 
        {
            get { return (null != ctrl) ? ctrl.Password : null; }
            set 
            { 
                if (null != ctrl)
                {
                    ctrl.Password = value;
                }
            } 
        }

        #endregion

        #region TextAlignment

        /// <summary>
        /// The TextAlignmentProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register(
                nameof(TextAlignment),
                typeof(TextAlignment),
                typeof(NPasswordBox),
                new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets TextAlignment.
        /// </summary>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        #endregion

        #region Watermark Image Source

        /// <summary>
        /// The WatermarkImageSourceProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty WatermarkImageSourceProperty =
            DependencyProperty.Register(
                nameof(WatermarkImageSource),
                typeof(ImageSource),
                typeof(NPasswordBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Watermark Image Source.
        /// </summary>
        public ImageSource WatermarkImageSource
        {
            get { return (ImageSource)GetValue(WatermarkImageSourceProperty); }
            set { SetValue(WatermarkImageSourceProperty, value); }
        }

        #endregion

        #region Watermark Width

        /// <summary>
        /// The WatermarkImageWidthProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty WatermarkImageWidthProperty =
            DependencyProperty.Register(
                nameof(WatermarkImageWidth),
                typeof(double),
                typeof(NPasswordBox),
                new FrameworkPropertyMetadata((double)25, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Watermark Image Width.
        /// </summary>
        public double WatermarkImageWidth
        {
            get { return (double)GetValue(WatermarkImageWidthProperty); }
            set { SetValue(WatermarkImageWidthProperty, value); }
        }

        #endregion

        #region Watermark Height

        /// <summary>
        /// The WatermarkImageHeightProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty WatermarkImageHeightProperty =
            DependencyProperty.Register(
                nameof(WatermarkImageHeight),
                typeof(double),
                typeof(NPasswordBox),
                new FrameworkPropertyMetadata((double)25, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Watermark Image Height.
        /// </summary>
        public double WatermarkImageHeight
        {
            get { return (double)GetValue(WatermarkImageHeightProperty); }
            set { SetValue(WatermarkImageHeightProperty, value); }
        }

        #endregion

        #region Watermark Image Margin

        /// <summary>
        /// The WatermarkMarginProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty WatermarkMarginProperty =
            DependencyProperty.Register(
                nameof(WatermarkMargin),
                typeof(Thickness),
                typeof(NPasswordBox),
                new FrameworkPropertyMetadata(new Thickness(10, 1, 10, 1), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Watermark Image Margin.
        /// </summary>
        public Thickness WatermarkMargin
        {
            get { return (Thickness)GetValue(WatermarkMarginProperty); }
            set { SetValue(WatermarkMarginProperty, value); }
        }

        #endregion

        #region Watermark Image Alignment

        /// <summary>
        /// The WatermarkImageAlignmentProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty WatermarkImageAlignmentProperty =
            DependencyProperty.Register(
                nameof(WatermarkImageAlignment),
                typeof(WatermarkImageAlignment),
                typeof(NPasswordBox),
                new FrameworkPropertyMetadata(WatermarkImageAlignment.Left, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Watermark Foreground.
        /// </summary>
        public WatermarkImageAlignment WatermarkImageAlignment
        {
            get { return (WatermarkImageAlignment)GetValue(WatermarkImageAlignmentProperty); }
            set { SetValue(WatermarkImageAlignmentProperty, value); }
        }

        #endregion

        #region Watermark Foreground

        /// <summary>
        /// The WatermarkForegroundProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty WatermarkForegroundProperty =
            DependencyProperty.Register(
                nameof(WatermarkForeground),
                typeof(Brush),
                typeof(NPasswordBox),
                new FrameworkPropertyMetadata(Brushes.DimGray, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Watermark Foreground.
        /// </summary>
        public Brush WatermarkForeground
        {
            get { return (Brush)GetValue(WatermarkForegroundProperty); }
            set { SetValue(WatermarkForegroundProperty, value); }
        }

        #endregion

        #region Watermark Margin

        /// <summary>
        /// The WatermarkImageMarginProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty WatermarkImageMarginProperty =
            DependencyProperty.Register(
                nameof(WatermarkImageMargin),
                typeof(Thickness),
                typeof(NPasswordBox),
                new FrameworkPropertyMetadata(new Thickness(5, 1, 5, 1), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Watermark Image Margin.
        /// </summary>
        public Thickness WatermarkImageMargin
        {
            get { return (Thickness)GetValue(WatermarkImageMarginProperty); }
            set { SetValue(WatermarkImageMarginProperty, value); }
        }

        #endregion

        #region Watermark Text

        /// <summary>
        /// The WatermarkTextProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty WatermarkTextProperty =
            DependencyProperty.Register(
                nameof(WatermarkText),
                typeof(string),
                typeof(NPasswordBox),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Watermark Text.
        /// </summary>
        public string WatermarkText
        {
            get { return (string)GetValue(WatermarkTextProperty); }
            set { SetValue(WatermarkTextProperty, value); }
        }

        #endregion

        #endregion
    }
}
