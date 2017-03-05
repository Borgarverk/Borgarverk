using System;
using SQLite;

namespace Borgarverk.Models
{
	public class StationModel
	{
		#region private variables
		private string name;
		#endregion

		#region properties
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		#endregion

		public StationModel(string n)
		{
			this.Name = n;
		}
		public StationModel()
		{
		}

	}
}
