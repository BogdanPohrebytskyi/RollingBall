using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RollingBall.Models;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RollingBall
{
    class hole_size_convertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(double))
                throw new Exception();
            return (double)value*2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}