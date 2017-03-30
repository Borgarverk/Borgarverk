using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Borgarverk.Models;

namespace Borgarverk
{
	public class AppData
	{
		public ObservableCollection<CarModel> Cars { get; set; }
		public ObservableCollection<StationModel> Stations { get; set; }
		[XmlIgnore]
		public CarModel CurrentCar { get; set; }
		[XmlIgnore]
		public int CurrentCarIndex { get; set; }
		public StationModel CurrentStation { get; set; }
		public int CurrentStationIndex { get; set; }
		public string JobNo { get; set; }
		public string No { get; set; }
		public string RoadWidth { get; set; }
		public string RoadLength { get; set; }
		public string RoadArea { get; set; }
		public string TarQty { get; set; }
		public string Rate { get; set; }
		public string Degrees { get; set; }
		public string Comment { get; set; }

		public AppData()
		{
			Cars = new ObservableCollection<CarModel>();
			Stations = new ObservableCollection<StationModel>();
			CurrentCarIndex = -1;
			CurrentStationIndex = -1;
		}

		public string Serialize()
		{
			Debug.WriteLine("SERIALIZE");
			// If the CurrentInfo is valid, set the CurrentInfoIndex.
			if (CurrentCar != null)
			{
				CurrentCarIndex = Cars.IndexOf(CurrentCar);
			}
			if (CurrentStation != null)
			{
				CurrentStationIndex = Stations.IndexOf(CurrentStation);
			}

			XmlSerializer serializer = new XmlSerializer(typeof(AppData));
			using (StringWriter stringWriter = new StringWriter())
			{
				serializer.Serialize(stringWriter, this);
				return stringWriter.GetStringBuilder().ToString();
			}
		}

		public static AppData Deserialize(string strAppData)
		{
			Debug.WriteLine("DESERIALIZE");
			XmlSerializer serializer = new XmlSerializer(typeof(AppData));
			using (StringReader stringReader = new StringReader(strAppData))
			{
				AppData appData = (AppData)serializer.Deserialize(stringReader);
				// If the CurrentInfoIndex is valid, set the CurrentInfo.
				if (appData.CurrentCarIndex != -1)
				{
					appData.CurrentCar = appData.Cars[appData.CurrentCarIndex];
				}
				if (appData.CurrentStationIndex != -1)
				{
					appData.CurrentStation = appData.Stations[appData.CurrentStationIndex];
				}
				return appData;
			}
		}
	}
}
