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

        #region Private Methods

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
            ClearInputs();
        }

        #endregion
    }
}
