using System;
using System.Globalization;
using System.Windows.Data;

namespace MineSweeper
{
	public class TotalSecondsConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if(value is TimeSpan timeSpan)
			{
				return timeSpan.TotalSeconds.ToString();
			}
			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}