#region Using

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
using System.Windows.Shapes;

using M3.QA.Models;
using NLib;

#endregion

namespace M3.QA.Windows
{
    /// <summary>
    /// Interaction logic for RequestFullCHWindow.xaml
    /// </summary>
    public partial class RequestFullCHWindow : Window
    {
        #region Constructor

        public RequestFullCHWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtRemark.Text))
            {
                M3QAApp.Windows.ShowMessage("Remark Required.");
                this.Dispatcher.Invoke(new Action(() =>
                {
                    txtRemark.FocusControl();
                }));

                return;
            }

            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region Public Methods

        public void Setup(string lotNo, int spNo)
        {
            txtLotNo.Text = lotNo;
            txtSPNo.Text = spNo.ToString();
            txtRemark.Text = string.Empty;
            this.Dispatcher.Invoke(new Action(() => 
            { 
                txtRemark.FocusControl(); 
            }));
        }

        #endregion

        #region Public Property

        public string LotNo
        {
            get { return txtLotNo.Text; }
        }
        public string SPNo
        {
            get { return txtSPNo.Text; }
        }
        public string Remark 
        { 
            get { return txtRemark.Text; } 
        }

        #endregion
    }
}
