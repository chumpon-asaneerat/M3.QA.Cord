#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib.Services;
using M3.QA.Models;
using NLib.Models;
using NLib;
using NLib.Wpf.Controls;

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for UserManagementPage.xaml
    /// </summary>
    public partial class UserManagementPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserManagementPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<UserInfo> _items = null;

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

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            M3QAApp.Pages.GotoQAMainMenu();
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = (string.IsNullOrWhiteSpace(txtSearch.Text)) ? string.Empty : txtSearch.Text.Trim();
            this.InvokeAction(() =>
            {
                RefreshGrid();
            });
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            var item = new UserInfo();
            Add(item);
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            if (null == btn) return;
            var item = btn.DataContext as UserInfo;
            if (null == item) return;

            Edit(item);
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            if (null == btn) return;
            var item = btn.DataContext as UserInfo;

            Delete(item);
        }

        private void cmdReset_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            if (null == btn) return;
            var item = btn.DataContext as UserInfo;

            Reset(item);
        }

        #endregion

        #region Private Methods

        private void Add(UserInfo item)
        {
            if (null == item) return;
            var win = M3QAApp.Windows.UserEditor;
            win.Setup(item);

            if (win.ShowDialog() == true)
            {
                var win2 = M3QAApp.Windows.MessageBox;

                var ret = UserInfo.Save(item);
                if (null != ret && ret.Ok)
                    win2.Setup("บันทึกรายการสำเร็จ");
                else
                    win2.Setup("บันทึกรายการไม่สำเร็จ");

                win2.ShowDialog();

                this.InvokeAction(() =>
                {
                    RefreshGrid();
                });
            }
        }

        private void Edit(UserInfo item)
        {
            if (null == item) return;
            var win = M3QAApp.Windows.UserEditor;
            win.Setup(item);

            if (win.ShowDialog() == true)
            {
                var win2 = M3QAApp.Windows.MessageBox;

                var ret = UserInfo.Save(item);
                if (null != ret && ret.Ok)
                    win2.Setup("บันทึกรายการสำเร็จ");
                else
                    win2.Setup("บันทึกรายการไม่สำเร็จ");

                win2.ShowDialog();

                this.InvokeAction(() =>
                {
                    RefreshGrid();
                });
            }
        }

        private void Delete(UserInfo item)
        {
            if (null == item) return;

            var win = M3QAApp.Windows.MessageBoxOKCancel;
            win.Setup("ต้องการลบรายการใช่หรือไม่");
            if (win.ShowDialog() == true)
            {
                var win2 = M3QAApp.Windows.MessageBox;

                var ret = UserInfo.Delete(item);
                if (null != ret && ret.Ok)
                    win2.Setup("ลบรายการสำเร็จ");
                else
                    win2.Setup("ลบรายการไม่สำเร็จ");

                win2.ShowDialog();

                this.InvokeAction(() =>
                {
                    RefreshGrid();
                });
            }
        }

        private void Reset(UserInfo item)
        {
            if (null == item) return;

            var win = M3QAApp.Windows.MessageBoxOKCancel;
            win.Setup("ต้องการ Reset Password ใช่หรือไม่");
            if (win.ShowDialog() == true)
            {
                var win2 = M3QAApp.Windows.MessageBox;

                var ret = UserInfo.Reset(item);
                if (null != ret && ret.Ok)
                    win2.Setup("Reset Password สำเร็จ");
                else
                    win2.Setup("Reset Password ไม่สำเร็จ");

                win2.ShowDialog();

                this.InvokeAction(() =>
                {
                    RefreshGrid();
                });
            }
        }

        private void RefreshGrid()
        {
            string search = txtSearch.Text;

            ActiveStatus status;
            if (rbAll.IsChecked.HasValue && rbAll.IsChecked == true)
                status = ActiveStatus.All;
            else if (rbActive.IsChecked.HasValue && rbActive.IsChecked == true)
                status = ActiveStatus.Active;
            else status = ActiveStatus.Inactive;

            int? userId = (null != M3QAApp.Current.User) ? M3QAApp.Current.User.UserId : new int?();
            int? roleId = (null != M3QAApp.Current.User) ? M3QAApp.Current.User.RoleId : new int?();

            grid.ItemsSource = null;
            _items = UserInfo.Search(search, status, userId, roleId).Value();
            grid.ItemsSource = _items;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            RefreshGrid();
        }

        #endregion
    }
}
