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
    /// Interaction logic for CordSpecificationSettingPage.xaml
    /// </summary>
    public partial class CordSpecificationSettingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordSpecificationSettingPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<CordCode> cordCodes;
        private List<CordTestSpec> specs;

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

        #region Combox Handlers

        private void cbCordCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
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
        }

        private void Refresh()
        {
            entry.ItemsSource = null;
            var cordCode = cbCordCode.SelectedItem as CordCode;

            entry.IsEnabled = (null != cordCode);

            if (null != cordCode)
            {
                specs = CordTestSpec.GetSettings(cordCode);
                entry.ItemsSource = specs;
            }
        }

        private void Save()
        {

        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            LoadComboboxes();

            Refresh();
        }

        #endregion
    }
}
