#region Using

using System;
using System.Windows;
using System.Windows.Controls;

using NLib;

#endregion

namespace M3.QA.Windows
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SignInWindow()
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
            SignIn();
        }

        #endregion

        #region Private Methods

        private void SignIn()
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

            SignInStatus status = SignInManager.Instance.SignIn(userName, password);

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
                // login failed show message.
                var win = M3QAApp.Windows.MessageBox;
                win.Setup(msg);
                win.ShowDialog();

                if (status == SignInStatus.InvalidPassword)
                    txtPassword.FocusControl();
                else txtUserName.FocusControl();
            }

            DialogResult = success;
        }

        #endregion
    }
}
