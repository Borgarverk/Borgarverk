using System;
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
			newEntryButton = new Button { Text = "Ný færsla" };
			allEntriesButton = new Button { Text = "Allar færslur" };
			borgarverkImage = new Image { Source = "Borgarverk_clearbackground.png" };
			layout = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Children = {
					borgarverkImage,
					newEntryButton,
					allEntriesButton
				}
			};
			newEntryButton.Clicked += NewEntry;
			allEntriesButton.Clicked += AllEntries;
			Content = layout;
		}

		public void NewEntry(object sender, EventArgs e)
		{
			Navigation.PushAsync(new NewEntryPage());
		}

		public void AllEntries(object sender, EventArgs e)
		{
			Navigation.PushAsync(new EntryListPage());
		}
	}
}
