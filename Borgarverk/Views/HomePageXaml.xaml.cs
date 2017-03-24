using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Borgarverk
{
	public partial class HomePageXaml : ContentPage
	{
		public HomePageXaml()
		{
			InitializeComponent();
		}

		public async void AllEntriesButtonClicked(object sender, EventArgs e)
		{
			allEntriesButton.SetValue(OpacityProperty, 0.2);
			//await Task.Delay(100);
			//await Navigation.PushAsync(new EntryListPage(), false);
			await Navigation.PushAsync(new EntryListPage(), false);
		}

		public async void SettingsButtonClicked(object sender, EventArgs e)
		{
			settingsButton.SetValue(OpacityProperty, 0.2);
			//await Task.Delay(100);
			await Navigation.PushAsync(new SettingPage(), false);
		}

		public async void StartJobButtonClicked(object sender, EventArgs e)
		{
			newJobButton.SetValue(OpacityProperty, 0.2);
			//await Task.Delay(100);
			await Navigation.PushAsync(new TimePage(), false);
		}

		#region OnAppearing()
		protected override void OnAppearing()
		{
			base.OnAppearing();
			allEntriesButton.SetValue(OpacityProperty, 1);
			settingsButton.SetValue(OpacityProperty, 1);
			newJobButton.SetValue(OpacityProperty, 1);
		}
		#endregion
	}
}
