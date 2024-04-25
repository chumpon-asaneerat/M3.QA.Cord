#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;
using System.Reflection.Emit;

#endregion

namespace M3.QA.Controls
{
    #region OverlayAdorner

    /// <summary>
    /// The OverlayAdorner class.
    /// Adorners must subclass the abstract base class Adorner.
    /// </summary>
    public class OverlayAdorner : Adorner
    {
        #region  Constructor

        /// <summary>
        /// Constructor. required to call base class.
        /// </summary>
        /// <param name="adornedElement"></param>
        public OverlayAdorner(UIElement adornedElement)
          : base(adornedElement)
        {
            IsHitTestVisible = false;
        }

        #endregion

        #region Static Fields

        private static CultureInfo culture = new CultureInfo("en-US");
        private static FlowDirection flow = FlowDirection.LeftToRight;
        private static Typeface wingdings2 = new Typeface("Wingdings 2");
        private static double fontSize = 20;
        private static double pixelPerDip = 100;

        #endregion

        #region Private Methods

        private void DrawText(DrawingContext drawingContext, string text, SolidColorBrush brush, Point pt)
        {
            if (null == drawingContext)
                return;
            FormattedText fmt = new FormattedText(text, culture, flow, wingdings2, fontSize, brush, pixelPerDip);
            drawingContext.DrawText(fmt, pt);
        }

        #endregion

        #region Override Methods

        // A common way to implement an adorner's rendering behavior is to override the OnRender
        // method, which is called by the layout system as part of a rendering pass.
        protected override void OnRender(DrawingContext drawingContext)
        {
            var ctrl = (null != this.AdornedElement && this.AdornedElement is FrameworkElement) ? 
                (FrameworkElement)this.AdornedElement : null;
            if (null == ctrl || ctrl.Visibility != Visibility.Visible)
                return; // Not visible so skip.

            var item = (null != ctrl && null != ctrl.DataContext) ?
                ctrl.DataContext as Models.NRTestPropertyItem : null;

            if (null == item) return;

            Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);
            Point pt = new Point(adornedElementRect.Right - 20, adornedElementRect.Top);
            
            this.DrawText(drawingContext, "P", Brushes.ForestGreen, pt);
        }

        #endregion
    }

    #endregion
}
