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
		#endregion

		#region events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		public EntryListViewModel(ISendService sService)
		{
			this.sendService = sService;
			entries = new ObservableCollection<EntryModel>(DataService.GetEntries());
			//SwipeButtonCommand = new Command((o) => OnSwipeButtonClick(o));
			SendAllEntriesCommand = new Command(() => SendAllEntries());
			ModifySelectedEntryCommand = new Command(() => ModifySelectedEntry());
			DeleteSelectedEntriesCommand = new Command(() => DeleteSelectedEntries());
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

		public EntryModel SelectedItem { get; set; }

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

		// Ath mögulega er þetta ekki sniðugasta leiðin að hafa svona takka neðst til að eyða og breyta,
		// frekar að vera með það í línunni sem þú velur, takkarnir birtast þegar lína valin?
		void DeleteSelectedEntries()
		{
			System.Diagnostics.Debug.WriteLine("DeleteSelectedEntriesCommand");
		}

		// Ath mögulega er þetta ekki sniðugasta leiðin að hafa svona takka neðst til að eyða og breyta,
		// frekar að vera með það í línunni sem þú velur, takkarnir birtast þegar lína valin?
		void ModifySelectedEntry()
		{
			System.Diagnostics.Debug.WriteLine("ModifySelectedEntryCommand");
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
