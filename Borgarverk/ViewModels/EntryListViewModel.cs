using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Borgarverk.Models;
using DevExpress.Mobile.DataGrid;
using Xamarin.Forms;

namespace Borgarverk.ViewModels
{
	public class EntryListViewModel : INotifyPropertyChanged
	{
		#region private variables
		private ObservableCollection<EntryModel> entries;
		//private IDataService dataService;
		private ISendService sendService;
		private INavigation navigation;
		#endregion

		#region events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		public EntryListViewModel(ISendService sService, INavigation navigation)
		{
			this.sendService = sService;
			entries = new ObservableCollection<EntryModel>(DataService.GetEntries());
			SwipeButtonCommand = new Command((o) => OnSwipeButtonClick(o));
			this.navigation = navigation;
		}

		#region commands
		public Command SwipeButtonCommand { get; }
		public Command EditEntryCommand { get; }
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
				}
			}
		}
		#endregion

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
				else if (arg.ButtonInfo.ButtonName == "EditButton")
				{
					//EntryViewModel vm = new EntryViewModel(navigation, sendService);
					//vm.Car = new CarModel(entries[arg.SourceRowIndex].Car);
					//vm.Station = new StationModel(entries[arg.SourceRowIndex].Station);
					//vm.No = entries[arg.SourceRowIndex].No;
					//vm.RoadArea = entries[arg.SourceRowIndex].RoadArea;
					//vm.RoadWidth = entries[arg.SourceRowIndex].RoadWidth;
					//vm.RoadLength = (entries[arg.SourceRowIndex].RoadLength == null ? entries[arg.SourceRowIndex].RoadLength : ""); // verður null annars...
					//vm.Rate = entries[arg.SourceRowIndex].Rate;
					//vm.TarQty = entries[arg.SourceRowIndex].TarQty;
					//vm.TimeCreated = entries[arg.SourceRowIndex].TimeCreated;
					//vm.TimeSent = entries[arg.SourceRowIndex].TimeSent;
					//// Entries[arg.SourceRowIndex] 
					//navigation.PushAsync(new NewEntryPage(vm));
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
