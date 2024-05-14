using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.WpfExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfSample.Converter
{
    [ValueConversion(typeof(Mat), typeof(BitmapSource))]
    public class MatToBitmapSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Mat mat && mat is not null && mat.Width > 0 && mat.Height > 0)
            {
                try
                {
                    using (var image = mat.ToBitmap())
                    {
                        return BitmapSourceConverter.ToBitmapSource(image);
                    }

                }
                catch (Exception ex)
                {
#if DEBUG
                    Debug.WriteLine("MatToBitmapSource(): " + ex.Message);
#endif
                    return null;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("MatToBitmapSourceConverter is a one-way converter and cannot be used for ConvertBack.");
        }
    }
}
