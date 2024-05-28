#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

#endregion

using Dapper;
using M3.QA.Models;

namespace M3.QA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDbStatus();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Disconnect();
        }

        #endregion

        #region Button Handler

        private void cmdBrowseExcel_Click(object sender, RoutedEventArgs e)
        {
            string file = ExcelModel.Dialogs.OpenDialog(this);
            if (!string.IsNullOrWhiteSpace(file))
            {
                txtExcelFile.Text = file;
                ProcessExcelFile(file);
            }
        }

        private void cmdBrowseExcel2_Click(object sender, RoutedEventArgs e)
        {
            string file = ExcelModel.Dialogs.OpenDialog(this);
            if (!string.IsNullOrWhiteSpace(file))
            {
                txtExcelFile.Text = file;
                ProcessExcelFile2(file);
            }
        }

        private void cmdConnect_Click(object sender, RoutedEventArgs e)
        {
            Connect();
        }

        private void cmdDisconnect_Click(object sender, RoutedEventArgs e)
        {
            Disconnect();
        }

        private void cmdGetUsers_Click(object sender, RoutedEventArgs e)
        {
            GetUsers();
        }

        private void cmdSaveUsers_Click(object sender, RoutedEventArgs e)
        {
            SaveUsers();
        }

        #endregion

        #region Private Methods

        #region Sql Server Methods

        private void Connect()
        {
            if (!DbServer.Instance.Connected)
            {
                DbServer.Instance.Start();
            }
            UpdateDbStatus();
        }

        private void Disconnect()
        {
            gridDB.ItemsSource = null;
            DbServer.Instance.Shutdown();
            UpdateDbStatus();
        }

        private void UpdateDbStatus()
        {
            txtConnectStatus.Text = DbServer.Instance.Connected ? "Connected" : "Disconnected";
        }

        private void GetUsers()
        {
            gridDB.ItemsSource = null;

            var cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                Console.WriteLine(msg);
            }

            var p = new DynamicParameters();
            try
            {
                var data = cnn.Query<UserInfo>("SELECT * FROM UserInfo", p,
                    commandType: CommandType.Text).AsList();
                gridDB.ItemsSource = data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void SaveUsers()
        {
            if (!DbServer.Instance.Connected) return;
            var users = gridDB.ItemsSource as List<UserInfo>;
            if (null == users || users.Count <= 0) return;
            var cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                Console.WriteLine(msg);
            }

            string cmd = "UPDATE USERINFO SET FullName = @FullName WHERE UserId = @UserId";

            foreach (var user in users) 
            {
                var p = new DynamicParameters();
                p.Add("@FullName", user.FullName);
                p.Add("@UserId", user.UserId);
                try
                {
                    cnn.Execute(cmd, p, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            gridDB.ItemsSource = null; // clear
        }

        #endregion

        #region Excel methods

        private void ProcessExcelFile(string fileName)
        {
            // Note: ตอนเปิดโปรแกรมต้องปิด excel app ก่อน ไม่เช่นนั้นจะ connect ไม่ได้
            var cfg = new NLib.Data.ExcelConfig();
            cfg.DataSource.FileName = fileName;
            cfg.DataSource.HeaderInFirstRow = false;
            cfg.DataSource.Driver = NLib.Data.ExcelDriver.Jet;
            cfg.DataSource.IMexMode = NLib.Data.IMex.ImportMode;

            var conn = new NLib.Components.NDbConnection();
            conn.Config = cfg;
            if (conn.Connect())
            {
                string sheetName1 = "UniTest.TensileCond";
                var table1 = conn.Query("Select * from [" + sheetName1 + "$]").Result;
                gridExcel1.ItemsSource = (null != table1) ? table1.DefaultView : null;

                string sheetName2 = "Data";
                var table2 = conn.Query("Select * from [" + sheetName2 + "$]").Result;
                gridExcel2.ItemsSource = (null != table2) ? table2.DefaultView : null;
            }
            conn.Disconnect();
            conn.Dispose();
            conn = null;
        }

        private void ProcessExcelFile2(string fileName)
        {
            pgrid.SelectedObject = UniTestTensileCond.Import(fileName);
        }

        #endregion

        #endregion
    }
}
