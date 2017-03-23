using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Borgarverk
{
	public class TimeViewModel: INotifyPropertyChanged
	{
		public TimeViewModel()
		{
			Device.StartTimer(TimeSpan.FromMilliseconds(1000), OnTimerTick);
			UpdateTimer();
		}

		int seconds = 0;
		int minutes = 0;
		int hours = 0;
		string timer = "";
		DateTime startTime = DateTime.Now;
		DateTime endTime;

		public event PropertyChangedEventHandler PropertyChanged;

		bool OnTimerTick()
		{
			Debug.WriteLine("Komin inn i timertick");
			Seconds += 1;
			return true;
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
					if (seconds == 60)
					{
						seconds = 0;
						Minutes += 1;

					}
					else
					{
						seconds = value;
					}
					UpdateTimer();
					Debug.WriteLine("Seconds");
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
					if (minutes == 60)
					{
						minutes = 0;
						Hours += 1;
					}
					else
					{
						minutes = value;
					}
					UpdateTimer();
					Debug.WriteLine("Minutes");
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
					Debug.WriteLine("Hours");
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
					Debug.WriteLine("Timer");
					OnPropertyChanged("Timer");
				}
			}
		}

		private void UpdateTimer()
		{
			Timer = String.Format("{0}:{1}:{2}", hours, minutes, seconds);
			Debug.WriteLine(timer);
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
