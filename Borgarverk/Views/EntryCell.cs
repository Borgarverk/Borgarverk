using System;

using Xamarin.Forms;

namespace Borgarverk.Views
{
	// Þurfum ekkert að hafa þennan, en var að hugsa hann fyrir "allar færslur" vieweið
	public class EntryCell : ViewCell
	{
		public static readonly BindableProperty CarProperty =
			BindableProperty.Create("Car", typeof(string), typeof(EntryCell), "");
		
		public string Car
		{
			get { return (string)GetValue(CarProperty); }
			set { SetValue(CarProperty, value); }
		}

		public static readonly BindableProperty PlaceProperty =
			BindableProperty.Create("Place", typeof(string), typeof(EntryCell), "");

		public string Place
		{
			get { return (string)GetValue(PlaceProperty); }
			set { SetValue(PlaceProperty, value); }
		}
	}
}

