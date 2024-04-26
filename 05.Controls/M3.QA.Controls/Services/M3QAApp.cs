#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

using NLib.Services;
using M3.QA.Models;
using M3.QA.Pages;
using M3.QA.Windows;

#endregion

namespace M3.QA
{
    public class M3QAApp
    {
        /// Current.
        /// </summary>
        public static class Current
        {
            /// <summary>
            /// Gets Current User.
            /// </summary>
            public static UserInfo User { get { return SignInManager.Instance.User; } }
        }

        /// <summary>
        /// Pages Static class.
        /// </summary>
        public static class Pages
        {
            #region Static Methods

            private static Dictionary<Type, UserControl> _pages = new Dictionary<Type, UserControl>();

            private static T GetPage<T>()
                where T : UserControl, new()
            {
                Type type = typeof(T);
                if (!_pages.ContainsKey(type))
                {
                    lock (typeof(M3QAApp))
                    {
                        T inst = new T();
                        _pages.Add(type, inst);
                    }
                }

                T ret = _pages[type] as T;

                return ret;
            }

            #endregion

            #region Common Method(s)

            public static void GotoQAMainMenu()
            {
                var page = M3QAMainMenu;
                page.Setup();
                PageContentManager.Instance.Current = page;
            }

            #endregion

            #region Main Menu (Cord)

            /// <summary>Gets M3 QA MainMenu Page.</summary>
            public static M3QAMainMenuPage M3QAMainMenu
            {
                get
                {
                    return GetPage<M3QAMainMenuPage>();
                }
            }

            #endregion

            #region Cord

            /// <summary>Gets M3 Receive Cord Sample Test Page.</summary>
            public static ReceiveCordTestSamplePage ReceiveCordSampleTest
            {
                get
                {
                    return GetPage<ReceiveCordTestSamplePage>();
                }
            }
            /// <summary>Gets M3 Cord Sample Test Data Page.</summary>
            public static CordSampleTestDataPage CordSampleTestData
            {
                get
                {
                    return GetPage<CordSampleTestDataPage>();
                }
            }

            #endregion

            #region Dip Solution

            /// <summary>Gets M3 Receive Solution Sample Test Page.</summary>
            public static ReceiveDIPSolutionTestSamplePage ReceiveDipSolutionTestSample
            {
                get
                {
                    return GetPage<ReceiveDIPSolutionTestSamplePage>();
                }
            }
            /// <summary>Gets M3 DIP Solution Sample Test Data Page.</summary>
            public static SolutionSampleTestDataPage SolutionSampleTestData
            {
                get
                {
                    return GetPage<SolutionSampleTestDataPage>();
                }
            }

            #endregion

            #region Master

            /// <summary>Gets M3 Cord Code Setting Page.</summary>
            public static CordCodeSettingPage CordCodeSetting
            {
                get { return GetPage<CordCodeSettingPage>(); }
            }
            /// <summary>Gets M3 Cord User Manage Page.</summary>
            public static UserManagementPage UserManage
            {
                get { return GetPage<UserManagementPage>(); }
            }

            #endregion
        }

        /// <summary>
        /// Windows Static class.
        /// </summary>
        public static class Windows
        {
            #region Static Methods

            private static T GetWindow<T>()
                where T : Window, new()
            {
                T inst = new T();
                inst.Owner = Application.Current.MainWindow;
                return inst;
            }

            #endregion

            #region MessageBox

            /// <summary>Gets MessageBox Window.</summary>
            public static MessageBoxWindow MessageBox
            {
                get { return GetWindow<QA.Windows.MessageBoxWindow>(); }
            }

            public static void SaveSuccess()
            {
                var win = M3QAApp.Windows.MessageBox;
                win.Setup("Save Success" + Environment.NewLine + "บันทึกข้อมูลสำเร็จ");
                win.ShowDialog();
            }

            public static void SaveFailed()
            {
                var win = M3QAApp.Windows.MessageBox;
                win.Setup("Save Failed" + Environment.NewLine + "บันทึกข้อมูลไม่สำเร็จ");
                win.ShowDialog();
            }

            public static void ShowMessage(string message)
            {
                var win = M3QAApp.Windows.MessageBox;
                win.Setup(message);
                win.ShowDialog();
            }

            #endregion

            #region MessageBoxOKCancel

            /// <summary>Gets MessageBoxOkCancel Window.</summary>
            public static MessageBoxOKCancelWindow MessageBoxOKCancel
            {
                get { return GetWindow<QA.Windows.MessageBoxOKCancelWindow>(); }
            }

            #endregion

            #region SignIn

            /// <summary>Gets SignIn Window.</summary>
            public static SignInWindow SignIn
            {
                get { return GetWindow<SignInWindow>(); }
            }

            #endregion

            #region Confirm User

            /// <summary>Gets Confirm User Window.</summary>
            public static ConfirmUserWindow ConfirmUser
            {
                get { return GetWindow<ConfirmUserWindow>(); }
            }

            #endregion

            #region Choose User

            /// <summary>Gets Choose User Window.</summary>
            public static ChooseUserWindow ChooseUser
            {
                get { return GetWindow<ChooseUserWindow>(); }
            }

            #endregion

            #region User Editor

            /// <summary>Gets Cord User Window.</summary>
            public static UserEditorWindow UserEditor
            {
                get { return GetWindow<UserEditorWindow>(); }
            }

            #endregion

            #region Cord Code Setting Editor

            /// <summary>Gets Cord Code Setting Window.</summary>
            public static CordCodeSettingEditorWindow CordCodeSettingEditor
            {
                get { return GetWindow<CordCodeSettingEditorWindow>(); }
            }

            #endregion
        }
    }
}
