#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib.Models;
using M3.QA.Models;
using NLib;

#endregion

namespace M3.QA.Windows
{
    /// <summary>
    /// Interaction logic for CordCodeSettingEditorWindow.xaml
    /// </summary>
    public partial class CordCodeSettingEditorWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordCodeSettingEditorWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<CordCode> cordCodes;
        private List<MCustomer> customers;
        private List<ProductType> productTypes;
        private List<YarnType> yarnTypes;
        private List<CoaReportType> coaReports;

        private CordCodeDetail item;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Update value from controls to item.
            UpdateControlsToItem();

            // Check required
            if (string.IsNullOrEmpty(item.Customer) && string.IsNullOrEmpty(item.NewCustomer))
            {
                string msg = string.Empty;
                msg += "Save Failed" + Environment.NewLine + "บันทึกข้อมูลไม่สำเร็จ";
                msg += Environment.NewLine;
                msg += "Plase enter Customer !";
                M3QAApp.Windows.ShowMessage(msg);

                return;
            }

            // Set current user
            var user = M3QAApp.Current.User;
            NDbResult ret;

            ret = CordCodeDetail.Save(item, user);
            if (null == ret || !ret.Ok)
            {
                // error.
                string msg = string.Empty;
                msg += "Save Failed" + Environment.NewLine + "บันทึกข้อมูลไม่สำเร็จ";
                msg += Environment.NewLine;
                msg += (null != ret) ? ret.ErrMsg : "Unknown Error!!";
                M3QAApp.Windows.ShowMessage(msg);

                return;
            }
            else
            {
                // success
                M3QAApp.Windows.SaveSuccess();

                DialogResult = true;
            }
        }

        #endregion

        #region Combobox Handlers

        private void cbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != cbCustomers.SelectedItem)
            {
                txtNewCustomer.Text = string.Empty;
            }
        }

        #endregion

        #region TextBox Handlers

        private void txtNewCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewCustomer.Text)) 
            {
                cbCustomers.SelectedIndex = -1; // Reset.
            }
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
                //cbCordCode.SelectedIndex = -1;
            });

            cbCustomers.ItemsSource = null;
            customers = MCustomer.Gets(null).Value();
            this.InvokeAction(() =>
            {
                cbCustomers.ItemsSource = customers;
                //cbCustomers.SelectedIndex = (null != customers && customers.Count > 0) ? 0 : -1;
                //cbCustomers.SelectedIndex = -1;
            });

            cbProductTypes.ItemsSource = null;
            productTypes = ProductType.Gets();
            this.InvokeAction(() =>
            {
                cbProductTypes.ItemsSource = productTypes;
                //cbProductTypes.SelectedIndex = (null != productTypes && productTypes.Count > 0) ? 0 : -1;
                //cbProductTypes.SelectedIndex = -1;
            });

            cbYarnTypes.ItemsSource = null;
            yarnTypes = YarnType.Gets();
            this.InvokeAction(() =>
            {
                cbYarnTypes.ItemsSource = yarnTypes;
                //cbYarnTypes.SelectedIndex = (null != yarnTypes && yarnTypes.Count > 0) ? 0 : -1;
                //cbYarnTypes.SelectedIndex = -1;
            });

            cbCoaReports.ItemsSource = null;
            coaReports = CoaReportType.Gets();
            this.InvokeAction(() =>
            {
                cbCoaReports.ItemsSource = coaReports;
                //cbCoaReports.SelectedIndex = (null != coaReports && coaReports.Count > 0) ? 0 : -1;
                //cbCoaReports.SelectedIndex = -1;
            });
        }

        private void SyncItemToControls()
        {
            this.InvokeAction(() =>
            {
                int idx;
                idx =  (null != cordCodes) ? cordCodes.FindIndex(x =>
                {
                    return string.Compare(x.ItemCode, item.ItemCode, true) == 0;
                }) : -1;
                cbCordCode.SelectedIndex = idx;

                idx = (null != customers) ? customers.FindIndex(x =>
                {
                    return string.Compare(x.Customer, item.Customer, true) == 0;
                }) : -1;
                cbCustomers.SelectedIndex = idx;

                idx = (null != productTypes) ? productTypes.FindIndex(x =>
                {
                    return string.Compare(x.Text, item.ProductType, true) == 0;
                }) : -1;
                cbProductTypes.SelectedIndex = idx;

                idx = (null != yarnTypes) ? yarnTypes.FindIndex(x =>
                {
                    return string.Compare(x.Text, item.YarnType, true) == 0;
                }) : -1;
                cbYarnTypes.SelectedIndex = idx;

                idx = (null != coaReports) ? coaReports.FindIndex(x =>
                {
                    return (x.CoaNo == item.CoaNo) && (string.Compare(x.FMQC, item.FMQC, true) == 0);
                }) : -1;
                cbCoaReports.SelectedIndex = idx;
            });
        }

        private void UpdateControlsToItem()
        {
            if (null == item)
                return;
            if (cbCordCode.SelectedIndex != -1)
            {
                var x = cbCordCode.SelectedItem as CordCode;
                item.ItemCode = x.ItemCode;
            }
            if (cbCustomers.SelectedIndex != -1)
            {
                var x = cbCustomers.SelectedItem as MCustomer;
                item.Customer = x.Customer;
            }
            if (cbProductTypes.SelectedIndex != -1)
            {
                var x = cbProductTypes.SelectedItem as ProductType;
                item.ProductType = x.Text;
            }
            if (cbYarnTypes.SelectedIndex != -1)
            {
                var x = cbYarnTypes.SelectedItem as YarnType;
                item.YarnType = x.Text;
            }
            if (cbCoaReports.SelectedIndex != -1)
            {
                var x = cbCoaReports.SelectedItem as CoaReportType;
                item.CoaNo = x.CoaNo;
                item.FMQC = x.FMQC;
            }
        }

        #endregion

        #region Public Methods

        public void Setup(CordCodeDetail inst)
        {
            this.DataContext = null;
            LoadComboboxes();

            item = inst;
            if (null != item) 
            {
                SyncItemToControls();
            }

            this.DataContext = item;
        }

        #endregion
    }
}
