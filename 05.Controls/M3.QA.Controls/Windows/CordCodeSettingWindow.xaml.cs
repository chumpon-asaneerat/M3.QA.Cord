#region Using

using System;
using System.Collections.Generic;
using System.Windows;

using NLib.Models;
using M3.QA.Models;

#endregion

namespace M3.QA.Windows
{
    /// <summary>
    /// Interaction logic for CordCodeSettingWindow.xaml
    /// </summary>
    public partial class CordCodeSettingWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordCodeSettingWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private CordCodeDetail item;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        #endregion

        #region Private Methods

        private void LoadComboboxes()
        {

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

            }

            this.DataContext = item;
        }

        #endregion
    }
}
