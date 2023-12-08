#region Using

using M3.Cord.Models;
using NLib;
using System;
using System.Collections.Generic;
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
using static M3.Cord.Models.ExcelModel;

#endregion

namespace M3.Cord.App
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

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handler

        private void cmdBrowseExcel_Click(object sender, RoutedEventArgs e)
        {
            string file = Dialogs.OpenDialog(this);
            if (!string.IsNullOrWhiteSpace(file))
            {
                txtExcelFile.Text = file;
                ProcessExcelFile(file);
            }
        }

        #endregion

        private void ProcessExcelFile(string fileName)
        {
            var cfg = new NLib.Data.ExcelConfig();
            cfg.DataSource.FileName = fileName;
            cfg.DataSource.HeaderInFirstRow = false;
            cfg.DataSource.Driver = NLib.Data.ExcelDriver.Jet;
            cfg.DataSource.IMexMode = NLib.Data.IMex.ImportMode;

            var conn = new NLib.Components.NDbConnection();
            conn.Config = cfg;
            if (conn.Connect())
            {
                string sheetName = "UniTest.TensileCond";
                var table = conn.Query("Select * from [" + sheetName + "$]").Result;
                gridExcel.ItemsSource = table.DefaultView;
            }
            conn.Disconnect();
            conn.Dispose();
            conn = null;
        }
    }
}
