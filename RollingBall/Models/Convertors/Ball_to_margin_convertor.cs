using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Data;
using RollingBall.Models;
using System.Globalization;
using System.Windows;

namespace RollingBall
{
    class Ball_to_margin_convertor : IValueConverter
    {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value.GetType() != typeof(Ball))
                    throw new Exception();
                return new Thickness( ((Ball)value).position.X-((Ball)value).radius, ((Ball)value).position.Y - ((Ball)value).radius, 0, 0);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
    }
}
