using System;
using SQLite;

namespace Borgarverk
{
	public class CarModel
	{
		#region private variables
		private string num;
		#endregion

		#region properties
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Num
		{
			get { return num; }
			set { num = value; }
		}
		#endregion

		public CarModel(string n)
		{
			this.Num = n;
		}

		public CarModel()
		{
		}
	}
}
