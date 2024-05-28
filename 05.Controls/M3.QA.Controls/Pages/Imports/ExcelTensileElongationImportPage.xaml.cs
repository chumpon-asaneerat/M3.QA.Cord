﻿#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

using M3.QA.Models;
using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for ExcelTensileElongationImportPage.xaml
    /// </summary>
    public partial class ExcelTensileElongationImportPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ExcelTensileElongationImportPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private UniTestTensileElongation item;

        #endregion

        #region Button Handlers

        private void cmdBrowseExcel_Click(object sender, RoutedEventArgs e)
        {
            Browse();
        }

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3QAApp.Pages.GotoQAMainMenu();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        #endregion

        #region Private Methods

        private void Browse()
        {
            string file = ExcelModel.Dialogs.OpenDialog();
            if (!string.IsNullOrWhiteSpace(file))
            {
                this.DataContext = null;

                txtExcelFileName.Text = file;

                var ret = UniTestTensileElongation.Import(file);
                if (null == ret || !ret.IsValid)
                {
                    string errMsg = null == ret ? "Error open excel file." : ret.ErrMsg;
                    M3QAApp.Windows.ShowMessage(errMsg);
                    return;
                }
                // update item
                item = ret.Value;
                // bind to data context
                this.DataContext = item;
            }
        }

        private void Save()
        {

        }

        private void ClearInput()
        {
            txtExcelFileName.Text = string.Empty;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            ClearInput();
        }

        #endregion
    }
}
