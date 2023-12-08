#region Using

using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

#endregion

namespace NLib.Wpf.Controls.Converters
{
    /// <summary>
    /// The String To Number Converter class.
    /// </summary>
    public class WatermarkAlignmentToDockConverter : IValueConverter
    {
        /// <summary>
        /// Convert.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WatermarkImageAlignment align = (WatermarkImageAlignment)value;
            if (align == WatermarkImageAlignment.Left)
            {
                return Dock.Left;
            }
            else
            {
                return Dock.Right;
            }
        }
        /// <summary>
        /// ConvertBack.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
