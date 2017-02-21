using System;
namespace Borgarverk
{
	public class StationModel
	{
		#region private variables
		private string name;
		#endregion

		#region properties
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

	}
}
