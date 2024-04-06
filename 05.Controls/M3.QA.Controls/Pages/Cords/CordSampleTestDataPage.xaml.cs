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
    /// Interaction logic for CordSampleTestDataPage.xaml
    /// </summary>
    public partial class CordSampleTestDataPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordSampleTestDataPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private CordSampleTestData item = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3QAApp.Pages.GotoQAMainMenu();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
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

            var ret = CordSampleTestData.GetByLotNo(sLotNo.Trim());
            if (null == ret || !ret.Ok)
            {
                string msg = string.Empty;
                msg += "Lot No not found on Received Test Data";

                M3QAApp.Windows.ShowMessage(msg);
                return;
            }
            // Set current item and binding
            this.DataContext = null;

            item = ret.Value();
            this.DataContext = item;

            //pgrid.SelectedObject = item;
        }

        private void Clear()
        {
            this.DataContext = null;

            txtLotNo.Text = string.Empty;
            item = new CordSampleTestData();
            
            this.DataContext = item;

            //pgrid.SelectedObject = item;
            this.InvokeAction(() =>
            {
                txtLotNo.FocusControl();
            });
        }

        private void Save()
        {
            if (null != item)
            {
                // Set current user
                var user = M3QAApp.Current.User;
                NDbResult ret;
                ret = CordSampleTestData.Save(item, user);
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
                }

                Clear();
            }
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
