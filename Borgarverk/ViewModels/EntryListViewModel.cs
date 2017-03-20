using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Borgarverk.Models;
using DevExpress.Mobile.DataGrid;
using Xamarin.Forms;

namespace Borgarverk.ViewModels
{
	public class EntryListViewModel : INotifyPropertyChanged
	{
		#region private variables
		private ObservableCollection<EntryModel> allEntries;
		private ObservableCollection<EntryModel> filteredEntries;
		private ISendService sendService;
		private EntryModel selectedEntry;
		private EntryModel unfocusedEntry;
		private bool deleteButtonActive, isSelected;
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
			SendAllEntriesCommand = new Command(() => SendAllEntries());
			ModifySelectedEntryCommand = new Command((object obj) => ModifySelectedEntry(obj));
			DeleteSelectedEntryCommand = new Command((object obj) => DeleteSelectedEntry(obj));
			CloseCommand = new Command(() => Close());
			//SearchCommand = new Command(() => Search());
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
			}
		}

		public ObservableCollection<EntryModel> FilteredEntries
		{
			get { return filteredEntries; }
			set
			{
				filteredEntries = value;
			}
		}

		/*public EntryModel SelectedEntry { 
			get { return selectedEntry; }
			set
			{
				if (value == null)
				{
					selectedEntry = null;
					deleteButtonActive = false;
					ButtonColor = "#d0cccc";
				}
				else if (value == selectedEntry)
				{
					selectedEntry = null;
				}
				else
				{
					selectedEntry = value;
					deleteButtonActive = true;
					ButtonColor = "#008ead";
					OnPropertyChanged("ButtonColor");
				}
			}
		}*/
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
					isSelected = value;
					OnPropertyChanged("IsSelected");
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
		#endregion

		void DeleteSelectedEntry(object sender)
		{
			
			try
			{
				var delEntry = ((EntryModel)sender);
				DataService.DeleteEntry(delEntry.ID);
				allEntries.Remove(delEntry);
			}
			catch
			{
				Application.Current.MainPage.DisplayAlert("", "Ekki tókst að eyða færslu", "OK");
			}
		}

		// TODO: implement
		// spurja hvort þetta sé mögulega óþarfi fítus...
		void ModifySelectedEntry(object sender)
		{
			/*if (!DeleteButtonActive || selectedEntry == null)
			{
				Application.Current.MainPage.DisplayAlert("", "Engin færsla valin", "OK");
				return;
			}*/

			SelectedEntry = ((EntryModel)sender);
			//EntryViewModel vm = new EntryViewModel(navigation, sendService);
			//vm.Car = new CarModel(selectedEntry.Car);
			//vm.Station = new StationModel(selectedEntry.Station);
			//vm.No = selectedEntry.ID.ToString();
			//vm.RoadArea = selectedEntry.RoadArea;
			//vm.RoadWidth = selectedEntry.RoadWidth;
			//vm.RoadLength = (selectedEntry.RoadLength == null ? selectedEntry..RoadLength : ""); // verður null annars...
			//vm.Rate = selectedEntry.Rate;
			//vm.TarQty = selectedEntry.TarQty;
			//vm.TimeCreated = selectedEntry.TimeCreated;
			//vm.TimeSent = selectedEntry.TimeSent;
			//navigation.PushAsync(new NewEntryPage(vm));
		}

		void Close()
		{
			IsSelected = false;
			SelectedEntry = null;
		}

		void Search()
		{
			var temp = AllEntries;
			Debug.WriteLine("KOMIN INN I SEARCH");
			Debug.WriteLine(searchString);
			if (searchString == "")
			{
				AllEntries = temp;
			}
			var match = AllEntries.Where(c => c.No.Contains(SearchString) || c.Car.Contains(searchString));
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
