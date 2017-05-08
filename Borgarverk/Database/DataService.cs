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
	public static class DataService // : IDataService
	{
		static object locker = new object();
		private static SQLiteConnection database;

		// TODO: Setja inn kóða þannig að ef búinn er til db í fyrsta skiptið fylla þessi gildi inn
		public static void OpenConnection()
		{
			if (database == null)
			{
				database = DependencyService.Get<ISQLite>().GetConnection();
				database.CreateTable<EntryModel>();
				database.CreateTable<CarModel>();
				database.CreateTable<StationModel>();
				if (GetCars().Count() == 0)
				{
					AddCar(new CarModel("ML-455"));
					AddCar(new CarModel("MU-510"));
					AddCar(new CarModel("BZ-963"));
					AddCar(new CarModel("US-553"));
					AddCar(new CarModel("AZ-R93"));
				}
				if (GetStations().Count() == 0)
				{
					AddStation(new StationModel("Akureyri"));
					AddStation(new StationModel("Hlaðbær Colas"));
					AddStation(new StationModel("Höfði"));
					AddStation(new StationModel("Ísafjörður"));
					AddStation(new StationModel("Reyðarfjörður"));
					AddStation(new StationModel("Sauðárkrókur"));
				}
			}
		}

		#region Entry functions
		public static IEnumerable<EntryModel> GetEntries()
		{
			lock(locker)
			{
				return (from t in database.Table<EntryModel>()
				        select t).OrderByDescending(x => x.TimeCreated).ToList();
			}
		}

		public static EntryModel GetEntry(int id)
		{
			lock (locker)
			{
				return database.Table<EntryModel>().FirstOrDefault(t => t.ID == id);
			}
		}

		public static int DeleteEntry(int id)
		{
			lock(locker)
			{
				var ret = database.Delete<EntryModel>(id);
				return ret;
			}
		}

		public static int AddEntry(EntryModel model)
		{
			lock(locker)
			{
				var check = database.Table<EntryModel>().FirstOrDefault(t => t.ID == model.ID);
				if (check == null)
				{
					var inserted = database.Insert(model);
					if (inserted == 1)
					{
						return model.ID;
					}
					else
					{
						return 0;
					}
				}
				else
				{
					return UpdateEntry(model);
				}
			} 	
		}

		public static int UpdateEntry(EntryModel model)
		{
			var currModel = database.Table<EntryModel>().FirstOrDefault(t => t.ID == model.ID);
			currModel.Car = model.Car;
			currModel.Station = model.Station;
			currModel.No = model.No;
			currModel.JobNo = model.JobNo;
			currModel.RoadWidth = model.RoadWidth;
			currModel.RoadLength = model.RoadLength;
			currModel.RoadArea = model.RoadArea;
			currModel.TarQty = model.TarQty;
			currModel.Rate = model.Rate;
			currModel.Degrees = model.Degrees;
			currModel.Comment = model.Comment;
			currModel.TimeCreated = model.TimeCreated;
			currModel.Sent = model.Sent;
			currModel.TimeSent = model.TimeSent;
			currModel.StartTime = model.StartTime;
			currModel.EndTime = model.EndTime;
			return database.Update(currModel);
		}

		public static int DeleteEntries()
		{
			lock (locker)
			{
				return database.DeleteAll<EntryModel>();
			}
		}

		#endregion

		#region car functions
		public static int  AddCar(CarModel car)
		{
			lock(locker)
			{
				var carCheck = database.Table<CarModel>().FirstOrDefault(t => t.Num == car.Num);
				if (carCheck == null)
				{
					return database.Insert(car);
				}
				else
				{
					return 0;
				}
			}
			
		}

		public static int DeleteCar(int id)
		{
			lock (locker)
			{
				return database.Delete<CarModel>(id);			
			}

		}

		public static IEnumerable<CarModel> GetCars()
		{
			lock(locker)
			{
				return (from t in database.Table<CarModel>()
						select t).ToList();
			}
		}

		public static CarModel GetCar(int id)
		{
			lock (locker)
			{
				return database.Table<CarModel>().FirstOrDefault(t => t.ID == id);
			}
		}

		public static int DeleteCars()
		{
			lock(locker)
			{
				return database.DeleteAll<CarModel>();
			}
		}
		#endregion

		#region station functions
		public static int AddStation(StationModel station)
		{
			lock (locker)
			{
				return database.Insert(station);
			}

		}

		public static int DeleteStation(int id)
		{
			lock (locker)
			{
				return database.Delete<StationModel>(id);
			}

		}

		public static IEnumerable<StationModel> GetStations()
		{
			lock(locker)
			{
				return (from t in database.Table<StationModel>()
						select t).ToList();
			}
		}

		public static StationModel GetStation(int id)
		{
			lock (locker)
			{
				return database.Table<StationModel>().FirstOrDefault(t => t.ID == id);
			}
		}

		public static int DeleteStations()
		{
			lock (locker)
			{
				return database.DeleteAll<StationModel>();
			}
		}
		#endregion

	}
}
