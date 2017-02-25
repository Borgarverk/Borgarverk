using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Borgarverk
{
	public class EntryViewModel : INotifyPropertyChanged
	{
		#region private variables
		private string roadWidth = "", no = "", roadLength = "", roadArea = "", tarQty = "", rate = "";
		private CarModel car;
		private StationModel station;
		private DateTime timeSent, timeCreated;
		private bool isValid = false;
		private readonly IDataService dataService;
		private ObservableCollection<CarModel> cars;
		private ObservableCollection<StationModel> stations;
		#endregion

		public EntryViewModel(IDataService service)
		{
			ConfirmOneCommand = new Command(async () => await SaveEntry(), () => ValidEntry());
			this.dataService = service;
			cars = new ObservableCollection<CarModel>(dataService.GetCars());
			stations = new ObservableCollection<StationModel>(dataService.GetStations());
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

		public DateTime TimeSent
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

		public DateTime TimeCreated
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
					if (IsValid)
					{
						Debug.WriteLine("true");
					}
					else
					{
						Debug.WriteLine("false");
					}
					ConfirmOneCommand.ChangeCanExecute();
				}
			}
		}

		#endregion


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

		bool ValidEntry()
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
			if (IsValid)
			{
				Debug.WriteLine("true func");
			}
			else
			{
				Debug.WriteLine("false func");
			}
			OnPropertyChanged("IsValid");
			return valid;
		}

		async Task SaveEntry()
		{
			EntryModel model = new EntryModel();
			model.Car = Car.Num;
			model.Station = Station.Name;
			model.RoadWidht = RoadWidth;
			model.RoadLength = RoadLength;
			model.RoadArea = RoadArea;
			model.TarQty = TarQty;
			model.Rate = Rate;
			model.TimeCreated = DateTime.Now;
			dataService.AddEntry(model);
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
