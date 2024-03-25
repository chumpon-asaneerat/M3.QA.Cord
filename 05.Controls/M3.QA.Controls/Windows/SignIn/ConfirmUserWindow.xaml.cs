#region Using

using System;
using System.Windows;
using System.Windows.Controls;
using M3.QA.Models;
using NLib;

#endregion

namespace M3.QA.Windows
{
    /// <summary>
    /// Interaction logic for ConfirmUserWindow.xaml
    /// </summary>
    public partial class ConfirmUserWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConfirmUserWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Focus();
        }

        #endregion

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            Confirm();
        }

        #endregion

        #region Private Methods

        private void Confirm()
        {
            string userName = txtUserName.Text;
            if (string.IsNullOrWhiteSpace(userName))
            {
                var win = M3QAApp.Windows.MessageBox;
                win.Setup("กรุณาป้อน ชื่อบัญชีผู้ใช้");
                win.ShowDialog();

                txtUserName.FocusControl();
                return;
            }

            string password = txtPassword.Password;
            if (string.IsNullOrWhiteSpace(password))
            {
                var win = M3QAApp.Windows.MessageBox;
                win.Setup("กรุณาป้อน รหัสผ่าน");
                win.ShowDialog();

                txtPassword.FocusControl();
                return;
            }

            SignInStatus status = VerifyManager.Instance.Verify(userName, password);

            bool success = false;
            string msg = string.Empty;
            switch (status)
            {
                case SignInStatus.UserNotFound:
                    msg = "ไม่พบข้อมูล ชื่อผู้ใช้งาน กรุณาตรวจสอบ";
                    break;
                case SignInStatus.InvalidPassword:
                    msg = "รหัสผ่านไม่ถูกต้อง กรุณาตรวจสอบ";
                    break;
                case SignInStatus.Success:
                    success = true;
                    break;
            }

            if (!success)
            {
                User = null;

                // login failed show message.
                var win = M3QAApp.Windows.MessageBox;
                win.Setup(msg);
                win.ShowDialog();

                if (status == SignInStatus.InvalidPassword)
                    txtPassword.FocusControl();
                else txtUserName.FocusControl();
            }

            User = VerifyManager.Instance.User;
            DialogResult = success;
        }

        #endregion

        #region Public Properties

        public UserInfo User { get; set; }

        #endregion
    }
}
