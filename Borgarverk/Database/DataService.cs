using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Borgarverk.Models;
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
			//database.DropTable<EntryModel>();
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

		public int DeleteEntry(int id)
		{
			lock(locker)
			{
				return database.Delete<EntryModel>(id);
			}

		}

		public int AddEntry(EntryModel model)
		{
			lock(locker)
			{
				return database.Insert(model);
			} 	
		}

		public int UpdateEntry(EntryModel model)
		{
			var currModel = database.Table<EntryModel>().FirstOrDefault(t => t.ID == model.ID);
			currModel.Car = model.Car;
			currModel.Station = model.Station;
			currModel.No = model.No;
			currModel.RoadWidth = model.RoadArea;
			currModel.RoadLength = model.RoadLength;
			currModel.RoadArea = model.RoadArea;
			currModel.TarQty = model.TarQty;
			currModel.Rate = model.Rate;
			currModel.TimeCreated = model.TimeCreated;
			currModel.Sent = model.Sent;
			currModel.TimeSent = model.TimeSent;
			return database.Update(currModel);
		}

		public int DeleteEntries()
		{
			lock (locker)
			{
				return database.DeleteAll<EntryModel>();
			}
		}

		#endregion

		#region car functions
		public int  AddCar(CarModel car)
		{
			lock(locker)
			{
				return database.Insert(car);
			}
			
		}

		public int DeleteCar(int id)
		{
			lock (locker)
			{
				return database.Delete<CarModel>(id);			
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

		public int DeleteCars()
		{
			lock(locker)
			{
				return database.DeleteAll<CarModel>();
			}
		}
		#endregion

		#region station functions
		public int AddStation(StationModel station)
		{
			lock (locker)
			{
				return database.Insert(station);
			}

		}

		public int DeleteStation(int id)
		{
			lock (locker)
			{
				return database.Delete<StationModel>(id);
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

		public int DeleteStations()
		{
			lock (locker)
			{
				return database.DeleteAll<StationModel>();
			}
		}
		#endregion

	}
}
