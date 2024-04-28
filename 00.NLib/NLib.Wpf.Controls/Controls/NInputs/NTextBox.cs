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
using NLib;

#endregion

namespace NLib.Wpf.Controls
{
    /// <summary>
    /// The Watermark Image Alignment Enum
    /// </summary>
    public enum WatermarkImageAlignment { Left, Right }

    /// <summary>
    /// The NTextBox Control
    /// </summary>
    public class NTextBox : NInputControlBase
    {
        #region Constructor

        static NTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NTextBox), 
                new FrameworkPropertyMetadata(typeof(NTextBox)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NTextBox() : base() { }

        #endregion

        #region Internal Variables

        private TextBox ctrl;

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ctrl = null;
            var obj = GetTemplateChild("ctrl");
            if (null != obj && obj is TextBox)
            {
                ctrl = (TextBox)obj;
                if (null != ctrl)
                {
                    ctrl.TextChanged += Ctrl_TextChanged;
                }
            }
        }

        /// <summary>
        /// Focus internal control.
        /// </summary>
        public override void FocusControl() 
        {
            if (null != ctrl) ctrl.Focus();
        }

        private void Ctrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            RaiseTextChanged(e);
        }

        #endregion

        #region Private Methods

        private void RaiseTextChanged(TextChangedEventArgs e)
        {
            var evt = new TextChangedEventArgs(TextChangedEvent, e.UndoAction, e.Changes);
            RaiseEvent(evt);
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
                typeof(NTextBox),
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

        #region Text

        /// <summary>
        /// The TextProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text), 
                typeof(string), 
                typeof(NTextBox),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Text.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
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
                typeof(NTextBox),
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

        #region TextWrapping

        /// <summary>
        /// The TextWrappingProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty TextWrappingProperty =
            DependencyProperty.Register(nameof(TextWrapping), 
                typeof(TextWrapping), 
                typeof(NTextBox),
                new FrameworkPropertyMetadata(TextWrapping.NoWrap, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets TextWrapping.
        /// </summary>
        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }

        #endregion

        #region AcceptsReturn

        /// <summary>
        /// The AcceptsReturnProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty AcceptsReturnProperty =
            DependencyProperty.Register(
                nameof(AcceptsReturn), 
                typeof(bool), 
                typeof(NTextBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets AcceptsReturn.
        /// </summary>
        public bool AcceptsReturn
        {
            get { return (bool)GetValue(AcceptsReturnProperty); }
            set { SetValue(AcceptsReturnProperty, value); }
        }

        #endregion

        #region AcceptsTab

        /// <summary>
        /// The AcceptsTabProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty AcceptsTabProperty =
            DependencyProperty.Register(
                nameof(AcceptsTab), 
                typeof(bool), 
                typeof(NTextBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets AcceptsTab.
        /// </summary>
        public bool AcceptsTab
        {
            get { return (bool)GetValue(AcceptsTabProperty); }
            set { SetValue(AcceptsTabProperty, value); }
        }

        #endregion

        #region IsReadOnly

        /// <summary>
        /// The IsReadOnlyProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(
                nameof(IsReadOnly),
                typeof(bool),
                typeof(NTextBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets AcceptsTab.
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
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
                typeof(NTextBox),
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
                typeof(NTextBox),
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
                typeof(NTextBox),
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
                typeof(NTextBox),
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
                typeof(NTextBox),
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
                typeof(NTextBox),
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
                typeof(NTextBox),
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
                typeof(NTextBox),
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

        #region Event Handlers

        /// <summary>
        /// The TextChangedEvent Route Event.
        /// </summary>
        public static readonly RoutedEvent TextChangedEvent = EventManager.RegisterRoutedEvent(
            "TextChanged",
            RoutingStrategy.Bubble,
            typeof(TextChangedEventHandler),
            typeof(NTextBox));
        /// <summary>
        /// The TextChanged event Handler.
        /// </summary>
        public event TextChangedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }

        #endregion
    }
}
