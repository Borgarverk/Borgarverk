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
		public EntryListPage()
		{
			InitializeComponent();
			BindingContext = new EntryListViewModel(new DataService(), new SendService());
			ThemeManager.ThemeName = Themes.Light;
			ThemeManager.RefreshTheme();
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
