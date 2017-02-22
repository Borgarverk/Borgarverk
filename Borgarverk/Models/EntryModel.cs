using System;
namespace Borgarverk
{
	public class EntryModel
	{
		public CarModel Car { get; set; }
		public StationModel Station { get; set; }
		public string No { get; set; }
		public string RoadLength { get; set; }
		public string RoadWidht { get; set; }
		public string RoadArea { get; set; }
		public string TarQty { get; set; }
		public string Rate { get; set; }
		public DateTime TimeSent { get; set; }

		public EntryModel()
		{
		}
	}
}
