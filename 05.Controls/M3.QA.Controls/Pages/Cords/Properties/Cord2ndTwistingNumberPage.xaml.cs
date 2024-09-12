﻿#region Using

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

using M3.QA.Models;

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for Cord2ndTwistingNumberPage.xaml
    /// </summary>
    public partial class Cord2ndTwistingNumberPage : UserControl
    {
        #region Constructor

        public Cord2ndTwistingNumberPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdRequest_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var ctx = (null != btn) ? btn.DataContext : null;
            var item = (null != ctx) ? ctx as Cord2ndTwistingNumber : null;
            if (null != item && item.SPNo.HasValue)
            {
                RequestFullCHService.Request(item.LotNo, item.SPNo.Value);
            }
        }

        #endregion
    }
}
