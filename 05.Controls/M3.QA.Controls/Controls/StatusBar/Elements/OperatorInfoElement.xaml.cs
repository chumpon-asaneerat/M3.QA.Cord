#region Using

using NLib.Utils;
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
    /// Interaction logic for OperatorInfoElement.xaml
    /// </summary>
    public partial class OperatorInfoElement : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public OperatorInfoElement()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SignInManager.Instance.UserChanged += Instance_UserChanged;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            SignInManager.Instance.UserChanged -= Instance_UserChanged;
        }

        #endregion

        #region SignInManager Handlers

        private void Instance_UserChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        #endregion

        #region Private Methods

        private void UpdateUI()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                var usr = M3QAApp.Current.User;
                txtUserInfo.Text = (null != usr) ? usr.FullName + " (" + usr.RoleName + ")" : "-";
            }));
        }

        #endregion
    }
}
