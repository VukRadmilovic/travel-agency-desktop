using System;
using System.Globalization;
using System.Windows.Data;

namespace Tourist_Agency.ValueConverters
{
    class MenuItemFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var height = (double)value;
            return .53 * height;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
