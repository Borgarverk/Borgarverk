using System;
using System.Collections.Generic;
using Borgarverk.Models;
using Borgarverk.ViewModels;
using Xamarin.Forms;

namespace Borgarverk
{
	public partial class EntryListPage : ContentPage
	{
		private EntryListViewModel viewModel;
		
		public EntryListPage()
		{
			InitializeComponent();
		}

		// TODO: implement (async or not?)
		public /*async*/ void SendButtonClicked(object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine(sender);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			viewModel = new EntryListViewModel(new SendService(), this.Navigation);
			BindingContext = viewModel;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			viewModel = null;
			BindingContext = null;
		}

		// TODO: implement
		private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("SearchBar_OnTextChanged");
		}

		// TODO: reyna að gera þetta í entrylistviewmodel frekar
		//private void onEntryTapped(object sender, ItemTappedEventArgs e)
		//{
		//	EntryModel entry = e.Item as EntryModel;
		//	System.Diagnostics.Debug.WriteLine("onEntryTapped");
		//	viewModel.SelectedEntry = entry;
			
		//	//if (viewModel.SelectedEntry == null)
		//	//{
		//	//	viewModel.SelectedEntry = entry;
		//	//}
		//

		public void OnMore(object sender, EventArgs e)
		{
			var mi = ((MenuItem)sender);
			DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
		}

		public void OnDelete(object sender, EventArgs e)
		{
			var mi = ((MenuItem)sender);
			DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
		}
	}
}
