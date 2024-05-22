#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using NLib;
using NLib.Models;
using M3.QA.Models;

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for DIPSolutionProductionPage.xaml
    /// </summary>
    public partial class DIPSolutionProductionPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPSolutionProductionPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        public List<DIPSolutionProduction> searchs = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3QAApp.Pages.GotoQAMainMenu();
        }

        private void cmdSeach_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearSearch();
        }

        private void cmdExport_Click(object sender, RoutedEventArgs e)
        {
            var ctx = (null != sender && sender is Button) ? (sender as Button).DataContext : null;
            var item = (null != ctx) ? ctx as DIPSolutionProduction : null;
            Export(item);
        }

        #endregion

        #region TextBox Handlers

        private void txtLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                ClearSearch();
                e.Handled = true;
            }
        }

        #endregion

        #region DateTimePicker Handlers

        private void dtDateFrom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Search();
        }

        private void dtDateTo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Search();
        }

        #endregion

        #region ListView Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void Search()
        {
            DateTime? from = dtDateFrom.Value;
            DateTime? to = dtDateTo.Value;

            if (string.IsNullOrEmpty(txtLotNo.Text) && !from.HasValue && !to.HasValue)
            {
                M3QAApp.Windows.ShowMessage("กรุณาใส่ Lot No หรือ ระบุวันที่ที่ต้องการค้นหา");
                this.InvokeAction(() =>
                {
                    txtLotNo.FocusControl();
                });
                return;
            }

            string lotNo = txtLotNo.Text.Trim();

            grid.ItemsSource = null;
            searchs = DIPSolutionProduction.Gets(lotNo, from, to).Value();
            grid.ItemsSource = searchs;
        }

        private void ClearSearch()
        {
            txtLotNo.Text = string.Empty;
            dtDateTo.Value = DateTime.Today;
            dtDateFrom.Value = DateTime.Today;

            grid.ItemsSource = null;
            searchs = new List<DIPSolutionProduction>();
            grid.ItemsSource = searchs;

            this.InvokeAction(() =>
            {
                txtLotNo.FocusControl();
            });
        }

        private void Export(DIPSolutionProduction item)
        {
            if (null == item)
                return;

            COAService.COA5.Export(item);
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            ClearSearch();
        }

        #endregion
    }
}
