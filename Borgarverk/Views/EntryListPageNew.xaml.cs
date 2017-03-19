using System;
using System.Collections.Generic;
using Borgarverk.Models;
using Borgarverk.ViewModels;
using Xamarin.Forms;

namespace Borgarverk
{
	public partial class EntryListPageNew : ContentPage
	{
		private EntryListViewModel viewModel;
		
		public EntryListPageNew()
		{
			InitializeComponent();
			viewModel = new EntryListViewModel(new SendService());
		}

		// TODO: implement (async or not?)
		public /*async*/ void SendButtonClicked(object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine(sender);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
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
		//}
	}
}
