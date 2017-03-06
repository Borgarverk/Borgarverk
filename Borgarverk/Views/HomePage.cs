using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Borgarverk.Views
{
	public partial class HomePage : ContentPage
	{
		#region Private member variables
		private StackLayout layout;
		private Button newEntryButton;
		private Button allEntriesButton;
		private Image borgarverkImage;
		#endregion

		public HomePage()
		{
			newEntryButton = new Button { Text = "Ný færsla", HeightRequest = 70, WidthRequest = 200};
			allEntriesButton = new Button { Text = "Allar færslur", HeightRequest = 70, WidthRequest = 200 };
			borgarverkImage = new Image { Source = "Borgarverk_clearbackground.png", HeightRequest = 200, WidthRequest = 300 };

			layout = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(60, 60, 60, 60),
				BackgroundColor = Color.FromRgb(255, 252, 242),
				Children =
				{
					borgarverkImage,
					newEntryButton,
					allEntriesButton
				}
			};
			newEntryButton.Clicked += NewEntry;
			allEntriesButton.Clicked += AllEntries;
			Content = layout;
		}

		public async void NewEntry(object sender, EventArgs e)
		{
			newEntryButton.SetValue(OpacityProperty, 0.2);
			await Task.Delay(100);
			await Navigation.PushAsync(new NewEntryPage());
		}

		public async void AllEntries(object sender, EventArgs e)
		{
			allEntriesButton.SetValue(OpacityProperty, 0.2);
			await Task.Delay(100);
			await Navigation.PushAsync(new EntryListPage());
		}

		#region OnAppearing()
		protected override void OnAppearing()
		{
			base.OnAppearing();
			newEntryButton.SetValue(OpacityProperty, 1);
			allEntriesButton.SetValue(OpacityProperty, 1);
		}
		#endregion
	}
}
