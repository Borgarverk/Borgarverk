using System;
using System.IO;
using Xamarin.Forms;
using Borgarverk.Droid;
using SQLite;

[assembly: Dependency(typeof(SQLiteAndroid))]
namespace Borgarverk.Droid
{
	public class SQLiteAndroid : ISQLite
	{
		public SQLiteAndroid()
		{
		}

		#region ISQLite implementation

		public SQLiteConnection GetConnection()
		{
			var dbName = "BorgarverkDatabase.db3";
			var path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);
			return new SQLiteConnection(path);
		}
		#endregion
	}
}
