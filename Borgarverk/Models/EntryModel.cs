using System;
using SQLite;

namespace Borgarverk.Models
{
	public class EntryModel
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Car { get; set; }
		public string Station { get; set; }
		public string No { get; set; }
		public string RoadLength { get; set; }
		public string RoadWidth { get; set; }
		public string RoadArea { get; set; }
		public string TarQty { get; set; }
		public string Rate { get; set; }
		public DateTime? TimeSent { get; set; }
		public DateTime TimeCreated { get; set; }
		public bool Sent { get; set; }

		public EntryModel()
		{
		}
	}
}
