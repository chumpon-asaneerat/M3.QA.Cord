#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;

#endregion

namespace M3.QA.Controls
{
    #region AttachedAdorner

    /// <summary>
    /// The Attached Adorner class.
    /// </summary>
    public class AttachedAdorner
    {
        #region Adorner

        public static readonly DependencyProperty AdornerProperty =
           DependencyProperty.RegisterAttached(
              nameof(Adorner), typeof(Type), typeof(AttachedAdorner),
              new FrameworkPropertyMetadata(default(Type), PropertyChangedCallback));

        public static void SetAdorner(DependencyObject element, Type value)
        {
            element.SetValue(AdornerProperty, value);
        }

        public static Type GetAdorner(DependencyObject element)
        {
            return (Type)element.GetValue(AdornerProperty);
        }

        private static void PropertyChangedCallback(
           DependencyObject dependencyObject,
           DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var frameworkElement = dependencyObject as FrameworkElement;
            if (frameworkElement != null)
            {
                frameworkElement.Loaded += Loaded;
            }
        }

        #endregion

        #region Loaded

        private static void Loaded(object sender, RoutedEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement != null)
            {
                var adorner = Activator.CreateInstance(GetAdorner(frameworkElement), frameworkElement) as Adorner;
                if (adorner != null)
                {
                    var adornerLayer = AdornerLayer.GetAdornerLayer(frameworkElement);
                    adornerLayer.Add(adorner);
                }
            }
        }

        #endregion
    }

    #endregion
}
