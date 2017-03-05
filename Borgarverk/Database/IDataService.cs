using System;
using System.Collections.Generic;
using Borgarverk.Models;

namespace Borgarverk
{
	public interface IDataService
	{
		IEnumerable<EntryModel> GetEntries();
		EntryModel GetEntry(int id);
		int DeleteEntry(int id);
		int AddEntry(EntryModel model);
		int DeleteEntries();
		int UpdateEntry(EntryModel model);
		int AddCar(CarModel car);
		int DeleteCar(int id);
		IEnumerable<CarModel> GetCars();
		CarModel GetCar(int id);
		int DeleteCars();
		int AddStation(StationModel car);
		int DeleteStation(int id);
		IEnumerable<StationModel> GetStations();
		StationModel GetStation(int id);
		int DeleteStations();
	}
}
