#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using NLib;
using NLib.Models;
using M3.QA.Models;

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for CordProductionPage.xaml
    /// </summary>
    public partial class CordProductionPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordProductionPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        public List<CordProduction> searchs = null;

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

        private void cmdView_Click(object sender, RoutedEventArgs e)
        {
            var ctx = (null != sender && sender is Button) ? (sender as Button).DataContext : null;
            var item = (null != ctx) ? ctx as CordProduction : null;
            Edit(item);
        }

        private void cmdExport_Click(object sender, RoutedEventArgs e)
        {
            var ctx = (null != sender && sender is Button) ? (sender as Button).DataContext : null;
            var item = (null != ctx) ? ctx as CordProduction : null;
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
            searchs = CordProduction.Gets(lotNo, from, to).Value();
            grid.ItemsSource = searchs;
        }

        private void ClearSearch()
        {
            txtLotNo.Text = string.Empty;
            dtDateTo.Value = DateTime.Today;
            dtDateFrom.Value = DateTime.Today;

            grid.ItemsSource = null;
            searchs = new List<CordProduction>();
            grid.ItemsSource = searchs;

            this.InvokeAction(() =>
            {
                txtLotNo.FocusControl();
            });
        }

        private void Edit(CordProduction item)
        {
            if (null == item)
                return;
            var win = M3QAApp.Windows.CordProductionTestView;
            win.Setup(item);
            if (win.ShowDialog() == true)
            {

            }
        }

        private void Export(CordProduction item)
        {
            if (null == item)
                return;
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
