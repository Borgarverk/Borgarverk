using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Borgarverk
{
	public class DecimalConverter: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			Debug.WriteLine("Converting");
			if (value is decimal)
				return value.ToString();
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			decimal dec;
			if (decimal.TryParse(value as string, out dec))
				return dec;
			return value;
		}
	}
}
