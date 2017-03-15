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
			SwipeButtonCommand = new Command((o) => OnSwipeButtonClick(o));
		}

		#region commands
		public Command SwipeButtonCommand { get; }
		public Command SendEntryCommand { get; }
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
					OnPropertyChanged("Entries");
				}
			}
		}
		#endregion

		#region class methods 
		// If delete button is clicked delete entry from database and table
		// If Send button try to resend the entry to TrackWell
		void OnSwipeButtonClick(object parameter)
		{
			SwipeButtonEventArgs arg = parameter as SwipeButtonEventArgs;
			if (arg != null)
			{
				if (arg.ButtonInfo.ButtonName == "DeleteButton")
				{
					DataService.DeleteEntry(Entries[arg.SourceRowIndex].ID);
					this.Entries.RemoveAt(arg.SourceRowIndex);
				}
				else if (arg.ButtonInfo.ButtonName == "SendButton")
				{
					if (sendService.SendEntry(Entries[arg.SourceRowIndex]))
					{
						var model = Entries[arg.SourceRowIndex];
						model.TimeSent = DateTime.Now;
						model.Sent = true;
						DataService.UpdateEntry(model);
						OnPropertyChanged("Entries");
						Entries.RemoveAt(arg.SourceRowIndex);
						Entries.Insert(arg.SourceRowIndex, model);
					}
					else
					{
						Application.Current.MainPage.DisplayAlert("Tókst ekki að senda færslu", "Reyndu aftur síðar", "Í lagi");
					}
				}
			}
		}

		// Save edited entry to database
		public void OnEntryEdited(int index)
		{
			var model = entries[index];
			DataService.UpdateEntry(model);
			Entries.RemoveAt(index);
			Entries.Insert(index, model);
		}

		// Returns a string that tells the user all errors in his input
		public string ModelValidation(EntryModel model)
		{
			var list = new List<string>();
			if (model.No == "")
			{
				list.Add("no");
				Debug.WriteLine("TomtNumer");
			}
			if (model.RoadWidth == "" || Int32.Parse(model.RoadWidth) <= 0)
			{
				list.Add("Breiddin");
			}
			if (model.RoadLength == "" || Int32.Parse(model.RoadLength) <= 0)
			{
				list.Add("Lengdin");
			}
			if (model.RoadArea == "" || Int32.Parse(model.RoadArea) <= 0)
			{
				list.Add("Flatarmálið");
			}
			if (model.TarQty == "" || Int32.Parse(model.TarQty) <= 0)
			{
				list.Add("tarqty");
			}
			if (model.Rate == "" || Int32.Parse(model.Rate) <= 0)
			{
				list.Add("rate");
			}
			return ErrorString(list);
		}

		// Constructs the error string
		private string ErrorString(List<string> l)
		{
			string message = "";
			for (int i = 0; i < l.Count; i++)
			{
				if (l[i] == "no")
				{
					message += "Færslunúmerið getur ekki verið tómt\n";
				}
				else if (l[i] == "tarqty")
				{
					message += "Lítrarnir verða að vera fleiri en 0\n";
				}
				else if (l[i] == "rate")
				{
					message += "Lítrar/fm verða að vera hærri en 0\n";
				}
				else
				{
					message += String.Format("{0} verða að vera hærri en 0\n", l[i]);
				}
			}
			return message;
		}
		#endregion

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var changed = PropertyChanged;
			if (changed != null)
			{
				Debug.WriteLine("Breyttist " + propertyName);
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

	}
}
