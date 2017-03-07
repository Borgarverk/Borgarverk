using System;
using System.Collections.Generic;
using Borgarverk.ViewModels;
using Xamarin.Forms;

namespace Borgarverk
{
	public partial class SettingPage : ContentPage
	{
		private SettingsViewModel viewModel; 
		public SettingPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{

			base.OnAppearing();
			viewModel = new SettingsViewModel(new DataService());
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
