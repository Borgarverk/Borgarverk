using System;
using Xamarin.Forms;

namespace Borgarverk
{
	public class BooleanToColorConverter: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if ((bool)value)
			{
				return Color.Gray;
			}
			else
			{
				return Color.White;
			}
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}
