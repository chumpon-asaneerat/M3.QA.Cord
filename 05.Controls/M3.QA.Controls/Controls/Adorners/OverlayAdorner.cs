#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;

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

        #region Override Methods

        // A common way to implement an adorner's rendering behavior is to override the OnRender
        // method, which is called by the layout system as part of a rendering pass.
        protected override void OnRender(DrawingContext drawingContext)
        {
            /*
            Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);

            // Some arbitrary drawing implements.
            SolidColorBrush renderBrush = new SolidColorBrush(Colors.Green);
            renderBrush.Opacity = 0.2;
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
            double renderRadius = 5.0;

            // Draw a circle at each corner.
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius, renderRadius);
            */
        }

        #endregion
    }

    #endregion
}
