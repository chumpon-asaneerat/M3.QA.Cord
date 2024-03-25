#region Using

using NLib.Data.Design;
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
    /// Interaction logic for ReceiveCordSampleTestPage.xaml
    /// </summary>
    public partial class ReceiveCordSampleTestPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReceiveCordSampleTestPage()
        {
            InitializeComponent();
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

        #region Combobox Handlers

        private void cbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void LoadCustomers()
        {
            cbCustomers.ItemsSource = null;


        }

        private void ClearInputs()
        {
            dtRecv.Value = DateTime.Now;
            txtLotNo.Text = string.Empty;
        }

        private void Save()
        {

            ClearInputs();
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            LoadComboboxs();
            ClearInputs();
        }

        #endregion
    }
}
