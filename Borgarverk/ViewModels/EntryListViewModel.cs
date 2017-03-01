using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using DevExpress.Mobile.DataGrid;
using Xamarin.Forms;

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
			SwipeButtonCommand = new Command((o) => OnSwipeButtonClick(o));
			Debug.WriteLine(Entries.Count);
		}

		public Command SwipeButtonCommand { get; }
		public Command DeleteEntryCommand { get; }
		public Command EditEntryCommand { get; }
		public Command SendEntryCommand { get; }

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

		void OnSwipeButtonClick(object parameter)
		{
			SwipeButtonEventArgs arg = parameter as SwipeButtonEventArgs;
			if (arg != null)
			{
				if (arg.ButtonInfo.ButtonName == "DeleteButton")
				{
					dataService.DeleteEntry(Entries[arg.SourceRowIndex].ID);
					this.Entries.RemoveAt(arg.SourceRowIndex);
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
