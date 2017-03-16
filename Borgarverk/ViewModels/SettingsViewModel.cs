using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Borgarverk.Models;
using Xamarin.Forms;

namespace Borgarverk.ViewModels
{
	public class SettingsViewModel: INotifyPropertyChanged
	{
		#region private properties
		private ObservableCollection<SelectableItemWrapper<CarModel>> cars;
		private ObservableCollection<SelectableItemWrapper<StationModel>> stations;
		private string newStation, newCar;
		//private readonly IDataService dataService;
		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		public Command DeleteCarsCommand { get; }
		public Command DeleteStationsCommand { get; }
		public Command AddCarCommand { get; }
		public Command AddStationCommand { get; }

		public SettingsViewModel()
		{
			GetCars();
			GetStations();
			DeleteCarsCommand = new Command(() => DeleteCars());
			DeleteStationsCommand = new Command(() => DeleteStations());
			AddCarCommand = new Command(() => AddCar());
			AddStationCommand = new Command(() => AddStation());
		}

		#region public properties
		public string NewCar
		{
			get { return newCar; }
			set
			{
				if (newCar != value)
				{
					newCar = value;
					OnPropertyChanged("NewCar");
				}
			}
		}

		public string NewStation
		{
			get { return newStation; }
			set
			{
				if (newStation != value)
				{
					newStation = value;
					OnPropertyChanged("NewStation");
				}
			}
		}

		public ObservableCollection<SelectableItemWrapper<CarModel>> Cars
		{
			get { return cars; }
			set { cars = value; OnPropertyChanged("Cars"); }
		}

		public ObservableCollection<SelectableItemWrapper<StationModel>> Stations
		{
			get { return stations; }
			set { stations = value; OnPropertyChanged("Stations"); }
		}
		#endregion

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var changed = PropertyChanged;
			if (changed != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private void GetCars()
		{
			var carsToConvert = new List<CarModel>(DataService.GetCars());
			Cars = new ObservableCollection<SelectableItemWrapper<CarModel>>
				(carsToConvert.Select(car => new SelectableItemWrapper<CarModel>(car)));
		}

		private void GetStations()
		{
			var stationsToConvert = new List<StationModel>(DataService.GetStations());
			Stations = new ObservableCollection<SelectableItemWrapper<StationModel>>
				(stationsToConvert.Select(station => new SelectableItemWrapper<StationModel>(station)));
		}

		// TODO Eyða ef ekki notað
		/*ObservableCollection<CarModel> GetSelectedCars()
		{
			var selected = Cars
				.Where(p => p.IsSelected)
				.Select(p => p.Item)
				.ToList();
			return new ObservableCollection<CarModel>(selected);
		}

		ObservableCollection<StationModel> GetSelectedStations()
		{
			var selected = Stations
				.Where(p => p.IsSelected)
				.Select(p => p.Item)
				.ToList();
			return new ObservableCollection<StationModel>(selected);
		}*/

		private void DeleteCars()
		{
			for (var i = Cars.Count - 1; i >= 0; i--)
			{
				if (Cars[i].IsSelected)
				{
					DataService.DeleteCar(Cars[i].Item.ID);
					Cars.RemoveAt(i);
				}
			}
		}

		private void DeleteStations()
		{
			for (var i = Stations.Count - 1; i >= 0; i--)
			{
				if (Stations[i].IsSelected)
				{
					DataService.DeleteStation(Stations[i].Item.ID);
					Stations.RemoveAt(i);
				}
			}
		}

		private void AddCar()
		{
			// If AddCarButton is clicked when entry is empty, nothing happens
			if (NewCar != null && NewCar.Length == 0)
			{
				return;
			}
			var nCar = new CarModel(NewCar);
			DataService.AddCar(nCar);
			Cars.Add(new SelectableItemWrapper<CarModel>(nCar));
			NewCar = ""; // Clear entry after saving
		}

		private void AddStation()
		{
			// If AddStationButton is clicked when entry is empty, nothing happens
			if (NewStation != null && NewStation.Length == 0)
			{
				return;
			}
			var nStation = new StationModel(NewStation);
			DataService.AddStation(nStation);
			Stations.Add(new SelectableItemWrapper<StationModel>(nStation));
			NewStation = ""; //Clear entry after saving
		}
	}
}
