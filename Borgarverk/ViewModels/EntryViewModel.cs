using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Borgarverk.Models;
using Xamarin.Forms;

namespace Borgarverk.ViewModels
{
	public class EntryViewModel : INotifyPropertyChanged
	{
		#region private variables
		private string roadWidth = "", no = "", jobNo = "", roadLength = "", roadArea = "", tarQty = "", rate = "", degrees="";
		private int id;
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
		public EntryViewModel(INavigation navigation, ISendService sService, EntryModel m)
		{
			ConfirmOneCommand = new Command(async () => await SaveEntry(), () => ValidEntry());
			this.sendService = sService;
			this.navigation = navigation;
			this.cars = new ObservableCollection<CarModel>(DataService.GetCars());
			this.stations = new ObservableCollection<StationModel>(DataService.GetStations());
			ID = m.ID;
			jobNo = m.JobNo;
			no = m.No;
			roadWidth = m.RoadWidth;
			roadLength = m.RoadLength;
			roadArea = m.RoadArea;
			tarQty = m.TarQty;
			rate = m.Rate;
			degrees = m.Degrees;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public Command ConfirmOneCommand { get; }

		#region properties
		public int ID
		{
			get { return id; }
			set
			{
				if (id!= value)
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
						No = "VSB";
						OnPropertyChanged("No");
					}
					else
					{
						if (No.Contains("VSB"))
						{
							No = "";
							OnPropertyChanged("No");
						}
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
					Debug.WriteLine(RoadWidth);
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
				try
				{
					if (roadLength != value)
					{
						roadLength = value;
						Debug.WriteLine(roadLength);
						ValidEntry();
						OnPropertyChanged("RoadLength");
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.InnerException.Message);
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
					Debug.WriteLine("RATE");
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
					Debug.WriteLine("Degrees");
					OnPropertyChanged("Degrees");
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
				(JobNo.Length > 0) &&
				(Double.Parse(JobNo) > 0) &&
				(RoadLength.Length > 0) &&
				(Double.Parse(RoadLength) > 0) &&
				(RoadWidth.Length > 0) &&
				(Double.Parse(RoadWidth) > 0) &&
				(RoadArea.Length > 0) &&
				(Double.Parse(RoadArea) > 0) &&
				(TarQty.Length > 0) &&
				(Double.Parse(TarQty) > 0) &&
				(Rate.Length > 0) &&
				(Double.Parse(Rate) > 0) &&
				(Degrees.Length > 0) &&
				(Double.Parse(Degrees) > 0) &&
				(Car != null) &&
				(Station != null );
			Debug.WriteLine(Double.Parse("2."));
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
				model.JobNo = JobNo;
				model.RoadWidth = (Double.Parse(RoadWidth)).ToString();
				model.RoadLength = (Double.Parse(RoadLength)).ToString();
				model.RoadArea = (Double.Parse(RoadArea)).ToString();
				model.TarQty = (Double.Parse(TarQty)).ToString();
				model.Rate = (Double.Parse(Rate)).ToString();
				model.Degrees = (Double.Parse(Degrees)).ToString();
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
