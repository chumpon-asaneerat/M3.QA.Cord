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

namespace TestDynamicNestSource.Controls
{
    /// <summary>
    /// The VerticalInputContainer control.
    /// </summary>
    public class VerticalInputContainer : Control
    {
        static VerticalInputContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VerticalInputContainer), new FrameworkPropertyMetadata(typeof(VerticalInputContainer)));
        }
    }
}
