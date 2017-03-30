using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Borgarverk.Models;
using Xamarin.Forms;

namespace Borgarverk
{
	public partial class App : Application
	{
		public AppData AppData { private set; get; }
		public static new App Current;

		public App()
		{
			InitializeComponent();
			DataService.OpenConnection();
			Current = this;

			var homepage = new HomePageXaml();
			MainPage = new NavigationPage(homepage);
		}

		protected override async void OnStart()
		{
			// Handle when your app starts
			if (Properties.ContainsKey("appData"))
			{
				AppData = AppData.Deserialize((string)Properties["appData"]);
				if (AppData != null)
				{
					await InitializeCollections();
				}
			}
			else
			{
				AppData = new AppData();
				if (AppData != null)
				{
					await InitializeCollections();
				}
			}

		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
			Properties["appData"] = AppData.Serialize();
		}

		protected override async void OnResume()
		{
			if (Properties.ContainsKey("appData"))
			{
				AppData = AppData.Deserialize((string)Properties["appData"]);
				if (AppData != null)
				{
					await InitializeCollections();
				}
				Debug.WriteLine("There is appdata");
			}
			else
			{
				AppData = new AppData();
				if (AppData != null)
				{
					await InitializeCollections();
				}
				Debug.WriteLine("no appdata");
			}
		}

		public async Task InitializeCollections()
		{
			await Task.Run(() => 
			{ 
				if (Current.AppData.Cars.Count == 0)
				{
					AppData.Cars = new ObservableCollection<CarModel>(DataService.GetCars());
				}
				if (Current.AppData.Stations.Count == 0)
				{
					AppData.Stations = new ObservableCollection<StationModel>(DataService.GetStations());				}
			});
		}
	}
}
