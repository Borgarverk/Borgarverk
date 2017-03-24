using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Borgarverk
{
	public class TimeViewModel: INotifyPropertyChanged
	{
		
		private int seconds = 0;
		private int minutes = 59;
		private int hours = 0;
		private string timer;
		private DateTime startTime = DateTime.UtcNow;
		private DateTime endTime;
		private bool active = true;

		public TimeViewModel()
		{
			Device.StartTimer(TimeSpan.FromMilliseconds(1000), OnTimerTick);
			UpdateTimer();
			PauseTimerCommand = new Command(() => PauseTimer());
			ResumeTimerCommand = new Command(() => ResumeTimer());
			EndJobCommand = new Command(() => EndJob());
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#region commands
		public Command PauseTimerCommand { get; }
		public Command ResumeTimerCommand { get; }
		public Command EndJobCommand { get; }
		#endregion

		bool OnTimerTick()
		{
			Seconds += 1;
			return active;
		}

		public DateTime StartTime
		{
			get { return startTime; }
			private set
			{
				startTime = value;
			}
		}

		public DateTime Endtime
		{
			get { return endTime; }
			private set
			{
				endTime = value;
			}
		}

		public int Seconds
		{
			get { return seconds; }
			set
			{
				if (seconds != value)
				{
					if (seconds == 59)
					{
						seconds = 0;
						Minutes += 1;

					}
					else
					{
						seconds = value;
					}
					UpdateTimer();
					OnPropertyChanged("Seconds");
				}
			}
		}

		public int Minutes
		{
			get { return minutes; }
			set
			{
				if (minutes != value)
				{
					if (minutes == 59 && Seconds == 59)
					{
						minutes = 0;
						Hours += 1;
					}
					else
					{
						minutes = value;
					}
					UpdateTimer();
					OnPropertyChanged("Minutes");
				}
			}
		}

		public int Hours
		{
			get { return hours; }
			set
			{
				if (hours != value)
				{
					hours = value;
					UpdateTimer();
					OnPropertyChanged("Hours");
				}
			}
				
		}

		public string Timer
		{
			get { return timer; }
			set
			{
				if (timer != value)
				{
					timer = value;
					OnPropertyChanged("Timer");
				}
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

		private void UpdateTimer()
		{
			string sec = "", min = "", h = "";
			if (seconds < 10)
			{
				sec = String.Format("0{0}", seconds);
			}
			else
			{
				sec = String.Format("{0}", seconds);
			}

			if (minutes < 10)
			{
				min = String.Format("0{0}", minutes);
			}
			else
			{
				min = String.Format("{0}", minutes);
			}

			if (hours < 10)
			{
				h = String.Format("0{0}", hours);
			}
			else
			{
				h = String.Format("{0}", hours);
			}

			Timer = String.Format("{0}:{1}:{2}", h, min, sec);
		}

		void PauseTimer()
		{
			Active = false;
		}

		void ResumeTimer()
		{
			Active = true;
			Device.StartTimer(TimeSpan.FromMilliseconds(1000), OnTimerTick);
		}

		void EndJob()
		{
			throw new NotImplementedException();
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
