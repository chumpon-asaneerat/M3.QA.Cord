#region Using

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

#endregion

namespace M3.QA.Controls.Elements
{
    /// <summary>
    /// Interaction logic for AppVersionElement.xaml
    /// </summary>
    public partial class AppVersionElement : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AppVersionElement()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }

        #endregion

        #region Private Methods

        private void UpdateUI()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                txtAppInfo.Text = ApplicationManager.Instance.Environments.Options.AppInfo.DisplayText;
            }));
        }

        #endregion
    }
}
