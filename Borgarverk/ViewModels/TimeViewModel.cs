using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Borgarverk
{
	public class TimeViewModel: INotifyPropertyChanged
	{
		private DateTime startTime = DateTime.Now;
		private DateTime endTime = DateTime.Now;
		private bool active = true;
		private INavigation navigation;

		public TimeViewModel()
		{
		}

		public TimeViewModel(INavigation n)
		{
			// If a job has aldready started retreive the startTime
			if (Application.Current.Properties.ContainsKey("startTime"))
			{
				startTime = (DateTime)(Application.Current.Properties["startTime"]);
			}
			else // If not save the startTime
			{
				Application.Current.Properties["startTime"] = startTime;
			}

			// If job has ended, retreive the endtime
			if (Application.Current.Properties.ContainsKey("endTime"))
			{
				endTime = (DateTime)(Application.Current.Properties["endTime"]);
				active = false;
			}
			this.navigation = n;
			EndJobCommand = new Command(async () => await EndJob());
			CancelCommand = new Command(async () => await Cancel());
			ContinueCommand = new Command(() => Continue());
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#region commands
		public Command EndJobCommand { get; }
		public Command CancelCommand { get; }
		public Command ContinueCommand { get; }
		#endregion

		bool OnTimerTick()
		{
			Debug.WriteLine("Timer enn on");
			return active;
		}

		public DateTime StartTime
		{
			get { return startTime; }
			private set
			{
				startTime = value;
				OnPropertyChanged("StartTime");
			}
		}

		public DateTime EndTime
		{
			get { return endTime; }
			private set
			{
				endTime = value;
				OnPropertyChanged("EndTime");
			}
		}

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

		async Task EndJob()
		{
			var confirm = await Application.Current.MainPage.DisplayAlert("Enda verk", "Ertu viss um að þú viljir enda verk?", "Já", "Nei");

			if (confirm)
			{
				EndTime = DateTime.Now;
				Application.Current.Properties["endTime"] = endTime;
				Active = false;
			}
		}

		async Task Cancel()
		{
			var confirm = await Application.Current.MainPage.DisplayAlert("Hætta við verk", "Ertu viss um að þú viljir eyða verki", "Já", "Nei");

			if (confirm)
			{
				if (Application.Current.Properties.ContainsKey("startTime"))
				{
					Application.Current.Properties.Remove("startTime");
				}
				if (Application.Current.Properties.ContainsKey("endTime"))
				{
					Application.Current.Properties.Remove("endTime");
				}
				await this.navigation.PopAsync();
			}
			// Job canceled, delete the saved starttime
		}

		void Continue()
		{
			this.navigation.PushAsync(new NewEntryPage(StartTime, EndTime));
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
