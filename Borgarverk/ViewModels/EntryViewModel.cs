using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Borgarverk
{
	public class EntryViewModel : INotifyPropertyChanged
	{
		#region private variables
		private string roadWidth = "", no = "", roadLength = "", roadArea = "", tarQty = "", rate = "";
		private CarModel car;
		private StationModel station;
		private DateTime timeSent;
		private bool isValid = false;
		private readonly DataService dataService;
		#endregion

		public EntryViewModel()
		{
			//ConfirmOneCommand = new Command();
			dataService = new DataService();
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
					OnPropertyChanged("IsValid");
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
					OnPropertyChanged("IsValid");
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
					OnPropertyChanged("IsValid");
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
					OnPropertyChanged("IsValid");
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
					OnPropertyChanged("IsValid");
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
					OnPropertyChanged("IsValid");
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
					OnPropertyChanged("IsValid");
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
					OnPropertyChanged("IsValid");
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
					ValidEntry();
					OnPropertyChanged("IsValid");
					OnPropertyChanged("TimeSent");
				}
			}
		}

		public bool Isvalid
		{
			get { return isValid; }
			set 
			{
				if (isValid != value)
				{
					isValid = value;
					OnPropertyChanged("IsValid");
				}
			}
		}
					
		#endregion

		private ObservableCollection<CarModel> carItems = new ObservableCollection<CarModel>()
			{
				new CarModel("ML-455"),
				new CarModel("MU-510"),
				new CarModel("BZ-963"),
				new CarModel("US-553"),
				new CarModel("AZ-R92")
			};

		public ObservableCollection<CarModel> CarItems
		{
			get
			{
				return carItems;
			}
			set
			{
				carItems = value;
			}
		}

		private ObservableCollection<StationModel> stationItems = new ObservableCollection<StationModel>()
			{
				new StationModel("Akureyri"),
				new StationModel("Ísafjörður"),
				new StationModel("Reykjavík"),
				new StationModel("Hlaðbær Colas"),
				new StationModel("Reyðarfjörður")
			};

		public ObservableCollection<StationModel> StationItems
		{
			get
			{
				return stationItems;
			}
			set
			{
				stationItems = value;

			}
		}

		void ValidEntry()
		{
			isValid = (No.Length > 0) &&
				(RoadLength.Length > 0) &&
				(RoadWidth.Length > 0) &&
				(RoadArea.Length > 0) &&
				(TarQty.Length > 0) &&
				(Rate.Length > 0) &&
				(Car != null) &&
				(Station != null);
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
