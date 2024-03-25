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

        private void cmdReceiveCordTestSample_Click(object sender, RoutedEventArgs e)
        {
            /*
            var page = M3QAApp.Pages.ReceiveCordTestSample;
            page.Setup();
            PageContentManager.Instance.Current = page;
            */
        }

        private void cmdCordTestData_Click(object sender, RoutedEventArgs e)
        {
            /*
            var page = M3QAApp.Pages.CordTestData;
            page.Setup();
            PageContentManager.Instance.Current = page;
            */
        }

        private void cmdCordProduction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdReceiveDipSolution_Click(object sender, RoutedEventArgs e)
        {
            /*
            var page = M3QAApp.Pages.ReceiveDipSolutionTestSample;
            page.Setup();
            PageContentManager.Instance.Current = page;
            */
        }

        private void cmdDipSolutionTestData_Click(object sender, RoutedEventArgs e)
        {
            /*
            var page = M3QAApp.Pages.DipSolutionTestData;
            page.Setup();
            PageContentManager.Instance.Current = page;
            */
        }

        private void cmdDipSolutionProduction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdCordTestSpecification_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdDipSolutionTestSpecification_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdUserManage_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Public Methods

        public void Setup()
        {

        }

        #endregion
    }
}
