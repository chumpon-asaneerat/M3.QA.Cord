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

using NLib;
using NLib.Services;
using NLib.Models;
using NLib.Wpf.Controls;
using M3.QA.Models;

#endregion

namespace M3.QA.Pages
{
    /// <summary>
    /// Interaction logic for CordTestDataPage.xaml
    /// </summary>
    public partial class CordTestDataPage : UserControl
    {
        #region Constructor
        public CordTestDataPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tensileStrength.Visibility = Visibility.Collapsed;
            elongation.Visibility = Visibility.Collapsed;
            adhesionForce.Visibility = Visibility.Collapsed;
            shrinkageForce.Visibility = Visibility.Collapsed;
            shrinkage.Visibility = Visibility.Collapsed;
            twisting1.Visibility = Visibility.Collapsed;
            twisting2.Visibility = Visibility.Collapsed;
            thickness.Visibility = Visibility.Collapsed;
            denier.Visibility = Visibility.Collapsed;
            rpu.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers
        private void cmdTensileStrength_Click(object sender, RoutedEventArgs e)
        {
            tensileStrength.Visibility = Visibility.Visible;
            elongation.Visibility = Visibility.Collapsed;
            adhesionForce.Visibility = Visibility.Collapsed;
            shrinkageForce.Visibility = Visibility.Collapsed;
            shrinkage.Visibility = Visibility.Collapsed;
            twisting1.Visibility = Visibility.Collapsed;
            twisting2.Visibility = Visibility.Collapsed;
            thickness.Visibility = Visibility.Collapsed;
            denier.Visibility = Visibility.Collapsed;
            rpu.Visibility = Visibility.Collapsed;
        }
        private void cmdElongation_Click(object sender, RoutedEventArgs e)
        {
            tensileStrength.Visibility = Visibility.Collapsed;
            elongation.Visibility = Visibility.Visible;
            adhesionForce.Visibility = Visibility.Collapsed;
            shrinkageForce.Visibility = Visibility.Collapsed;
            shrinkage.Visibility = Visibility.Collapsed;
            twisting1.Visibility = Visibility.Collapsed;
            twisting2.Visibility = Visibility.Collapsed;
            thickness.Visibility = Visibility.Collapsed;
            denier.Visibility = Visibility.Collapsed;
            rpu.Visibility = Visibility.Collapsed;
        }
        private void cmdAdhesionForce_Click(object sender, RoutedEventArgs e)
        {
            tensileStrength.Visibility = Visibility.Collapsed;
            elongation.Visibility = Visibility.Collapsed;
            adhesionForce.Visibility = Visibility.Visible;
            shrinkageForce.Visibility = Visibility.Collapsed;
            shrinkage.Visibility = Visibility.Collapsed;
            twisting1.Visibility = Visibility.Collapsed;
            twisting2.Visibility = Visibility.Collapsed;
            thickness.Visibility = Visibility.Collapsed;
            denier.Visibility = Visibility.Collapsed;
            rpu.Visibility = Visibility.Collapsed;
        }
        private void cmdShrinkageForce_Click(object sender, RoutedEventArgs e)
        {
            tensileStrength.Visibility = Visibility.Collapsed;
            elongation.Visibility = Visibility.Collapsed;
            adhesionForce.Visibility = Visibility.Collapsed;
            shrinkageForce.Visibility = Visibility.Visible;
            shrinkage.Visibility = Visibility.Collapsed;
            twisting1.Visibility = Visibility.Collapsed;
            twisting2.Visibility = Visibility.Collapsed;
            thickness.Visibility = Visibility.Collapsed;
            denier.Visibility = Visibility.Collapsed;
            rpu.Visibility = Visibility.Collapsed;
        }
        private void cmdShrinkage_Click(object sender, RoutedEventArgs e)
        {
            tensileStrength.Visibility = Visibility.Collapsed;
            elongation.Visibility = Visibility.Collapsed;
            adhesionForce.Visibility = Visibility.Collapsed;
            shrinkageForce.Visibility = Visibility.Collapsed;
            shrinkage.Visibility = Visibility.Visible;
            twisting1.Visibility = Visibility.Collapsed;
            twisting2.Visibility = Visibility.Collapsed;
            thickness.Visibility = Visibility.Collapsed;
            denier.Visibility = Visibility.Collapsed;
            rpu.Visibility = Visibility.Collapsed;
        }
        private void cmd1Twisting_Click(object sender, RoutedEventArgs e)
        {
            tensileStrength.Visibility = Visibility.Collapsed;
            elongation.Visibility = Visibility.Collapsed;
            adhesionForce.Visibility = Visibility.Collapsed;
            shrinkageForce.Visibility = Visibility.Collapsed;
            shrinkage.Visibility = Visibility.Collapsed;
            twisting1.Visibility = Visibility.Visible;
            twisting2.Visibility = Visibility.Collapsed;
            thickness.Visibility = Visibility.Collapsed;
            denier.Visibility = Visibility.Collapsed;
            rpu.Visibility = Visibility.Collapsed;
        }
        private void cmd2Twisting_Click(object sender, RoutedEventArgs e)
        {
            tensileStrength.Visibility = Visibility.Collapsed;
            elongation.Visibility = Visibility.Collapsed;
            adhesionForce.Visibility = Visibility.Collapsed;
            shrinkageForce.Visibility = Visibility.Collapsed;
            shrinkage.Visibility = Visibility.Collapsed;
            twisting1.Visibility = Visibility.Collapsed;
            twisting2.Visibility = Visibility.Visible;
            thickness.Visibility = Visibility.Collapsed;
            denier.Visibility = Visibility.Collapsed;
            rpu.Visibility = Visibility.Collapsed;
        }
        private void cmdThickness_Click(object sender, RoutedEventArgs e)
        {
            tensileStrength.Visibility = Visibility.Collapsed;
            elongation.Visibility = Visibility.Collapsed;
            adhesionForce.Visibility = Visibility.Collapsed;
            shrinkageForce.Visibility = Visibility.Collapsed;
            shrinkage.Visibility = Visibility.Collapsed;
            twisting1.Visibility = Visibility.Collapsed;
            twisting2.Visibility = Visibility.Collapsed;
            thickness.Visibility = Visibility.Visible;
            denier.Visibility = Visibility.Collapsed;
            rpu.Visibility = Visibility.Collapsed;
        }
        private void cmdDenier_Click(object sender, RoutedEventArgs e)
        {
            tensileStrength.Visibility = Visibility.Collapsed;
            elongation.Visibility = Visibility.Collapsed;
            adhesionForce.Visibility = Visibility.Collapsed;
            shrinkageForce.Visibility = Visibility.Collapsed;
            shrinkage.Visibility = Visibility.Collapsed;
            twisting1.Visibility = Visibility.Collapsed;
            twisting2.Visibility = Visibility.Collapsed;
            thickness.Visibility = Visibility.Collapsed;
            denier.Visibility = Visibility.Visible;
            rpu.Visibility = Visibility.Collapsed;
        }
        private void cmdRPU_Click(object sender, RoutedEventArgs e)
        {
            tensileStrength.Visibility = Visibility.Collapsed;
            elongation.Visibility = Visibility.Collapsed;
            adhesionForce.Visibility = Visibility.Collapsed;
            shrinkageForce.Visibility = Visibility.Collapsed;
            shrinkage.Visibility = Visibility.Collapsed;
            twisting1.Visibility = Visibility.Collapsed;
            twisting2.Visibility = Visibility.Collapsed;
            thickness.Visibility = Visibility.Collapsed;
            denier.Visibility = Visibility.Collapsed;
            rpu.Visibility = Visibility.Visible;
        }
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            M3QAApp.Pages.GotoQAMainMenu();
        }
        #endregion

        #region Public Methods

        public void Setup()
        {
            
        }

        #endregion

    }
}
