#region Using

using M3.QA.Models;
using NLib;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for DIPSolutionSampleTestDataPage.xaml
    /// </summary>
    public partial class DIPSolutionSampleTestDataPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPSolutionSampleTestDataPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPSolutionSampleTestData item;

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
                Clear();
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void Search()
        {
            string sLotNo = txtLotNo.Text;
            if (string.IsNullOrEmpty(sLotNo))
            {
                M3QAApp.Windows.ShowMessage("กรุณาใส่ Lot No");
                this.InvokeAction(() =>
                {
                    txtLotNo.FocusControl();
                });
                return;
            }

            // Set current item and binding
            this.DataContext = null;

            var ret = DIPSolutionSampleTestData.GetByLotNo(sLotNo.Trim());
            if (null == ret)
            {
                string msg = string.Empty;
                msg += "Lot No not found on Received Test Data";

                M3QAApp.Windows.ShowMessage(msg);
                return;
            }

            item = ret;
            this.DataContext = item;

            //pgrid.SelectedObject = item;
        }

        private void Clear()
        {
            this.DataContext = null;

            txtLotNo.Text = string.Empty;
            item = new DIPSolutionSampleTestData();

            this.DataContext = item;

            //pgrid.SelectedObject = item;
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
            Clear();
        }

        #endregion
    }
}
