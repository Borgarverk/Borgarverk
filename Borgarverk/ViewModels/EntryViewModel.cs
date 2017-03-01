using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
		private INavigation navigation;
		private EntryModel model;
		#endregion

		public EntryViewModel(IDataService service, INavigation navigation)
		{
			ConfirmOneCommand = new Command(async () => await SaveEntry(), () => ValidEntry());
			this.dataService = service;
			this.navigation = navigation;
			cars = new ObservableCollection<CarModel>(dataService.GetCars());
			stations = new ObservableCollection<StationModel>(dataService.GetStations());
			model = new EntryModel();
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
			OnPropertyChanged("IsValid");
			return valid;
		}

		async Task SaveEntry()
		{
			var confirmed = await App.Current.MainPage.DisplayAlert("Confirmation", "Staðfesta sendingu forms?", "Já", "Nei");
			if (confirmed)
			{
				model.TimeCreated = DateTime.Now;
				dataService.AddEntry(model);
				EntryToJsonToEntryExample();
				await navigation.PopAsync();
			}
		}

		void EntryToJsonToEntryExample()
		{
			var entry = this.Model;
			var json = JsonConvert.SerializeObject(entry);
			Debug.WriteLine("JSON representation of person: {0}", json);
			var model2 = JsonConvert.DeserializeObject<EntryModel>(json);
			Debug.WriteLine("{0} - {1}", model2.Car, model2.Station);
		}
		//async void OnSubmission(object sender, EventArgs e)
		//{
		//	var confirmed = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
		//	if (confirmed)
		//	{
		//		await SaveEntry();
		//	}
		//}

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
