using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Borgarverk
{
	public class EntryListViewModel : INotifyPropertyChanged
	{
		#region private variables
		private ObservableCollection<EntryModel> entries;
		private IDataService dataService;
		#endregion

		public event PropertyChangedEventHandler PropertyChanged;

		public EntryListViewModel(IDataService service)
		{
			dataService = service;
			entries = new ObservableCollection<EntryModel>(dataService.GetEntries());
		}

		public ObservableCollection<EntryModel> Entries
		{
			get { return entries; }
			set
			{
				if (Entries != value)
				{
					entries = value;
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
