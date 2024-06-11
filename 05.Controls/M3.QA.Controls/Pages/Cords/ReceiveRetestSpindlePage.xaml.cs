#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using NLib;
using NLib.Models;
using M3.QA.Models;
using System.Windows.Media.Effects;
using System.Windows.Media;

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for ReceiveRetestSpindlePage.xaml
    /// </summary>
    public partial class ReceiveRetestSpindlePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReceiveRetestSpindlePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private CordSampleTestData sample = null;
        private List<Models.Utils.P_GetActiveSPByLot> items = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3QAApp.Pages.GotoQAMainMenu();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void cmdEnable_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var ctx = btn.DataContext as Models.Utils.P_GetActiveSPByLot;
            if (null == ctx) return;
            ctx.EnableRetest();
        }

        private void cmdDisable_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var ctx = btn.DataContext as Models.Utils.P_GetActiveSPByLot;
            if (null == ctx) return;
            ctx.CancelRetest();
        }

        #endregion

        #region Textbox Handlers

        private void txtLotNo_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                ClearInputs();
                e.Handled = true;
            }
        }

        #endregion

        #region Border Handlers (for Retest1 only)

        private void Border_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var border = sender as Border;
            if (null == border) return;
            if (border.Visibility != Visibility.Visible) return;

            TextBox textBox = FindControl<TextBox>(border, typeof(TextBox));
            if (null != textBox)
            {
                this.InvokeAction(() =>
                {
                    textBox.SelectAll();
                    textBox.Focus();
                });
            }
        }

        #endregion

        #region Private Methods

        public static T FindControl<T>(UIElement parent, Type targetType) 
            where T : FrameworkElement
        {
            if (parent == null) return null;
            if (parent.GetType() == targetType) return (T)parent;

            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (!(child is UIElement)) continue;

                var elem = (UIElement)child;
                T result = FindControl<T>(elem, targetType);
                if (result != null) return result;
            }
            return null;
        }

        private void Search()
        {
            items = null;
            this.DataContext = null;
            spindles.ItemsSource = null;

            string lotNo;
            try
            {
                lotNo = (string.IsNullOrEmpty(txtLotNo.Text)) ? null : txtLotNo.Text.Trim();
            }
            catch 
            { 
                lotNo = null; 
            }

            sample = CordSampleTestData.GetByLotNo(lotNo).Value();
            items = (null != sample) ? Models.Utils.P_GetActiveSPByLot.Gets(lotNo).Value() : null;

            this.DataContext = sample;
            spindles.ItemsSource = items;

            if (null == sample)
            {
                string msg = string.Empty;
                msg += "Lot No not found on Received Test Data";

                M3QAApp.Windows.ShowMessage(msg);
            }
        }

        private void ClearInputs()
        {
            this.DataContext = null;
            txtLotNo.Text = string.Empty;
            sample = null;
            this.DataContext = sample;

            this.InvokeAction(() => 
            {
                txtLotNo.FocusControl();
            });
        }

        private void Save() 
        {

        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            ClearInputs();
        }

        #endregion
    }
}
