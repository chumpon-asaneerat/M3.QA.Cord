using M3.QA.Models;
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

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for CordShrinkagePctPage.xaml
    /// </summary>
    public partial class CordShrinkagePctPage : UserControl
    {
        #region Constructor

        public CordShrinkagePctPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdRequest_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var ctx = (null != btn) ? btn.DataContext : null;
            var item = (null != ctx) ? ctx as CordShrinkagePct : null;
            if (null != item)
            {
                var usr = (null != M3QAApp.Current.User) ? M3QAApp.Current.User.FullName : null;
                var sp = item.SPNo;
                var remark = "Request Full CH !!!!";
                var ret = Models.Utils.M_ReceiveFullSample.Save(item.LotNo, null, usr, DateTime.Now, sp, sp, null, remark);
                if (null != ret && ret.Ok)
                {
                    M3QAApp.Windows.SaveSuccess();
                }
                else M3QAApp.Windows.SaveFailed();
            }
        }

        #endregion
    }
}
