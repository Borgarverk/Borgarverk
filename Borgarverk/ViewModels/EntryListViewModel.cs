using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Borgarverk.Models;
using Xamarin.Forms;

namespace Borgarverk.ViewModels
{
	public class EntryListViewModel : INotifyPropertyChanged
	{
		#region private variables
		private ObservableCollection<EntryModel> allEntries;
		private ObservableCollection<EntryModel> entries;
		private ObservableCollection<CarModel> cars;
		private ObservableCollection<StationModel> stations;
		private ISendService sendService;
		private EntryModel selectedEntry;
		private EntryModel unfocusedEntry;
		private bool deleteButtonActive, isSelected, isEditing;
		private string searchString = "";
		private INavigation navigation;
		#endregion

		#region events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		// Ath þurfum navigation ef við ætlum að geta editað færslu
		// en kannski vilja þeir það ekki? spyrjum á fundinum
		public EntryListViewModel(ISendService sService, INavigation navigation)
		{
			this.sendService = sService;
			this.navigation = navigation;
			allEntries = new ObservableCollection<EntryModel>(DataService.GetEntries());
			this.cars = new ObservableCollection<CarModel>(DataService.GetCars());
			this.stations = new ObservableCollection<StationModel>(DataService.GetStations());
			Entries = AllEntries;
			SendAllEntriesCommand = new Command(() => SendAllEntries());
			ModifySelectedEntryCommand = new Command(() => ModifySelectedEntry());
			DeleteSelectedEntryCommand = new Command(() => DeleteSelectedEntry());
			CloseCommand = new Command(() => Close());
			deleteButtonActive = false;
			ButtonColor = "#d0cccc";
			selectedEntry = null;
			unfocusedEntry = null;
		}

		#region commands
		public Command SwipeButtonCommand { get; }
		public Command SendAllEntriesCommand { get; }
		public Command ModifySelectedEntryCommand { get; }
		public Command DeleteSelectedEntryCommand { get; }
		public Command CloseCommand { get; }
		//public Command SearchCommand { get; }
		#endregion

		#region properties
		public ObservableCollection<EntryModel> AllEntries
		{
			get { return allEntries; }
			set
			{
				allEntries = value;
				OnPropertyChanged("AllEntries");
			}
		}

		public ObservableCollection<EntryModel> Entries
		{
			get { return entries; }
			set
			{
				entries = value;
				OnPropertyChanged("Entries");
			}
		}

		public EntryModel SelectedEntry { 
			get { return selectedEntry; }
			set
			{
				if (selectedEntry != value)
				{
					if (value == null)
					{
						IsSelected = false;
						OnPropertyChanged("SelectedEntry");
					}
					else
					{
						IsSelected = true;
						OnPropertyChanged("SelectedEntry");
					}
					selectedEntry = value;
				}
			}
		}

		public bool DeleteButtonActive { 
			get { return deleteButtonActive; } 
			set 
			{
				if (deleteButtonActive != value)
				{
					deleteButtonActive = value;
					OnPropertyChanged("DeleteButtonActive");
				}
			}
		}

		public bool IsSelected
		{
			get { return isSelected; }
			set
			{
				if (isSelected != value)
				{
					if (value == true)
					{
						IsEditing = false;
					}
					isSelected = value;
					OnPropertyChanged("IsSelected");
				}
			}
		}

		public bool IsEditing
		{
			get { return isEditing; }
			set
			{
				if (isEditing != value)
				{
					if (value == true)
					{
						IsSelected = false;
					}
					isEditing = value;
					OnPropertyChanged("IsEditing");
				}
			}
		}

		public string SearchString
		{
			get { return searchString; }
			set
			{
				if (searchString != value)
				{
					searchString = value;
					OnPropertyChanged("SearchString");
					this.Search();
				}
			}
		}

		public string ButtonColor { get; set; }

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

		void DeleteSelectedEntry()
		{
			
			try
			{
				DataService.DeleteEntry(SelectedEntry.ID);
				allEntries.Remove(selectedEntry);
				IsSelected = false;
				SelectedEntry = null;
			}
			catch
			{
				Application.Current.MainPage.DisplayAlert("", "Ekki tókst að eyða færslu", "OK");
			}
		}

		// TODO: implement
		// spurja hvort þetta sé mögulega óþarfi fítus...
		void ModifySelectedEntry()
		{
			IsSelected = false;
			var page = new NewEntryPage(this.sendService, selectedEntry);
			this.navigation.PushAsync(page);
		}

		void Close()
		{
			IsSelected = false;
			IsEditing = false;
			SelectedEntry = null;
		}

		void Search()
		{
			var temp = AllEntries;
			if (String.IsNullOrEmpty(searchString))
			{
				AllEntries = entries;
			}
			var match = entries.Where(c => c.No.ToLower().Contains(SearchString.ToLower()) || 
			                             c.Station.ToLower().Contains(searchString.ToLower()) ||
			                             c.TimeCreated.ToString().ToLower().Contains(SearchString.ToLower()));
			AllEntries = new ObservableCollection<EntryModel>(match);
		}
		
		void SendAllEntries()
		{
			List<EntryModel> del = new List<EntryModel>();
			foreach (var entry in AllEntries)
			{
				if (!entry.Sent)
				{
					del.Add(entry);
				}
			}
			// Setjum timesent breytuna bæði hér og í sendservice gæti þá verið mismatch á tíma
			// Er betra að setja það hér, reyna að senda og ef ekki tekst að senda þá breyta því til baka?
			if (sendService.SendEntries(del))
			{
				Debug.WriteLine(del.Count);
				for (var i = del.Count - 1; i >= 0; i--)
				{
					var tmpEntry = AllEntries[AllEntries.IndexOf(del[i])];
					tmpEntry.Sent = true;
					tmpEntry.TimeSent = DateTime.Now;
					DataService.UpdateEntry(tmpEntry);
					AllEntries.Insert(AllEntries.IndexOf(tmpEntry), tmpEntry);
					AllEntries.Remove(tmpEntry);
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
