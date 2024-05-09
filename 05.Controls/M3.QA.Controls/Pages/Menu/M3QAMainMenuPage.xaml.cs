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
using System.Windows.Navigation;
using System.Windows.Shapes;

using NLib.Services;

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for M3QAMainMenuPage.xaml
    /// </summary>
    public partial class M3QAMainMenuPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public M3QAMainMenuPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        #region Cord Menu

        // Cord Receive Sample
        private void cmdReceiveCordTestSample_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3QAApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3QAApp.Current.User)
            {
                var msgbox = M3QAApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            // Receive Cord Test Sample
            var page = M3QAApp.Pages.ReceiveCordSampleTest;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }
        // Cord Test Data
        private void cmdCordTestData_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3QAApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3QAApp.Current.User)
            {
                var msgbox = M3QAApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            var page = M3QAApp.Pages.CordSampleTestData;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }
        // Cord Production
        private void cmdCordProduction_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3QAApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3QAApp.Current.User)
            {
                var msgbox = M3QAApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            var page = M3QAApp.Pages.CordProduction;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        #region DIP Solution Menu

        // Dip Solution Receive Sample
        private void cmdReceiveDipSolution_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3QAApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3QAApp.Current.User)
            {
                var msgbox = M3QAApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }

            var page = M3QAApp.Pages.ReceiveDIPSolutionTestSample;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }
        // Dip Solution Test Data
        private void cmdDipSolutionTestData_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3QAApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3QAApp.Current.User)
            {
                var msgbox = M3QAApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            var page = M3QAApp.Pages.DIPSolutionSampleTestData;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }
        // Not implements
        private void cmdDipSolutionProduction_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3QAApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3QAApp.Current.User)
            {
                var msgbox = M3QAApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
        }

        #endregion

        #region Master Data Menu

        // Cord Code Setting
        private void cmdCordCodeSetting_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3QAApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3QAApp.Current.User)
            {
                var msgbox = M3QAApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }

            var page = M3QAApp.Pages.CordCodeSetting;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }
        // Cord Spec Setting
        private void cmdCordTestSpecification_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3QAApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3QAApp.Current.User)
            {
                var msgbox = M3QAApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }

            var page = M3QAApp.Pages.CordSpecSetting;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }
        // Not Implements
        private void cmdDipSolutionTestSpecification_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3QAApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3QAApp.Current.User)
            {
                var msgbox = M3QAApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
        }
        // User Manage
        private void cmdUserManage_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3QAApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3QAApp.Current.User)
            {
                var msgbox = M3QAApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            if (M3QAApp.Current.User.RoleId > 10)
            {
                // Role 1 : Admin
                // Role 10 : Supervisor
                var msgbox = M3QAApp.Windows.MessageBox;
                msgbox.Setup("ผู้ใช้ปัจจุบันไม่มีสิทธิเข้าถึงหน้าจอนี้ได้");
                msgbox.ShowDialog();
                return;
            }

            var page = M3QAApp.Pages.UserManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        #endregion

        #region Public Methods

        public void Setup()
        {

        }

        #endregion
    }
}
