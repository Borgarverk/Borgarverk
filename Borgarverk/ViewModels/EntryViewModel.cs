using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Borgarverk.Models;
using Xamarin.Forms;

namespace Borgarverk.ViewModels
{
	public class EntryViewModel : INotifyPropertyChanged
	{
		#region private variables
		private string roadWidth = "", no = "", roadLength = "", roadArea = "", tarQty = "", rate = "";
		private CarModel car = null;
		private StationModel station = null;
		private DateTime? timeSent, timeCreated;
		private bool isValid = false;
		private readonly ISendService sendService;
		private ObservableCollection<CarModel> cars;
		private ObservableCollection<StationModel> stations;
		private INavigation navigation;
		#endregion

		// For Testing
		public EntryViewModel(ISendService sService)
		{
			ConfirmOneCommand = new Command(async () => await SaveEntry(), () => ValidEntry());
			this.sendService = sService;
			//this.navigation = navigation;
			this.cars = new ObservableCollection<CarModel>();
			this.stations = new ObservableCollection<StationModel>();
		}

		public EntryViewModel(INavigation navigation, ISendService sService)
		{
			ConfirmOneCommand = new Command(async () => await SaveEntry(), () => ValidEntry());
			this.sendService = sService;
			this.navigation = navigation;
			this.cars = new ObservableCollection<CarModel>(DataService.GetCars());
			this.stations = new ObservableCollection<StationModel>(DataService.GetStations());
		}

		// TODO Er þetta nauðsynlegt?
		/*public EntryViewModel(INavigation navigation, ISendService sService, EntryModel m)
		{
			ConfirmOneCommand = new Command(async () => await SaveEntry(), () => ValidEntry());
			//this.dataService = dService;
			this.sendService = sService;
			this.navigation = navigation;
			this.cars = new ObservableCollection<CarModel>(DataService.GetCars());
			this.stations = new ObservableCollection<StationModel>(DataService.GetStations());
		}*/

		public event PropertyChangedEventHandler PropertyChanged;
		public Command ConfirmOneCommand { get; }

		#region properties
		public CarModel Car
		{
			get { return car; }
			set
			{
				if (car != value)
				{
					car = value;
					ValidEntry();
					OnPropertyChanged("Car");
				}
			}
		}

		public StationModel Station
		{
			get { return station; }
			set
			{
				if (station != value)
				{
					station = value;
					ValidEntry();
					OnPropertyChanged("Station");
				}
			}
		}

		public string No
		{
			get { return no; }
			set
			{
				if (no != value)
				{
					no = value;
					ValidEntry();
					OnPropertyChanged("No");
				}
			}
		}

		public string RoadWidth
		{
			get { return roadWidth; }
			set
			{
				if (roadWidth != value)
				{
					roadWidth = value;
					ValidEntry();
					OnPropertyChanged("RoadWidth");
				}
			}
		}

		public string RoadLength
		{
			get { return roadLength; }
			set
			{
				if (roadLength != value)
				{
					roadLength = value;
					ValidEntry();
					OnPropertyChanged("RoadLength");
				}
			}
		}

		public string RoadArea
		{
			get { return roadArea; }
			set
			{
				if (roadArea != value)
				{
					roadArea = value;
					ValidEntry();
					OnPropertyChanged("RoadArea");
				}
			}
		}

		public string TarQty
		{
			get { return tarQty; }
			set
			{
				if (tarQty != value)
				{
					tarQty = value;
					ValidEntry();
					OnPropertyChanged("TarQty");
				}
			}
		}

		public string Rate
		{
			get { return rate; }
			set
			{
				if (rate != value)
				{
					rate = value;
					ValidEntry();
					OnPropertyChanged("Rate");
				}
			}
		}

		public DateTime? TimeSent
		{
			get { return timeSent; }
			set
			{
				if (timeSent != value)
				{
					timeSent = value;
					OnPropertyChanged("TimeSent");
				}
			}
		}

		public DateTime? TimeCreated
		{
			get { return timeCreated; }
			set
			{
				if (timeCreated != value)
				{
					timeCreated = value;
					OnPropertyChanged("TimeSent");
				}
			}
		}

		public bool IsValid
		{
			get { return isValid; }
			set 
			{
				if (isValid != value)
				{
					isValid = value;
					OnPropertyChanged("IsValid");
					ConfirmOneCommand.ChangeCanExecute();
				}
			}
		}

		public ObservableCollection<CarModel> Cars
		{
			get
			{
				return cars;
			}
			set
			{
				cars = value;
			}
		}

		public ObservableCollection<StationModel> Stations
		{
			get
			{
				return stations;
			}
			set
			{
				stations = value;

			}
		}

		#endregion

		// No entry can be empty and numeric values cannot be less then or equal to 0
		public bool ValidEntry()
		{
			bool valid = (No.Length > 0) &&
				(RoadLength.Length > 0) &&
				(Int32.Parse(RoadLength) > 0) &&
				(RoadWidth.Length > 0) &&
				(Int32.Parse(RoadWidth) > 0) &&
				(RoadArea.Length > 0) &&
				(Int32.Parse(RoadArea) > 0) &&
				(TarQty.Length > 0) &&
				(Int32.Parse(TarQty) > 0) &&
				(Rate.Length > 0) &&
				(Int32.Parse(Rate) > 0) &&
				(Car != null) &&
				(Station != null );
			IsValid = valid;
			OnPropertyChanged("IsValid");
			return valid;
		}

		async Task SaveEntry()
		{
			var confirmed = await Application.Current.MainPage.DisplayAlert("Confirmation", "Staðfesta sendingu forms?", "Já", "Nei");
			if (confirmed)
			{
				EntryModel model = new EntryModel();
				model.Car = Car.Num;
				model.Station = Station.Name;
				model.No = No;
				model.RoadWidth = RoadWidth;
				model.RoadLength = RoadLength;
				model.RoadArea = RoadArea;
				model.TarQty = TarQty;
				model.Rate = Rate;
				model.TimeCreated = DateTime.Now;

				if (sendService.SendEntry(model))
				{
					model.TimeSent = DateTime.Now;
					model.Sent = true;
					// Var það þannig að ef að það er entry i database-inum 
					//með sama ID þá er ekki insertað heldur update-að?
					DataService.AddEntry(model);
				}
				else
				{
					model.TimeSent = null;
					model.Sent = false;
					DataService.AddEntry(model);
				}

				await navigation.PopAsync(false);
			}
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var changed = PropertyChanged;
			if (changed != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
