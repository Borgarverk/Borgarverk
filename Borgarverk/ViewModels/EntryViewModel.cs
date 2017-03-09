using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Borgarverk.Models;
using Xamarin.Forms;

namespace Borgarverk.ViewModels
{
	public class EntryViewModel : INotifyPropertyChanged
	{
		#region private variables
		private string roadWidth = "", no = "", roadLength = "", roadArea = "", tarQty = "", rate = "";
		private CarModel car;
		private StationModel station;
		private DateTime? timeSent, timeCreated;
		private bool isValid = false;
		private readonly IDataService dataService;
		private readonly ISendService sendService;
		private ObservableCollection<CarModel> cars;
		private ObservableCollection<StationModel> stations;
		private INavigation navigation;
		private EntryModel model;
		#endregion

		public EntryViewModel(IDataService dService, INavigation navigation, ISendService sService)
		{
			ConfirmOneCommand = new Command(async () => await SaveEntry(), () => ValidEntry());
			this.dataService = dService;
			this.sendService = sService;
			this.navigation = navigation;
			this.cars = new ObservableCollection<CarModel>(dataService.GetCars());
			this.stations = new ObservableCollection<StationModel>(dataService.GetStations());
			model = new EntryModel();
		}

		public EntryViewModel(IDataService dService, INavigation navigation, ISendService sService, EntryModel m)
		{
			ConfirmOneCommand = new Command(async () => await SaveEntry(), () => ValidEntry());
			this.dataService = dService;
			this.sendService = sService;
			this.navigation = navigation;
			this.cars = new ObservableCollection<CarModel>(dataService.GetCars());
			this.stations = new ObservableCollection<StationModel>(dataService.GetStations());
			model = m;
		}

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
					model.Car = car.Num;
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
					model.Station = station.Name;
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
					model.No = no;
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
					model.RoadWidth = roadWidth;
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
					model.RoadLength = roadLength;
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
					model.RoadArea = roadArea;
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
					model.TarQty = tarQty;
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
					model.Rate = rate;
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

		public EntryModel Model
		{
			get { return model; }
			set
			{
				if (model != value)
				{
					model = value;
					ValidEntry();
					OnPropertyChanged("Model");
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

		public bool ValidEntry()
		{
			bool valid = (No.Length > 0) &&
				(RoadLength.Length > 0) &&
				(RoadWidth.Length > 0) &&
				(RoadArea.Length > 0) &&
				(TarQty.Length > 0) &&
				(Rate.Length > 0) &&
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
				model.TimeCreated = DateTime.Now;

				if (sendService.SendEntry(model))
				{
					model.TimeSent = DateTime.Now;
					model.Sent = true;
					// Var það þannig að ef að það er entry i database-inum 
					//með sama ID þá er ekki insertað heldur update-að?
					dataService.AddEntry(model);
				}
				else
				{
					model.TimeSent = null;
					model.Sent = false;
					dataService.AddEntry(model);
				}

				await navigation.PopAsync();
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
