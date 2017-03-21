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
