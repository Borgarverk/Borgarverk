using System;
using System.Collections.Generic;
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

		public void OnListViewItemSelected(object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("OnListViewItemSelected");
		}
	}
}
