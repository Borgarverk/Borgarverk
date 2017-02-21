using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Borgarverk
{
	public class EntryViewModel : INotifyPropertyChanged
	{
		#region private variables
		private string car, station, roadWidth, no, roadLength, roadArea, tarQty, rate;
		DateTime timeSent;
		#endregion

		public event PropertyChangedEventHandler PropertyChanged;

		#region properties
		public string Car
		{
			get { return car; }
			set
			{
				if (car != value)
				{
					car = value;
					OnPropertyChanged("Car");
				}
			}
		}

		public string Station
		{
			get { return station; }
			set
			{
				if (station != value)
				{
					station = value;
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
		#endregion

		public EntryViewModel()
		{
		}


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
