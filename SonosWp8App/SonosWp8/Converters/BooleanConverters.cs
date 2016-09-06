using System;
using System.Globalization;
using System.Windows.Data;

namespace SonosWp8.Converters
{
    public class BooleanInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bValue = false;
            if (value is bool)
            {
                bValue = (bool) value;
            }
            return !(bValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof (double))
                throw new InvalidOperationException("The target must be a boolean");

            var b = value as bool?;
            if (b != null && b.Value)
            {
                return 0.2;
            }
            return 1.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}