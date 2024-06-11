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

            }
        }

        private void Search()
        {
            this.DataContext = null;
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

            this.DataContext = sample;

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
        }

        private void Save() 
        {

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
