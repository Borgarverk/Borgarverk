using System;
using System.ComponentModel;
using SQLite;

namespace Borgarverk.Models
{
	public class EntryModel : INotifyPropertyChanged
	{
		#region events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Car { get; set; }
		public string Station { get; set; }
		public string No { get; set; }
		public int JobNo { get; set; }
		public double RoadLength { get; set; }
		public double RoadWidth { get; set; }
		public double RoadArea { get; set; }
		public double TarQty { get; set; }
		public double Rate { get; set; }
		public double Degrees { get; set; }
		public string Comment { get; set; }
		public DateTime? TimeSent { get; set; }
		public DateTime? TimeCreated { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public bool sent = false;
		private bool active = false;

		public bool Active
		{
			get { return active; }
			set
			{
				if (active != value)
				{
					active = value;
					OnPropertyChanged("Active");
				}
			}
		}

		public bool Sent
		{
			get { return sent; }
			set
			{
				if (sent != value)
				{
					sent = value;
					OnPropertyChanged("Sent");
				}
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
