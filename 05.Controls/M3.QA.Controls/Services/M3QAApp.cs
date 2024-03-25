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

            #region ReceiveCordTestSample

            public static ReceiveCordTestSamplePage ReceiveCordTestSample
            {
                get
                {
                    return GetPage<ReceiveCordTestSamplePage>();
                }
            }

            #endregion

            #region ReceiveDipSolution

            public static ReceiveDipSolutionTestSamplePage ReceiveDipSolutionTestSample
            {
                get
                {
                    return GetPage<ReceiveDipSolutionTestSamplePage>();
                }
            }

            public static DipSolutionTestDataPage DipSolutionTestData
            {
                get
                {
                    return GetPage<DipSolutionTestDataPage>();
                }
            }

            public static CordTestDataPage CordTestData
            {
                get
                {
                    return GetPage<CordTestDataPage>();
                }
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

            #endregion

            #region MessageBoxOKCancel

            /// <summary>Gets MessageBoxOkCancel Window.</summary>
            public static MessageBoxOKCancelWindow MessageBoxOKCancel
            {
                get { return GetWindow<QA.Windows.MessageBoxOKCancelWindow>(); }
            }

            #endregion
        }
    }
}
