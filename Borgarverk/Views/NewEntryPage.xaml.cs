using System;
using System.Collections.Generic;
using System.ComponentModel;
using Borgarverk.Models;
using Borgarverk.ViewModels;
using DevExpress.Mobile.DataGrid;
using DevExpress.Mobile.DataGrid.Theme;
using Xamarin.Forms;

namespace Borgarverk
{
	public partial class NewEntryPage : ContentPage
	{
		private EntryViewModel viewModel;
		public NewEntryPage()
		{
			InitializeComponent();
			viewModel = new EntryViewModel(this.Navigation, new SendService());
			BindingContext = viewModel;
		}

		public NewEntryPage(EntryViewModel viewModel)
		{
			InitializeComponent();
			this.viewModel = viewModel;
			BindingContext = viewModel;
		}

		public NewEntryPage(ISendService service, EntryModel model)
		{
			InitializeComponent();
			this.viewModel = viewModel = new EntryViewModel(this.Navigation, service, model);
			BindingContext = viewModel;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			//viewModel = new EntryViewModel(new DataService(), this.Navigation, new SendService());
			//BindingContext = viewModel;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			viewModel = null;
			BindingContext = null;
		}
	}
}
