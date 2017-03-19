using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Borgarverk.Models;
using DevExpress.Mobile.DataGrid;
using Xamarin.Forms;

namespace Borgarverk.ViewModels
{
	public class EntryListViewModel : INotifyPropertyChanged
	{
		#region private variables
		private ObservableCollection<EntryModel> entries;
		private ISendService sendService;
		private EntryModel selectedEntry;
		private EntryModel unfocusedEntry;
		private bool deleteButtonActive;
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
			entries = new ObservableCollection<EntryModel>(DataService.GetEntries());
			SendAllEntriesCommand = new Command(() => SendAllEntries());
			ModifySelectedEntryCommand = new Command(() => ModifySelectedEntry());
			DeleteSelectedEntriesCommand = new Command(() => DeleteSelectedEntries());
			deleteButtonActive = false;
			ButtonColor = "#d0cccc";
			selectedEntry = null;
			unfocusedEntry = null;
		}

		#region commands
		public Command SwipeButtonCommand { get; }
		public Command SendAllEntriesCommand { get; }
		public Command ModifySelectedEntryCommand { get; }
		public Command DeleteSelectedEntriesCommand { get; }
		#endregion

		#region properties
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

		public EntryModel SelectedEntry { 
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
		}

		public bool DeleteButtonActive { 
			get { return deleteButtonActive; } 
			set { deleteButtonActive = value; }
		}

		public string ButtonColor { get; set; }
		#endregion

		//void OnSwipeButtonClick(object parameter)
		//{
		//	SwipeButtonEventArgs arg = parameter as SwipeButtonEventArgs;
		//	if (arg != null)
		//	{
		//		if (arg.ButtonInfo.ButtonName == "DeleteButton")
		//		{
		//			Debug.WriteLine(Entries.Count);
		//			Debug.WriteLine(arg.SourceRowIndex);
		//			var delEntry = Entries[arg.SourceRowIndex];
		//			Debug.WriteLine(delEntry.No);
		//			Entries.Remove(delEntry);
		//			Debug.WriteLine(Entries.Count);
		//			try
		//			{
		//				DataService.DeleteEntry(delEntry.ID);
		//			}
		//			catch
		//			{
		//				Application.Current.MainPage.DisplayAlert("", "Ekki tókst að eyða færslu", "OK");
		//			}
		//		}
		//		else if (arg.ButtonInfo.ButtonName == "SendButton")
		//		{
		//			if (sendService.SendEntry(Entries[arg.SourceRowIndex]))
		//			{
		//				var model = Entries[arg.SourceRowIndex];
		//				model.TimeSent = DateTime.Now;
		//				model.Sent = true;
		//				DataService.UpdateEntry(model);
		//				OnPropertyChanged("Entries");
		//				Entries.RemoveAt(arg.SourceRowIndex);
		//				Entries.Insert(arg.SourceRowIndex, model);
		//			}
		//			else
		//			{
		//				Application.Current.MainPage.DisplayAlert("Tókst ekki að senda færslu", "Reyndu aftur síðar", "Í lagi");
		//			}
		//		}
		//	}
		//}


		// TODO: implement
		void DeleteSelectedEntries()
		{
			if (!DeleteButtonActive)
			{
				Application.Current.MainPage.DisplayAlert("", "Engin færsla valin", "OK");
				return;
			}

			try
			{
				DataService.DeleteEntry(SelectedEntry.ID);
				Entries.Remove(SelectedEntry);
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
			if (!DeleteButtonActive || selectedEntry == null)
			{
				Application.Current.MainPage.DisplayAlert("", "Engin færsla valin", "OK");
				return;
			}
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
		
		void SendAllEntries()
		{
			List<EntryModel> del = new List<EntryModel>();
			foreach (var entry in Entries)
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
					var tmpEntry = Entries[Entries.IndexOf(del[i])];
					tmpEntry.Sent = true;
					tmpEntry.TimeSent = DateTime.Now;
					DataService.UpdateEntry(tmpEntry);
					Entries.Insert(Entries.IndexOf(tmpEntry), tmpEntry);
					Entries.Remove(tmpEntry);
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
