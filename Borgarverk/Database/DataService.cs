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
		static object locker = new object();
		private SQLiteConnection database;

		// TODO: Setja inn kóða þannig að ef búinn er til db í fyrsta skiptið fylla þessi gildi inn
		public DataService()
		{
			database = DependencyService.Get<ISQLite>().GetConnection();
			database.CreateTable<EntryModel>();
			database.CreateTable<CarModel>();
			database.CreateTable<StationModel>();
			//database.DeleteAll<EntryModel>();
			DeleteCars();
			AddCar(new CarModel("ML-455"));
			AddCar(new CarModel("MU-510"));
			AddCar(new CarModel("BZ-963"));
			AddCar(new CarModel("US-553"));
			AddCar(new CarModel("AZ-R93"));
			DeleteStations();
			AddStation(new StationModel("Akureyri"));
			AddStation(new StationModel("Hlaðbær Colas"));
			AddStation(new StationModel("Höfði"));
			AddStation(new StationModel("Ísafjörður"));
			AddStation(new StationModel("Reyðarfjörður"));
			AddStation(new StationModel("Sauðárkrókur"));
		}

		#region Entry functons
		public IEnumerable<EntryModel> GetEntries()
		{
			lock(locker)
			{
				return (from t in database.Table<EntryModel>()
						select t).ToList();
			}
		}

		public EntryModel GetEntry(int id)
		{
			lock (locker)
			{
				return database.Table<EntryModel>().FirstOrDefault(t => t.ID == id);
			}
		}

		public void DeleteEntry(int id)
		{
			lock(locker)
			{
				database.Delete<EntryModel>(id);
			}

		}

		public void AddEntry(EntryModel model)
		{
			model.TimeSent = DateTime.Now;
			model.Sent = false;
			lock(locker)
			{
				database.Insert(model);
			}

		}

		public void DeleteEntries()
		{
			lock (locker)
			{
				database.DeleteAll<EntryModel>();
			}
		}
		#endregion

		#region car functions
		public void AddCar(CarModel car)
		{
			lock(locker)
			{
				database.Insert(car);
			}
			
		}

		public void DeleteCar(int id)
		{
			lock (locker)
			{
				database.Delete<CarModel>(id);			
			}

		}

		public IEnumerable<CarModel> GetCars()
		{
			lock(locker)
			{
				return (from t in database.Table<CarModel>()
						select t).ToList();
			}
		}

		public CarModel GetCar(int id)
		{
			lock (locker)
			{
				return database.Table<CarModel>().FirstOrDefault(t => t.ID == id);
			}
		}

		public void DeleteCars()
		{
			lock(locker)
			{
				database.DeleteAll<CarModel>();
			}
		}
		#endregion

		#region station functions
		public void AddStation(StationModel station)
		{
			lock (locker)
			{
				database.Insert(station);
			}

		}

		public void DeleteStation(int id)
		{
			lock (locker)
			{
				database.Delete<StationModel>(id);
			}

		}

		public IEnumerable<StationModel> GetStations()
		{
			lock(locker)
			{
				return (from t in database.Table<StationModel>()
						select t).ToList();
			}
		}

		public StationModel GetStation(int id)
		{
			lock (locker)
			{
				return database.Table<StationModel>().FirstOrDefault(t => t.ID == id);
			}
		}

		public void DeleteStations()
		{
			lock (locker)
			{
				database.DeleteAll<StationModel>();
			}
		}
		#endregion

	}
}
