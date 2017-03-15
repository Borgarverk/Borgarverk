using System;
using System.Collections.Generic;
using System.Diagnostics;
using Borgarverk.Models;
using Borgarverk.ViewModels;
using DevExpress.Mobile.DataGrid;
using DevExpress.Mobile.DataGrid.Theme;
using Xamarin.Forms;

namespace Borgarverk
{
	public partial class EntryListPage : ContentPage
	{
		private EntryListViewModel viewModel;
		private EntryModel model;

		public EntryListPage()
		{
			InitializeComponent();
			ThemeManager.ThemeName = Themes.Light;
			//ThemeManager.RefreshTheme();

			DataTemplate editFormContentTemplate = new DataTemplate(GetTemplate);
			grid.EditFormContent = editFormContentTemplate;
		}

		object GetTemplate()
		{
			return new CustomEditRow() { ViewModel = viewModel };
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			viewModel = new EntryListViewModel(new SendService());
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

		void OnRowEdit(object sender, RowEditingEventArgs e)
		{ 
			if (e.Action == EditingRowAction.Apply)
			{
				// Validate user input and get error string if not valid (this is working)
				string msg = viewModel.ModelValidation((EntryModel)grid.GetRow(e.RowHandle).DataObject);
				if (msg != "")
				{
					// need to display error msg and don't change the row (this is not working the row always changes)
					Application.Current.MainPage.DisplayAlert("Villa", msg, "Í lagi");
				}
				else
				{
					// the row changes and changes are saved to database (this works)
					viewModel.OnEntryEdited(e.SourceRowIndex);
				}
			}
			else // If user hit cancel no change (works)
			{
				return;
			}
		}
	}
}
