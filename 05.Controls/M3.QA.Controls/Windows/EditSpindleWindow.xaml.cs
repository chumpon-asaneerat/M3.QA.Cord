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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace M3.QA.Windows
{
    /// <summary>
    /// Interaction logic for EditSpindleWindow.xaml
    /// </summary>
    public partial class EditSpindleWindow : Window
    {
        #region Constructor
        
        public EditSpindleWindow()
        {
            InitializeComponent();
        }

        #endregion

        private CordTestSampleRecv sample = null;

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

        public void Setup(CordTestSampleRecv value)
        {
            this.DataContext = null;
            sample = value;
            this.DataContext = sample;
        }

        #endregion
    }
}
