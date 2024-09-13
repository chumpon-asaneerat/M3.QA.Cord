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
            if (Save())
            {
                DialogResult = true;
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region Private Methods

        private void UpdateProductType()
        {
            rbTwist.IsChecked = false;
            rbDIP.IsChecked = false;
            
            CheckSPPanels();

            if (null == sample)
                return;

            if (sample.ProductType == "Twist")
            {
                // Twist
                rbTwist.IsChecked = true;
            }
            else
            {
                // DIP need to select MC
                rbDIP.IsChecked = true;
            }
        }

        private void CheckSPPanels()
        {
            var state = Visibility.Collapsed;

            p1.Visibility = state;
            p2.Visibility = state;
            p3.Visibility = state;
            p4.Visibility = state;
            p5.Visibility = state;
            p6.Visibility = state;
            p7.Visibility = state;

            if (null == sample) return;
            if (sample.NoTestCH >= 1) p1.Visibility = Visibility.Visible;
            if (sample.NoTestCH >= 2) p2.Visibility = Visibility.Visible;
            if (sample.NoTestCH >= 3) p3.Visibility = Visibility.Visible;
            if (sample.NoTestCH >= 4) p4.Visibility = Visibility.Visible;
            if (sample.NoTestCH >= 5) p5.Visibility = Visibility.Visible;
            if (sample.NoTestCH >= 6) p6.Visibility = Visibility.Visible;
            if (sample.NoTestCH >= 7) p7.Visibility = Visibility.Visible;
        }

        private bool Save()
        {
            bool success = false;
            if (null == sample)
            {
                this.InvokeAction(() =>
                {
                    M3QAApp.Windows.ShowMessage("Instance is null.");
                });
                return success;
            }

            var ret = CordTestSampleRecv.Update(sample);
            if (null == ret || !ret.Ok)
            {
                M3QAApp.Windows.SaveFailed();
            }
            else
            {
                M3QAApp.Windows.SaveSuccess();
                success = true;
            }

            return success;
        }

        #endregion

        #region Public Methods

        public void Setup(CordTestSampleRecv value)
        {
            this.DataContext = null;

            sample = value;
            UpdateProductType();

            this.DataContext = sample;
        }

        #endregion
    }
}
