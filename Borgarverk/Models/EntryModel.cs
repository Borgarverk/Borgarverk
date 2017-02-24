using System;
using SQLite;

namespace Borgarverk
{
	public class EntryModel
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Car { get; set; }
		public string Station { get; set; }
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
