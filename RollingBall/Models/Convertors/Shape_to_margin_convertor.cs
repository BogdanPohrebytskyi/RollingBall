using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

using RollingBall.Models;

namespace RollingBall
{
    class Shape_to_margin_convertor : IValueConverter
    {
        int point_size = 5;
        int line_thicknes = 2;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new Thickness(-10,-10, 0, 0);
            if (value.GetType() == typeof(Point))
            {
                return new Thickness(((Point)value).X - point_size / 2, ((Point)value).Y - point_size / 2, 0, 0);
            }
            if(value.GetType() == typeof(double))
            {
                double result = ((double)value) - line_thicknes / 2;
                return result;
            }
            throw new Exception();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}