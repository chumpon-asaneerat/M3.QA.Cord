using M3.QA.Models;
using NLib.Models;
using NLib.Wpf.Controls;
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
using System.Windows.Shapes;

namespace M3.QA.Windows
{
    /// <summary>
    /// Interaction logic for ChooseUserWindow.xaml
    /// </summary>
    public partial class ChooseUserWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ChooseUserWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdSelect_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            var ctx = (null != btn) ? btn.DataContext : null;
            var user = (null != ctx)  ? ctx as UserInfo : null;
            if (null != user)
            {
                this.User = user;
                DialogResult = true;
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.User = null;
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            ChooseSelectedItem();
        }

        #endregion

        #region ListView Handlers

        private void grid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChooseSelectedItem();
        }

        #endregion

        #region Private Methods

        private void ChooseSelectedItem()
        {
            this.User = grid.SelectedItem as UserInfo;
            if (null == this.User)
            {
                M3QAApp.Windows.ShowMessage("กรุณาเลือก operator");
                return;
            }
            DialogResult = true;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            this.User = null;
            grid.ItemsSource = null;
            grid.ItemsSource = UserInfo.Gets().Value();
        }

        #endregion

        #region Public Properties

        public UserInfo User { get; set; }

        #endregion
    }
}
