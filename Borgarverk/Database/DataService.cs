using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace Borgarverk
{
	public class DataService : IDataService
	{
		private SQLiteConnection database;

		public DataService()
		{
			database = DependencyService.Get<ISQLite>().GetConnection();
			database.CreateTable<EntryModel>();
		}

		public IEnumerable<EntryModel> GetEntries()
		{
			return (from t in database.Table<EntryModel>()
					select t).ToList();
		}

		public EntryModel GetEntry(int id)
		{
			return database.Table<EntryModel>().FirstOrDefault(t => t.ID == id);
		}

		public void DeleteEntry(int id)
		{
			database.Delete<EntryModel>(id);
		}

		public void AddEntry(EntryModel model)
		{
			model.TimeSent = DateTime.Now;
			database.Insert(model);
		}
	}
}
