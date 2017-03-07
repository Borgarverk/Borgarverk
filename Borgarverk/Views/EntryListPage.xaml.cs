using System;
using System.Collections.Generic;
using System.Diagnostics;
using Borgarverk.ViewModels;
using DevExpress.Mobile.DataGrid;
using DevExpress.Mobile.DataGrid.Theme;
using Xamarin.Forms;

namespace Borgarverk
{
	public partial class EntryListPage : ContentPage
	{
		private EntryListViewModel viewModel;
		public EntryListPage()
		{
			InitializeComponent();
			ThemeManager.ThemeName = Themes.Light;
			ThemeManager.RefreshTheme();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			viewModel = new EntryListViewModel(new DataService(), new SendService());
			BindingContext = viewModel;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			viewModel = null;
			BindingContext = null;
		}

		void OnSwipeButtonShowing(object sender, SwipeButtonShowingEventArgs e)
		{
			if (((Boolean)grid.GetCellValue(e.RowHandle, "Sent")) && (e.ButtonInfo.ButtonName == "SendButton"))
			{
				e.IsVisible = false;
			}
		}
	}
}
