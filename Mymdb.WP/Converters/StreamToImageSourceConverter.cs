using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Mymdb.WP.Converters
{
    public class StreamToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is MemoryStream))
                return new BitmapImage();

            var imageSource = new BitmapImage();
            imageSource.SetSource(value as MemoryStream);
            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
