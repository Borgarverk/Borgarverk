using System;
namespace Borgarverk
{
	public class CarModel
	{
		#region private variables
		private string num;
		#endregion

		#region properties
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
	}
}
