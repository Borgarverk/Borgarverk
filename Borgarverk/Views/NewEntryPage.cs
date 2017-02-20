using System;
using Xamarin.Forms;

namespace Borgarverk.Views
{
	public class NewEntryPage : ContentPage
	{
		#region Private member variables
		private Entry carEntry, placeEntry;
		private Label carLabel, placeLabel;
		private EntryViewModel model;
		private StackLayout layout;
		private Button submitButton;
		#endregion

		#region Constructor
		public NewEntryPage()
		{
			// Entry boxes
			carEntry = new Entry { Placeholder = "MV-988" };

			placeEntry = new Entry { Placeholder = "Kóp" };

			carLabel = new Label { Text= "Bíll" };
			placeLabel = new Label { Text = "Staður" };

			submitButton = new Button
			{
				Text = "Staðfesta",
				BackgroundColor = Color.Red
			};
			submitButton.Clicked += SubmitForm;

			Content = new StackLayout
			{
				Children = 
				{
					carLabel,
					carEntry,
					placeLabel,
					placeEntry,
					submitButton
				}
			};

		}
		#endregion

		void SubmitForm(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}

