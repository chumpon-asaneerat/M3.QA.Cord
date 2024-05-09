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
    /// Interaction logic for CordProductionTestViewWindow.xaml
    /// </summary>
    public partial class CordProductionTestViewWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordProductionTestViewWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private CordProduction item = null;

        #endregion

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region Public Methods

        public void Setup(CordProduction value)
        {
            this.DataContext = null;
            item = value;
            this.DataContext = item;
        }

        #endregion
    }
}
