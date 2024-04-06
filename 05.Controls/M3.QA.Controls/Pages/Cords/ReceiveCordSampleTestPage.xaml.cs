#region Using

using M3.QA.Models;
using NLib;
using NLib.Data.Design;
using NLib.Models;
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

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for ReceiveCordTestSamplePage.xaml
    /// </summary>
    public partial class ReceiveCordTestSamplePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReceiveCordTestSamplePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private CordTestSampleRecv sample = new CordTestSampleRecv();
        private List<MCustomer> customers = null;
        private List<CordCode> cordCodes = null;
        private List<DIPMC> MCs = null;

        public List<Models.Utils.P_SearchReceiveCord> searchs = null;

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

        private void cmdSeach_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearSearch();
        }

        #endregion

        #region Combobox Handlers

        private void cbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = cbCustomers.SelectedItem as MCustomer;
            if (null == customer) return;
            LoadCordCodes(customer);
        }

        private void cbCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var code = cbCodes.SelectedItem as CordCode;
            UpdateProductType(code);
        }

        #endregion

        #region DateTimePicker Handlers

        private void dtDateFrom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //Search();
        }

        private void dtDateTo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //Search();
        }

        #endregion

        #region ListView Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void LoadMC()
        {
            cbDIPMC.ItemsSource = null;
            MCs = DIPMC.Gets();
            cbDIPMC.ItemsSource = MCs;
        }

        private void LoadCustomers()
        {
            cbCustomers.ItemsSource = null;

            // get cord customers
            customers = MCustomer.Gets("Cord").Value();
            cbCustomers.ItemsSource = customers;
            if (null != customers && customers.Count > 0)
            {
                this.InvokeAction(() =>
                {
                    cbCustomers.SelectedIndex = 0;
                });
            }
            else
            {
                LoadCordCodes(null);
            }
        }

        private void LoadCordCodes(MCustomer customer)
        {
            cbCodes.ItemsSource = null;
            CheckSPPanels(null);

            if (null == customer) return;
            // get cord code by customer
            cordCodes = CordCode.Gets(customer.Customer).Value();
            cbCodes.ItemsSource = cordCodes;
            if (null != cordCodes && cordCodes.Count > 0) 
            {
                this.InvokeAction(() =>
                {
                    cbCodes.SelectedIndex = 0;
                });
            }
            else
            {
                UpdateProductType(null);
            }
        }

        private void UpdateProductType(CordCode code)
        {
            rbTwist.IsChecked = false;
            rbDIP.IsChecked = false;
            cbDIPMC.IsEnabled = false;
            cbDIPMC.SelectedIndex = -1;
            CheckSPPanels(code);

            if (null == code) 
                return;

            if (code.ProductType == "Twist")
            {
                // Twist
                rbTwist.IsChecked = true;
            }
            else
            {
                // DIP need to select MC
                rbDIP.IsChecked = true;
                cbDIPMC.IsEnabled = true;
                if (null != MCs && MCs.Count > 0)
                {
                    this.InvokeAction(() =>
                    {
                        cbDIPMC.SelectedIndex = 0;
                    });
                }
            }
        }

        private void CheckSPPanels(CordCode code)
        {
            var state = Visibility.Collapsed;

            p1.Visibility = state;
            p2.Visibility = state;
            p3.Visibility = state;
            p4.Visibility = state;
            p5.Visibility = state;
            p6.Visibility = state;
            p7.Visibility = state;

            if (null == code) return;
            if (code.NoTestCH >= 1) p1.Visibility = Visibility.Visible;
            if (code.NoTestCH >= 2) p2.Visibility = Visibility.Visible;
            if (code.NoTestCH >= 3) p3.Visibility = Visibility.Visible;
            if (code.NoTestCH >= 4) p4.Visibility = Visibility.Visible;
            if (code.NoTestCH >= 5) p5.Visibility = Visibility.Visible;
            if (code.NoTestCH >= 6) p6.Visibility = Visibility.Visible;
            if (code.NoTestCH >= 7) p7.Visibility = Visibility.Visible;
        }

        private void ClearInputs()
        {
            this.DataContext = null;

            sample = new CordTestSampleRecv();
            sample.ReceiveDate = DateTime.Now; // Set default Now.

            this.DataContext = sample;

            // Change Customer selection index
            if (null != customers && customers.Count > 0)
            {
                this.InvokeAction(() =>
                {
                    cbCustomers.SelectedIndex = 0;
                });
            }
        }

        private void Save()
        {
            if (null == sample)
            {
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("Instance is null.");
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(sample.LotNo))
            {
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("กรุณาบันทึก Lot No");
                    txtLotNo.FocusControl();
                });
                return;
            }

            if (null == cbCustomers.SelectedItem)
            {
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("กรุณาเลือก Customer");
                    cbCustomers.FocusControl();
                });
                return;
            }

            if (null == cbCodes.SelectedItem)
            {
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("กรุณาเลือก Cord Code");
                    cbCodes.FocusControl();
                });
                return;
            }

            if (!dtRecv.Value.HasValue)
            {
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("กรุณาเลือก วันที่");
                    dtRecv.FocusControl();
                });
                return;
            }

            if (rbDIP.IsChecked == true && null == cbDIPMC.SelectedItem)
            {
                M3QAApp.Windows.ShowMessage("กรุณาเลือก DIP M/C");
                this.InvokeAction(() =>
                {
                    cbDIPMC.FocusControl();
                });
                return;
            }

            var code = cbCodes.SelectedItem as CordCode;
            var dip = cbDIPMC.SelectedItem as DIPMC;

            sample.MasterId = (null != code) ? code.MasterId : new int?();
            sample.DIPMC = (null != dip) ? dip.MCName : null;
            sample.ReceiveBy = (null != M3QAApp.Current.User) ? M3QAApp.Current.User.FullName : null;

            var ret = CordTestSampleRecv.Save(sample);
            if (null == ret || !ret.Ok)
            {
                M3QAApp.Windows.SaveFailed();
            }
            else
            {
                M3QAApp.Windows.SaveSuccess();
                ClearInputs();
            }
        }

        private void Search()
        {
            DateTime? dateFrom = dtDateFrom.Value;
            DateTime? dateTo = dtDateFrom.Value;

            grid.ItemsSource = null;
            searchs = Models.Utils.P_SearchReceiveCord.SearchByDate(dateFrom, dateTo).Value();
            grid.ItemsSource = searchs;

        }

        private void ClearSearch()
        {
            grid.ItemsSource = null;
            searchs = new List<Models.Utils.P_SearchReceiveCord>();
            grid.ItemsSource = searchs;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            LoadMC();
            LoadCustomers();
            ClearInputs();

            dtDateFrom.Value = DateTime.Today;
            dtDateTo.Value = DateTime.Today;

            ClearSearch();
        }

        #endregion
    }
}
