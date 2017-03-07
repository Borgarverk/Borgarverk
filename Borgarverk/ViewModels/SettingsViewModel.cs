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
		private ObservableCollection<CarModel> selectedCars;
		private ObservableCollection<StationModel> selectedStations;
		private string newStation, newCar;
		private readonly IDataService dataService;
		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		public Command DeleteCarsCommand { get; }
		public Command DeleteStationsCommand { get; }

		public SettingsViewModel(IDataService dService)
		{
			this.dataService = dService;
			GetCars();
			GetStations();
			DeleteCarsCommand = new Command(async () => await DeleteCars());
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

		public ObservableCollection<CarModel> SelectedCars
		{
			get { return selectedCars; }
			private set { selectedCars = value; OnPropertyChanged("SelectedCars"); }
		}

		public ObservableCollection<SelectableItemWrapper<StationModel>> Stations
		{
			get { return stations; }
			set { stations = value; OnPropertyChanged("Stations"); }
		}

		public ObservableCollection<StationModel> SelectedStations
		{
			get { return selectedStations; }
			private set { selectedStations = value; OnPropertyChanged("SelectedStations"); }
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
			var carsToConvert = new List<CarModel>(dataService.GetCars());
			Cars = new ObservableCollection<SelectableItemWrapper<CarModel>>
				(carsToConvert.Select(car => new SelectableItemWrapper<CarModel>(car)));
			foreach (var i in Cars)
			{
				Debug.WriteLine(i.Item.ID);
			}
		}

		private void GetStations()
		{
			var stationsToConvert = new List<StationModel>(dataService.GetStations());
			Stations = new ObservableCollection<SelectableItemWrapper<StationModel>>
				(stationsToConvert.Select(station => new SelectableItemWrapper<StationModel>(station)));
		}

		ObservableCollection<CarModel> GetSelectedCars()
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
		}

		async Task DeleteCars()
		{
			Debug.WriteLine("IN DELETE CARS!!");
			foreach (var car in GetSelectedCars())
			{
				Debug.WriteLine(car.ID);
				Debug.WriteLine("Deleted? " + dataService.DeleteCar(car.ID).ToString());
			}
		}
	}
}
