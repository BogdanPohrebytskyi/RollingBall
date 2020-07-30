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
    class bool_to_visible_convertor : IValueConverter
    {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.GetType() != typeof(bool))
            throw new Exception();
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
}