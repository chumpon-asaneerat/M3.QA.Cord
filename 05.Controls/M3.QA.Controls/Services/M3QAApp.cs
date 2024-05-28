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
            /// <summary>Gets M3 Cord Production Page.</summary>
            public static CordProductionPage CordProduction
            {
                get
                {
                    return GetPage<CordProductionPage>();
                }
            }

            #endregion

            #region Dip Solution

            /// <summary>Gets M3 Receive DIP Solution Sample Test Page.</summary>
            public static ReceiveDIPSolutionTestSamplePage ReceiveDIPSolutionTestSample
            {
                get
                {
                    return GetPage<ReceiveDIPSolutionTestSamplePage>();
                }
            }
            /// <summary>Gets M3 DIP Solution Sample Test Data Page.</summary>
            public static DIPSolutionSampleTestDataPage DIPSolutionSampleTestData
            {
                get
                {
                    return GetPage<DIPSolutionSampleTestDataPage>();
                }
            }
            /// <summary>Gets M3 DIP Solution Production Page.</summary>
            public static DIPSolutionProductionPage DIPSolutionProduction
            {
                get
                {
                    return GetPage<DIPSolutionProductionPage>();
                }
            }

            #endregion

            #region Auto Transfer

            /// <summary>Gets M3 Auto Transfer Tensile Strength/Elongation Import Page.</summary>
            public static ExcelTensileElongationImportPage ExcelTensileElongationImport
            {
                get
                {
                    return GetPage<ExcelTensileElongationImportPage>();
                }
            }

            #endregion

            #region Master

            /// <summary>Gets M3 Cord Code Setting Page.</summary>
            public static CordCodeSettingPage CordCodeSetting
            {
                get { return GetPage<CordCodeSettingPage>(); }
            }

            /// <summary>Gets M3 Cord Specification Setting Page.</summary>
            public static CordSpecificationSettingPage CordSpecSetting
            {
                get { return GetPage<CordSpecificationSettingPage>(); }
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

            public static void SaveFailed(string errmsg = null)
            {
                var win = M3QAApp.Windows.MessageBox;
                string msg = "Save Failed" + Environment.NewLine + "บันทึกข้อมูลไม่สำเร็จ";
                if (!string.IsNullOrEmpty(errmsg))
                {
                    msg += Environment.NewLine + errmsg;
                }
                win.Setup(msg);
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

            #region Cord Production Test Data View Window

            /// <summary>Gets Cord Production Test Data View Window.</summary>
            public static CordProductionTestViewWindow CordProductionTestView
            {
                get { return GetWindow<CordProductionTestViewWindow>(); }
            }

            #endregion

            #region Export Message

            public static void ExportSuccess()
            {
                var msg = Windows.MessageBox;
                msg.Setup("ส่งออกไฟล์สำเร็จ");
                msg.ShowDialog();
            }

            public static void ExportFailed()
            {
                var msg = Windows.MessageBox;
                msg.Setup("ส่งออกไฟล์ไม่สำเร็จ");
                msg.ShowDialog();
            }

            public static void ExportFailed(string err)
            {
                var msg = Windows.MessageBox;
                msg.Setup(err);
                msg.ShowDialog();
            }

            #endregion
        }
    }
}
