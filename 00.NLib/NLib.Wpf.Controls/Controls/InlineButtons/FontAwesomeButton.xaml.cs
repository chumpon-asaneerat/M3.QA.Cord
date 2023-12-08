#region Using

using System;
using System.Windows;
using System.Windows.Controls;

using NLib;

#endregion

namespace NLib.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for FontAwesomeButton.xaml
    /// </summary>
    public partial class FontAwesomeButton : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FontAwesomeButton()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        #region Button Handlers

        private void cmd_Click(object sender, RoutedEventArgs e)
        {
            if (null != Click)
            {
                this.InvokeAction(() =>
                {
                    e.Source = this; // Change source.
                    Click(this, e);
                });
            }
        }

        #endregion

        #endregion

        #region Public Properties

        #region IconType

        /// <summary>
        /// The IconTypeProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IconTypeProperty = 
            DependencyProperty.Register("IconType", typeof(FontAwesomeIcon), typeof(FontAwesomeButton));
        /// <summary>
        /// Gets or sets Font Awesome Icon Type.
        /// </summary>
        public FontAwesomeIcon IconType
        {
            get { return (FontAwesomeIcon)GetValue(IconTypeProperty); }
            set { SetValue(IconTypeProperty, value); }
        }

        #endregion

        #region Text

        /// <summary>
        /// The TextProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FontAwesomeButton));
        /// <summary>
        /// Gets or sets Inline Button Text.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        #endregion

        #region Public Events

        /// <summary>
        /// The Click Event.
        /// </summary>
        public event RoutedEventHandler Click;

        #endregion
    }
}
