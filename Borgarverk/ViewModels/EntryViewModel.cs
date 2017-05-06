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
		private string roadWidth = "", no = "", jobNo = "", roadLength = "", roadArea = "", tarQty = "", rate = "", degrees = "", title = "", comment = "";
		private int id = 0;
		private CarModel car = null;
		private StationModel station = null;
		private DateTime? timeSent, timeCreated, startTime, endTime;
		private bool isValid = false;
		private readonly ISendService sendService;
		private ObservableCollection<CarModel> cars;
		private ObservableCollection<StationModel> stations;
		private INavigation navigation;
		private Boolean selectedHladbaerColas;
		#endregion

		public Boolean SelectedHladbaerColas
		{
			get { return selectedHladbaerColas; }
			set { selectedHladbaerColas = value; }
		}

		// For Testing
		public EntryViewModel(ISendService sService)
		{
			ConfirmOneCommand = new Command(async () => await SaveEntry(), () => ValidEntry());
			this.sendService = sService;
			this.cars = new ObservableCollection<CarModel>();
			this.stations = new ObservableCollection<StationModel>();
			title = "Nýtt verk";
			this.selectedHladbaerColas = false;
		}

		public EntryViewModel(INavigation navigation, ISendService sService, DateTime start, DateTime end)
		{
			ConfirmOneCommand = new Command(async () => await SaveEntry(), () => ValidEntry());
			this.sendService = sService;
			this.navigation = navigation;
			this.startTime = start;
			this.endTime = end;
			this.cars = new ObservableCollection<CarModel>(DataService.GetCars());
			this.stations = new ObservableCollection<StationModel>(DataService.GetStations());
			title = "Nýtt verk";
			this.selectedHladbaerColas = false;
		}

		public EntryViewModel(INavigation navigation, ISendService sService, EntryModel m)
		{
			ConfirmOneCommand = new Command(async () => await SaveEntry(), () => ValidEntry());
			this.sendService = sService;
			this.navigation = navigation;
			this.cars = new ObservableCollection<CarModel>(DataService.GetCars());
			this.stations = new ObservableCollection<StationModel>(DataService.GetStations());
			title = "Breyta færslu";
			foreach (var c in cars)
			{
				if (c.Num == m.Car)
				{
					car = c;
				}
			}
			foreach (var s in stations)
			{
				if (s.Name == m.Station)
				{
					station = s;
				}
			}
			ID = m.ID;
			jobNo = m.JobNo;
			no = m.No;
			roadWidth = m.RoadWidth;
			roadLength = m.RoadLength;
			roadArea = m.RoadArea;
			tarQty = m.TarQty;
			rate = m.Rate;
			degrees = m.Degrees;
			comment = m.Comment;
			startTime = m.StartTime;
			endTime = m.EndTime;
			this.selectedHladbaerColas = false;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public Command ConfirmOneCommand { get; }

		#region properties
		public int ID
		{
			get { return id; }
			set
			{
				if (id != value)
				{
					id = value;
				}
			}
		}
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
					if (value.Name == "Hlaðbær Colas")
					{
						this.selectedHladbaerColas = true;
						OnPropertyChanged("SelectedHladbaerColas");
					}
					else
					{
						this.selectedHladbaerColas = false;
						OnPropertyChanged("SelectedHladbaerColas");
					}
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

		public string JobNo
		{
			get { return jobNo; }
			set
			{
				if (jobNo != value)
				{
					jobNo = value;
					ValidEntry();
					OnPropertyChanged("JobNo");
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

		public string Degrees
		{
			get { return degrees; }
			set
			{
				if (degrees != value)
				{
					degrees = value;
					ValidEntry();
					OnPropertyChanged("Degrees");
				}
			}
		}

		public string Comment
		{
			get { return comment; }
			set
			{
				if (comment != value)
				{
					comment = value;
					OnPropertyChanged("Comment");
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

		public DateTime? StartTime
		{
			get { return startTime; }
			set
			{
				if (startTime != value)
				{
					startTime = value;
					OnPropertyChanged("StartTime");
				}
			}
		}

		public DateTime? EndTime
		{
			get { return endTime; }
			set
			{
				if (endTime != value)
				{
					endTime = value;
					OnPropertyChanged("EndTime");
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

		public string Title
		{
			get { return title; }
			private set
			{
				title = value;
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
				(JobNo.Length > 0) &&
				(Double.Parse(JobNo) > 0) &&
				(RoadArea.Length > 0) &&
				(Double.Parse(RoadArea) > 0) &&
				(TarQty.Length > 0) &&
				(Double.Parse(TarQty) > 0) &&
				(Rate.Length > 0) &&
				(Double.Parse(Rate) > 0) &&
				(Degrees.Length > 0) &&
				(Double.Parse(Degrees) > 0) &&
				(Car != null) &&
				(Station != null);
			IsValid = valid;
			OnPropertyChanged("IsValid");
			return valid;
		}

		async Task SaveEntry()
		{
			var confirmed = await Application.Current.MainPage.DisplayAlert("Staðfesta sendingu", "Staðfesta sendingu færslu?", "Já", "Nei");
			if (confirmed)
			{
				Save();
				// Job finished, delete the saved start time and end time
				if (Application.Current.Properties.ContainsKey("startTime"))
				{
					Application.Current.Properties.Remove("startTime");
					await SaveProperties();

				}
				if (Application.Current.Properties.ContainsKey("endTime"))
				{
					Application.Current.Properties.Remove("endTime");
					await SaveProperties();
				}
				await navigation.PopToRootAsync(false);
			}
		}

		async Task Save()
		{
			EntryModel model = new EntryModel();
			if (ID != 0)
			{
				model.ID = ID;
			}
			model.Car = Car.Num;
			model.Station = Station.Name;
			if (this.selectedHladbaerColas)
			{
				model.No = "VSB" + No;
			}
			else
			{
				model.No = No;
			}

			model.JobNo = (Double.Parse(JobNo)).ToString();
			// RoadWidth and RoadLength can be left unfilled
			if (RoadWidth != "")
			{
				model.RoadWidth = (Double.Parse(RoadWidth)).ToString();
			}
			else
			{
				model.RoadWidth = RoadWidth;
			}
			if (RoadLength != "")
			{
				model.RoadLength = (Double.Parse(RoadLength)).ToString();
			}
			else
			{
				model.RoadLength = RoadLength;
			}
			model.RoadArea = (Double.Parse(RoadArea)).ToString();
			model.TarQty = (Double.Parse(TarQty)).ToString();
			model.Rate = (Double.Parse(Rate)).ToString();
			model.Degrees = (Double.Parse(Degrees)).ToString();
			model.TimeCreated = DateTime.Now;
			model.StartTime = StartTime;
			model.EndTime = EndTime;
			model.Comment = Comment;

			var sendResult = await sendService.SendEntry(model);
			if (sendResult)
			{
				model.TimeSent = DateTime.Now;
				model.Sent = true;
				// Var það þannig að ef að það er entry i database-inum 
				//með sama ID þá er ekki insertað heldur update-að?
				DataService.AddEntry(model);
				DependencyService.Get<IPopUp>().ShowToast("Sending tókst");
				//await Application.Current.MainPage.DisplayAlert("Sending tókst", "Færslan hefur verið send", "Loka");
			}
			else
			{
				model.TimeSent = null;
				model.Sent = false;
				DataService.AddEntry(model);
				DependencyService.Get<IPopUp>().ShowToast("Sending mistókst");
				//await Application.Current.MainPage.DisplayAlert("Sending mistókst", "Ekki tókst að senda færslu", "Loka");
			}
		}

		async Task SaveProperties()
		{
			await Application.Current.SavePropertiesAsync();
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