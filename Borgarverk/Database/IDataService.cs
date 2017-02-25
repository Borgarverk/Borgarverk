using System;
using System.Collections.Generic;

namespace Borgarverk
{
	public interface IDataService
	{
		IEnumerable<EntryModel> GetEntries();
		EntryModel GetEntry(int id);
		void DeleteEntry(int id);
		void AddEntry(EntryModel model);
		void DeleteEntries();
		void AddCar(CarModel car);
		void DeleteCar(int id);
		IEnumerable<CarModel> GetCars();
		CarModel GetCar(int id);
		void DeleteCars();
		void AddStation(StationModel car);
		void DeleteStation(int id);
		IEnumerable<StationModel> GetStations();
		StationModel GetStation(int id);
		void DeleteStations();
	}
}
