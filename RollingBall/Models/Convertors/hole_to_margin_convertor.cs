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
    class hole_to_margin_convertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(Hole))
                throw new Exception();
            return new Thickness(((Hole)value).position.X - ((Hole)value).radius, ((Hole)value).position.Y - ((Hole)value).radius, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
