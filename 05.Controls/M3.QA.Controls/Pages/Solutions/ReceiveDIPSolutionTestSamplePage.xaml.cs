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
        private List<MCustomer> customers = null;
        private List<CordCode> cordCodes = null;

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

        private void ClearInputs()
        {
            this.DataContext = null;

            sample = new DIPSolutionSampleRecv();

            sample.SendDate = DateTime.Now;
            sample.ReceiveDate = DateTime.Now;
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
                    txtLotNo.FocusControl();
                });
                return;
            }

            if (!dtSend.Value.HasValue)
            {
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("กรุณาเลือก วันที่ Send");
                    dtSend.FocusControl();
                });
                return;
            }

            if (!dtRecv.Value.HasValue)
            {
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("กรุณาเลือก วันที่ Receive");
                    dtRecv.FocusControl();
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

            sample.MasterId = (null != code) ? code.MasterId : new int?();
            sample.SaveBy = (null != M3QAApp.Current.User) ? M3QAApp.Current.User.FullName : null;
            sample.SaveDate = DateTime.Now;

            var ret = DIPSolutionSampleRecv.Save(sample);
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

        #endregion

        #region Public Methods

        public void Setup()
        {
            LoadCustomers();
            ClearInputs();
        }

        #endregion
    }
}
