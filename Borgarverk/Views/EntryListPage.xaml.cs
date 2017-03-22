using System;
using System.Collections.Generic;
using System.Diagnostics;
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

	}
}
