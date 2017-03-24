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
			this.navigation = n;
			//Device.StartTimer(TimeSpan.FromMilliseconds(1000), OnTimerTick);
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
