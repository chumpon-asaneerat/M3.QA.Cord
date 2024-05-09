#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Models;
using M3.QA.Models;

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for ReceiveDIPSolutionTestSamplePage.xaml
    /// </summary>
    public partial class ReceiveDIPSolutionTestSamplePage : UserControl
    {
        #region Constructor

        public ReceiveDIPSolutionTestSamplePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPSolutionSampleRecv sample = null;

        private List<CompoundType> compounds = null;
        private List<MCustomer> customers = null;
        private List<CordCode> cordCodes = null;

        public List<Models.Utils.P_SearchReceiveSolution> searchs = null;

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

        private void cmdSendBy_Click(object sender, RoutedEventArgs e)
        {
            if (null == sample)
                return;

            var win = M3QAApp.Windows.ChooseUser;
            win.Setup();
            if (win.ShowDialog() == true)
            {
                sample.SendBy = (null != win.User) ? win.User.FullName : null;
            }
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
            UpdateValidDate();
        }

        private void cbCompound_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update compound value.
            var compound = cbCompound.SelectedItem as CompoundType;
            if (null != sample)
            {
                sample.Compound = (null != compound) ? compound.Compound : null;
                // recalc valid date
                UpdateValidDate();
            }
        }

        #endregion

        #region DatePicker Handlers

        private void dtSend_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // recalc valid date
            UpdateValidDate();
        }

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

        private void LoadCustomers()
        {
            cbCustomers.ItemsSource = null;
            // get cord customers
            customers = MCustomer.Gets("Solution").Value();
            cbCustomers.ItemsSource = customers;
            if (null != customers && customers.Count > 0)
            {
                this.InvokeAction(() =>
                {
                    cbCustomers.SelectedIndex = 0;
                });
            }
        }

        private void LoadCompounds()
        {
            cbCompound.ItemsSource = null;
            
            compounds = CompoundType.Gets();
            
            cbCompound.ItemsSource = compounds;
        }

        private void LoadCordCodes(MCustomer customer)
        {
            cbCodes.ItemsSource = null;
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
        }

        private void UpdateValidDate()
        {
            var cordCode = cbCodes.SelectedItem as CordCode;
            DateTime? sendDT = dtSend.Value;
            int validdays = (null != cordCode && cordCode.ValidDays.HasValue) ? cordCode.ValidDays.Value : 0;
            DateTime? validDT = new DateTime?();

            if (sendDT.HasValue)
            {
                validDT = sendDT.Value.Date.AddDays(validdays);
            }

            var compound = cbCompound.SelectedItem as CompoundType;
            if (null != compound)
            {
                bool isRF = (!string.IsNullOrEmpty(compound.Compound) && compound.Compound == "RF");
                if (isRF)
                {
                    // Compound = RF - no valid date
                    dtValid.Value = new DateTime?();
                    dtValid.IsEnabled = false;
                }
                else
                {
                    // Compound = FINAL - valid date = Send DateTime + ValidDays
                    dtValid.Value = validDT;
                    dtValid.IsEnabled = true;
                }
            }
            else
            {
                dtValid.IsEnabled = false;
            }
        }

        private void ClearInputs()
        {
            this.DataContext = null;

            sample = new DIPSolutionSampleRecv();

            sample.SendDate = DateTime.Now;
            sample.ValidDate = new DateTime?();
            sample.ForecastFinishDate = DateTime.Now;

            this.DataContext = sample;

            // Change Customer selection index
            if (null != customers && customers.Count > 0)
            {
                this.InvokeAction(() =>
                {
                    cbCustomers.SelectedIndex = 0;
                });
            }
            // Change Compound selection index
            this.InvokeAction(() =>
            {
                cbCompound.SelectedIndex = -1;
            });
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
                    M3QAApp.Windows.ShowMessage("กรุณาเลือก DIP Solution");
                    cbCodes.FocusControl();
                });
                return;
            }

            if (string.IsNullOrWhiteSpace(sample.Compound))
            {
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("กรุณาบันทึก ค่า Compound");
                    cbCompound.FocusControl();
                });
                return;
            }

            // update compound value.
            var compound = cbCompound.SelectedItem as CompoundType;
            sample.Compound = (null != compound) ? compound.Compound : null;

            if (!dtSend.Value.HasValue)
            {
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("กรุณาเลือก วันที่ Send");
                    dtSend.FocusControl();
                });
                return;
            }

            bool isRF = (!string.IsNullOrEmpty(sample.Compound) && sample.Compound == "RF");

            if (!isRF && !dtValid.Value.HasValue)
            {
                // Compound = FINAL required valid date.
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("กรุณาเลือก วันที่ Valid");
                    dtValid.FocusControl();
                });
                return;
            }

            if (!dtForecast.Value.HasValue)
            {
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("กรุณาเลือก วันที่ Forcase Finished");
                    dtForecast.FocusControl();
                });
                return;
            }

            if (string.IsNullOrWhiteSpace(sample.SendBy))
            {
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("กรุณาเลือก Operator");
                });
                return;
            }

            var code = cbCodes.SelectedItem as CordCode;
            var validDate = (dtValid.Value.HasValue) ? dtValid.Value.Value.Date : new DateTime?();

            sample.MasterId = (null != code) ? code.MasterId : new int?();
            sample.ValidDate = (isRF) ? new DateTime?() : validDate;
            sample.SaveBy = (null != M3QAApp.Current.User) ? M3QAApp.Current.User.FullName : null;
            sample.SaveDate = DateTime.Now;

            var ret = DIPSolutionSampleRecv.Save(sample);
            if (null == ret || !ret.Ok)
            {
                var errmsg = (null != ret) ? ret.ErrMsg : null;
                M3QAApp.Windows.SaveFailed(errmsg);
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
            DateTime? dateTo = dtDateTo.Value;

            grid.ItemsSource = null;
            searchs = Models.Utils.P_SearchReceiveSolution.SearchByDate(dateFrom, dateTo).Value();
            grid.ItemsSource = searchs;

        }

        private void ClearSearch()
        {
            grid.ItemsSource = null;
            searchs = new List<Models.Utils.P_SearchReceiveSolution>();
            grid.ItemsSource = searchs;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            LoadCompounds();
            LoadCustomers();
            ClearInputs();

            dtDateFrom.Value = DateTime.Today;
            dtDateTo.Value = DateTime.Today;

            ClearSearch();
        }

        #endregion
    }
}
