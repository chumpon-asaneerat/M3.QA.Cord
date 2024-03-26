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
            //UpdateProductType(code);
        }

        #endregion

        #region Private Methods

        private void LoadCustomers()
        {
            cbCustomers.ItemsSource = null;
            // get cord customers
            customers = MCustomer.Gets("Solution").Value();
            cbCustomers.ItemsSource = customers;
            if (null != customers && customers.Count > 1)
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
            if (null != cordCodes && cordCodes.Count > 1)
            {
                this.InvokeAction(() =>
                {
                    cbCodes.SelectedIndex = 0;
                });
            }
        }

        private void ClearInputs()
        {

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
