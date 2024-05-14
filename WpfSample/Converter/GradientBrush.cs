using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WpfSample.Model;

namespace WpfSample.Converter
{
    [ValueConversion(typeof(PseudoColor.Type), typeof(LinearGradientBrush))]
    public class GradientBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PseudoColor.Type Type)
            {
                LinearGradientBrush gradientBrush = new LinearGradientBrush();
                gradientBrush.StartPoint = new System.Windows.Point(0, 0);
                gradientBrush.EndPoint = new System.Windows.Point(0, 1);

                switch (Type)
                {
                    case PseudoColor.Type.Gray:
                        gradientBrush = SetGradientBrush(Gray);
                        break;
                    case PseudoColor.Type.Rainbow:
                        gradientBrush = SetGradientBrush(Jet);
                        break;
                    case PseudoColor.Type.Inferno:
                        gradientBrush = SetGradientBrush(Inferno);
                        break;
                    case PseudoColor.Type.Virdis:
                        gradientBrush = SetGradientBrush(Virdis);
                        break;

                    default:
                        break;
                }

                return gradientBrush;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private LinearGradientBrush SetGradientBrush(string[] strings)
        {
            int count = strings.Count() - 1;
            double countDivider = 1.0 / (double)count;
            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new System.Windows.Point(0, 0);
            gradientBrush.EndPoint = new System.Windows.Point(0, 1);

            double index = 1;
            foreach (var @string in strings)
            {
                Color color = (Color)ColorConverter.ConvertFromString(@string);
                gradientBrush.GradientStops.Add(new GradientStop(color, index));
                index = index - countDivider;
            }

            return gradientBrush;
        }

        private string[] Gray = { "#000000", "#FFFFFF" };
        private string[] Jet = { "#000189", "#0090FF", "#81FF7C", "#FF7500", "#8F0001" };
        private string[] Inferno = { "#020108", "#5E126E", "#BD3853", "#F88B0B", "#F5FA96" };
        private string[] Virdis = { "#45085B", "#38568C", "#1F928B", "#50C46A", "#EFE51C" };
    }
}
