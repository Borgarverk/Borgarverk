using System;
using System.Collections.Generic;
using Borgarverk.ViewModels;
using Xamarin.Forms;

namespace Borgarverk
{
	public partial class SettingPage : ContentPage
	{
		public SettingPage()
		{
			InitializeComponent();
			BindingContext = new SettingsViewModel(new DataService());
		}
	}
}
