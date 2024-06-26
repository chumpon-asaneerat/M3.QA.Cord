#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using NLib;
using NLib.Models;
using M3.QA.Models;
using NLib.Serial.Terminals;
using System.Reflection;
using NLib.Serial;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for DIPSolutionRecordPHMeterPage.xaml
    /// </summary>
    public partial class DIPSolutionRecordPHMeterPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPSolutionRecordPHMeterPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private PHMeterConfig _cfg = null;

        private List<Models.Utils.M_GetSolutionList> solutions = null;
        private List<CompoundType> compounds = null;

        private DIPSolitionPHTestData item;

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PHMeterTerminal.Instance.OnRx += Instance_OnRx;
            this.InvokeAction(() =>
            {
                ChangeDevice();
            });
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            StopDevice();
            PHMeterTerminal.Instance.OnRx -= Instance_OnRx;
        }

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
                //Search();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                Clear();
                e.Handled = true;
            }
        }

        #endregion

        #region RadioButton Handlers

        private void TestMode_Checked(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region CheckBox Handlers

        private void chkManual_Checked(object sender, RoutedEventArgs e)
        {
            txtPH.IsReadOnly = false;
            txtPH.Text = string.Empty;

            txtTemp.IsReadOnly = false;
            txtTemp.Text = string.Empty;
        }

        private void chkManual_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPH.IsReadOnly = true;
            txtPH.Text = string.Empty;

            txtTemp.IsReadOnly = true;
            txtTemp.Text = string.Empty;

            this.InvokeAction(() =>
            {
                ChangeDevice();
            });
        }

        #endregion

        #region Device Event Handlers

        private void Instance_OnRx(object sender, EventArgs e)
        {
            UpdateTextBox();
        }

        #endregion

        #region Private Methods

        private void InitComboboxes()
        {
            cbCompound.ItemsSource = null;
            compounds = CompoundType.Gets();
            cbCompound.ItemsSource = compounds;

            cbDIPSolution.ItemsSource = null;
            solutions = Models.Utils.M_GetSolutionList.Gets().Value();
            cbDIPSolution.ItemsSource = solutions;
        }

        private void CheckConfig()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            _cfg = PHMeterConfig.GetConfig();
            if (null != _cfg)
            {
                med.Info("JIK6CAB Config Loaded");
            }
            else
            {
                med.Info("JIK6CAB Config is null.");
            }
        }

        private void ChangeDevice()
        {
            // Disconnect first
            PHMeterTerminal.Instance.Disconnect();
            txtPH.Text = string.Empty;
            txtTemp.Text = string.Empty;

            var dev = PHMeterTerminal.Instance as ISerialDevice;
            dev.Config = (null != _cfg) ? _cfg.Device1 : null;

            if (chkManual.IsChecked == false)
            {
                // Connect after load config
                if (null != _cfg)
                {
                    PHMeterTerminal.Instance.Connect();
                }
            }
        }

        private void StopDevice()
        {
            PHMeterTerminal.Instance.Disconnect();
            txtPH.Text = string.Empty;
            txtTemp.Text = string.Empty;
        }

        private void UpdateTextBox()
        {
            if (null == item) 
                return;
            this.InvokeAction(() =>
            {
                if (chkManual.IsChecked == false)
                {
                    var val = PHMeterTerminal.Instance.Value;
                    txtPH.Text = val.pH.ToString("n2");
                    txtTemp.Text = val.TempC.ToString("n1");
                }
            });
        }

        private void Clear()
        {
            this.DataContext = null;

            txtLotNo.Text = string.Empty;

            if (null != cbDIPSolution.ItemsSource && null != solutions && solutions.Count > 0)
                cbDIPSolution.SelectedIndex = 0;
            else cbDIPSolution.SelectedIndex = -1;

            if (null != cbCompound.ItemsSource && null != compounds && compounds.Count > 0)
                cbCompound.SelectedIndex = 0;
            else cbCompound.SelectedIndex = -1;

            item = new DIPSolitionPHTestData();

            this.DataContext = item;

            //pgrid.SelectedObject = item;
            this.InvokeAction(() =>
            {
                txtLotNo.FocusControl();
            });
        }

        private void Save()
        {
            // Check Lot No.
            if (string.IsNullOrEmpty(txtLotNo.Text))
            {
                M3QAApp.Windows.ShowMessage("กรุณาใส่ Lot No");
                this.InvokeAction(() =>
                {
                    txtLotNo.FocusControl();
                });
                return;
            }

            // Check Item Code value.
            var solution = cbDIPSolution.SelectedItem as Models.Utils.M_GetSolutionList;
            if (null == solution)
            {
                M3QAApp.Windows.ShowMessage("กรุณาใส่ เลือกประเภท DIP Solution");
                this.InvokeAction(() =>
                {
                    cbDIPSolution.FocusControl();
                });
                return;
            }

            // Check compound value.
            var compound = cbCompound.SelectedItem as CompoundType;
            if (null == compound)
            {
                M3QAApp.Windows.ShowMessage("กรุณาใส่ เลือกประเภท Compound");
                this.InvokeAction(() =>
                {
                    cbCompound.FocusControl();
                });
                return;
            }

            if (null != item)
            {
                // Set Lot/MasterId/ItemCode/Compound
                item.LotNo = txtLotNo.Text;
                item.MasterId = solution.MasterId;
                item.ItemCode = solution.ItemCode;
                item.Compounds = compound.Compound;

                // Set pH/Temp
                decimal ph;
                item.Ph = decimal.Zero;
                if (decimal.TryParse(txtPH.Text, out ph))
                {
                    item.Ph = ph;
                }

                decimal temp;
                item.Temperature = decimal.Zero;
                if (decimal.TryParse(txtTemp.Text, out temp))
                {
                    item.Temperature = temp;
                }

                // Set TestType/LinkType
                item.TestType = (rbRetest.IsChecked.HasValue && rbRetest.IsChecked.Value) ? "RETEST" : "NORMAL";
                item.LinkType = (chkManual.IsChecked.HasValue && chkManual.IsChecked.Value) ? "Manual" : null;

                // Set current user
                var user = M3QAApp.Current.User;
                NDbResult ret;

                ret = DIPSolitionPHTestData.Save(item, user);
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
            CheckConfig();
            InitComboboxes();
            Clear();
        }

        #endregion
    }
}
