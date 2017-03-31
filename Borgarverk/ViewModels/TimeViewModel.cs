using System;
using System.ComponentModel;
using System.Diagnostics;
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
				Debug.WriteLine("Það er key");
			}
			else // If not save the startTime
			{
				Debug.WriteLine("Reyni að búa til key");
				Application.Current.Properties["startTime"] = startTime;
				Debug.WriteLine("Það er ekki key, bjó til");
			}
			this.navigation = n;
			EndJobCommand = new Command(() => EndJob());
			CancelCommand = new Command(() => Cancel());
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

		void EndJob()
		{
			EndTime = DateTime.Now;
			Active = false;
		}

		void Cancel()
		{
			// Job canceled, delete the saved starttime
			if (Application.Current.Properties.ContainsKey("startTime"))
			{
				Application.Current.Properties.Remove("startTime");
			}
			this.navigation.PopAsync();
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
