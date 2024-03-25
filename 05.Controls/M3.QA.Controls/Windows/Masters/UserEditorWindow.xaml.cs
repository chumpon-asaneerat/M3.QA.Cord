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

using NLib.Models;
using M3.QA.Models;
using NLib;

#endregion

namespace M3.QA.Windows
{
    /// <summary>
    /// Interaction logic for UserEditorWindow.xaml
    /// </summary>
    public partial class UserEditorWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserEditorWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private UserInfo _item = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (null != _item)
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    var win = M3QAApp.Windows.MessageBox;
                    win.Setup("กรุณาบันทึก Full Name");
                    win.ShowDialog();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    var win = M3QAApp.Windows.MessageBox;
                    win.Setup("กรุณาบันทึก User Name");
                    win.ShowDialog();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPwd.Password))
                {
                    var win = M3QAApp.Windows.MessageBox;
                    win.Setup("กรุณาบันทึก Password");
                    win.ShowDialog();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPwdConfirm.Password))
                {
                    var win = M3QAApp.Windows.MessageBox;
                    win.Setup("กรุณาบันทึก Password (Confirm)");
                    win.ShowDialog();
                    return;
                }
                if (txtPwd.Password != txtPwdConfirm.Password)
                {
                    var win = M3QAApp.Windows.MessageBox;
                    win.Setup("Password (Confirm) ไม่ตรงกับข้อมูล Password");
                    win.ShowDialog();
                    return;
                }

                if (!rbAdmin.IsChecked.HasValue && !rbSupervisor.IsChecked.HasValue && !rbUser.IsChecked.HasValue)
                {
                    var win = M3QAApp.Windows.MessageBox;
                    win.Setup("กรุณาเลือก User Role.");
                    win.ShowDialog();
                    return;
                }

                if (rbAdmin.IsChecked == false && rbSupervisor.IsChecked == false && rbUser.IsChecked == false)
                {
                    var win = M3QAApp.Windows.MessageBox;
                    win.Setup("กรุณาเลือก User Role.");
                    win.ShowDialog();
                    return;
                }

                _item.FullName = txtFullName.Text;
                _item.UserName = txtUserName.Text;
                _item.Password = txtPwd.Password;
                _item.Active = 1;
                
                if (rbAdmin.IsChecked == true) 
                {
                    _item.RoleId = 1;
                }
                else if (rbSupervisor.IsChecked == true)
                {
                    _item.RoleId = 10;
                }
                else if (rbUser.IsChecked == true)
                {
                    _item.RoleId = 20;
                }

                DialogResult = true;
            }
        }

        #endregion

        #region Private Methods

        private void SetupRadioButtons()
        {
            var curr = M3QAApp.Current.User;
            if (curr.RoleId == 1)
            {
                // Admin
                rbAdmin.Visibility = Visibility.Visible;
                rbSupervisor.Visibility = Visibility.Visible;
                rbUser.Visibility = Visibility.Visible;

                rbAdmin.IsChecked = false;
                rbSupervisor.IsChecked = false;
                rbUser.IsChecked = false;
            }
            else if (curr.RoleId == 10)
            {
                // Supervisor
                rbAdmin.Visibility = Visibility.Collapsed;
                rbSupervisor.Visibility = Visibility.Visible;
                rbUser.Visibility = Visibility.Visible;

                rbAdmin.IsChecked = false;
                rbSupervisor.IsChecked = false;
                rbUser.IsChecked = false;
            }
            else if (curr.RoleId >= 20)
            {
                // User
                rbAdmin.Visibility = Visibility.Collapsed;
                rbSupervisor.Visibility = Visibility.Collapsed;
                rbUser.Visibility = Visibility.Visible;

                rbAdmin.IsChecked = false;
                rbSupervisor.IsChecked = false;
                rbUser.IsChecked = false;
            }

            if (null != _item)
            {
                rbAdmin.IsChecked = (_item.RoleId == 1);
                rbSupervisor.IsChecked = (_item.RoleId == 10);
                rbUser.IsChecked = (_item.RoleId >= 20);
            }
        }

        private void SetupUser()
        {
            if (null == _item) return;
            txtFullName.Text = _item.FullName;
            txtUserName.Text = _item.UserName;
            txtPwd.Password = _item.Password;
            txtPwdConfirm.Password = (_item.UserId <= 0) ? string.Empty : _item.Password;

            if (_item.RoleId == 1 && _item.UserId == 1)
            {
                // Special Admin User cannot change Role/FullName/UserName
                txtFullName.IsReadOnly = true;
                txtUserName.IsReadOnly = true;
                rbAdmin.IsEnabled = false;
                rbSupervisor.IsEnabled = false;
                rbUser.IsEnabled = false;
            }
        }

        #endregion

        #region Public Methods

        public void Setup(UserInfo value)
        {
            _item = value;

            SetupRadioButtons();
            SetupUser();
        }

        #endregion
    }
}
