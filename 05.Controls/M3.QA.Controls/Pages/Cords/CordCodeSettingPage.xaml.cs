#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using M3.QA.Models;
using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for CordCodeSettingPage.xaml
    /// </summary>
    public partial class CordCodeSettingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordCodeSettingPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<CordCode> cordCodes;
        private List<MCustomer> customers;
        private List<ProductType> productTypes;
        private List<YarnType> yarnTypes;
        private List<CordCodeDetail> items;

        #endregion

        #region Button Handlers

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3QAApp.Pages.GotoQAMainMenu();
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        #endregion

        #region Conbobox Handlers

        private void cbCordCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void cbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void cbProductTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void cbYarnTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        #endregion

        #region Private Methods

        private void LoadComboboxes()
        {
            cbCordCode.ItemsSource = null;
            cordCodes = CordCode.Gets(null).Value();
            this.InvokeAction(() =>
            {
                cbCordCode.ItemsSource = cordCodes;
                //cbCordCode.SelectedIndex = (null != cordCodes && cordCodes.Count > 0) ? 0 : -1;
                cbCordCode.SelectedIndex = -1;
            });

            cbCustomers.ItemsSource = null;
            customers = MCustomer.Gets(null).Value();
            this.InvokeAction(() =>
            {
                cbCustomers.ItemsSource = customers;
                //cbCustomers.SelectedIndex = (null != customers && customers.Count > 0) ? 0 : -1;
                cbCustomers.SelectedIndex = -1;
            });

            cbProductTypes.ItemsSource = null;
            productTypes = ProductType.Gets();
            this.InvokeAction(() =>
            {
                cbProductTypes.ItemsSource = productTypes;
                cbProductTypes.SelectedIndex = (null != productTypes && productTypes.Count > 0) ? 0 : -1;
                //cbProductTypes.SelectedIndex = -1;
            });

            cbYarnTypes.ItemsSource = null;
            yarnTypes = YarnType.Gets();
            this.InvokeAction(() =>
            {
                cbYarnTypes.ItemsSource = yarnTypes;
                //cbYarnTypes.SelectedIndex = (null != yarnTypes && yarnTypes.Count > 0) ? 0 : -1;
                cbYarnTypes.SelectedIndex = -1;
            });
        }

        private void Add()
        {

        }

        private void Edit()
        {

        }

        private void Search()
        {
            grid.ItemsSource = null;
            
            var cordCode = cbCordCode.SelectedItem as CordCode;
            string code =  (null != cordCode) ? cordCode.ItemCode : null;



            items = CordCodeDetail.Gets(code).Value();
            this.InvokeAction(() =>
            {
                grid.ItemsSource = items;
            });
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            LoadComboboxes();
        }

        #endregion
    }
}
