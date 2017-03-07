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

		public async void NewEntryButtonClicked(object sender, EventArgs e)
		{
			newEntryButton.SetValue(OpacityProperty, 0.2);
			await Task.Delay(100);
			await Navigation.PushAsync(new NewEntryPage());
		}

		public async void AllEntriesButtonClicked(object sender, EventArgs e)
		{
			allEntriesButton.SetValue(OpacityProperty, 0.2);
			await Task.Delay(100);
			await Navigation.PushAsync(new EntryListPage());
		}

		public async void SettingsButtonClicked(object sender, EventArgs e)
		{
			settingsButton.SetValue(OpacityProperty, 0.2);
			await Task.Delay(100);
			await Navigation.PushAsync(new SettingPage());
		}

		#region OnAppearing()
		protected override void OnAppearing()
		{
			base.OnAppearing();
			newEntryButton.SetValue(OpacityProperty, 1);
			allEntriesButton.SetValue(OpacityProperty, 1);
			settingsButton.SetValue(OpacityProperty, 1);
		}
		#endregion
	}
}
