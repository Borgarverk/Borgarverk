using System;
namespace Borgarverk
{
	public class EntryViewModel
	{
		#region private variables
		private string car, place;
		#endregion

		#region properties
		public string Car
		{
			get { return car; }
			set { car = value; }
		}

		public string Place
		{
			get { return place; }
			set { place = value; }
		}
		#endregion

		public EntryViewModel()
		{
		}
	}
}
