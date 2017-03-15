using System;
using Borgarverk.ViewModels;
using DevExpress.Mobile.DataGrid;
using Xamarin.Forms;

namespace Borgarverk
{
	public class CustomEditRow : ContentView
	{
		public EntryListViewModel ViewModel { get; set; }

		public CustomEditRow()
		{
			Grid layoutGrid = CreateGrid();
			FillFormGrid(layoutGrid);
			Content = layoutGrid;
		}

		private Grid CreateGrid()
		{
			Grid layoutGrid = new Grid();

			layoutGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
			layoutGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200, GridUnitType.Star) });
			for (int i = 0; i < 7; i++)
			{
				layoutGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto});
			}

			layoutGrid.RowSpacing = 20;
			layoutGrid.ColumnSpacing = 20;
			return layoutGrid;
		}

		void FillFormGrid(Grid layoutGrid)
		{
			FillLabels(layoutGrid);
			FillEditors(layoutGrid);
		}

		void FillLabels(Grid layoutGrid)
		{
			Label noLabel = new Label()
			{
				Text = "Færslunúmer",
				FontSize = 18,
				VerticalOptions = LayoutOptions.Center
			};
			Label carLabel = new Label()
			{
				Text = "Bíll:",
				FontSize = 18,
				VerticalOptions = LayoutOptions.Center
			};
			Label stationLabel = new Label()
			{
				Text = "Stöð",
				FontSize = 18,
				VerticalOptions = LayoutOptions.Start
			};
			Label widthLabel = new Label()
			{
				Text = "Breidd",
				FontSize = 18,
				VerticalOptions = LayoutOptions.Center
			};
			Label lengthLabel = new Label()
			{
				Text = "Lengd",
				FontSize = 18,
				VerticalOptions = LayoutOptions.Center
			};
			Label areaLabel = new Label()
			{
				Text = "Flatarmál:",
				FontSize = 18,
				VerticalOptions = LayoutOptions.Center
			};
			Label qtyLabel = new Label()
			{
				Text = "Lítrar",
				FontSize = 18,
				VerticalOptions = LayoutOptions.Start
			};
			Label rateLabel = new Label()
			{
				Text = "Lítrar/fm",
				FontSize = 18,
				VerticalOptions = LayoutOptions.Center
			};

			layoutGrid.Children.Add(noLabel, 0, 0);
			layoutGrid.Children.Add(carLabel, 0, 1);
			layoutGrid.Children.Add(stationLabel, 0, 2);
			layoutGrid.Children.Add(widthLabel, 0, 3);
			layoutGrid.Children.Add(lengthLabel, 0, 4);
			layoutGrid.Children.Add(areaLabel, 0, 5);
			layoutGrid.Children.Add(qtyLabel, 0, 6);
			layoutGrid.Children.Add(rateLabel, 0, 7);
		}

		void FillEditors(Grid layoutGrid)
		{
			Entry noEntry = new Entry()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center
			};
			noEntry.SetBinding(Entry.TextProperty,
										new Binding(EditValuesContainer.GetBindingPath("No")));
			
			Entry widthEntry = new Entry()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center
			};
			widthEntry.SetBinding(Entry.TextProperty,
										new Binding(EditValuesContainer.GetBindingPath("RoadWidth")));
			widthEntry.Keyboard = Keyboard.Numeric;

			Entry lengthEntry = new Entry()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center
			};
			lengthEntry.SetBinding(Entry.TextProperty,
										new Binding(EditValuesContainer.GetBindingPath("RoadLength")));
			lengthEntry.Keyboard = Keyboard.Numeric;

			Entry areaEntry = new Entry()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center
			};
			areaEntry.SetBinding(Entry.TextProperty,
										new Binding(EditValuesContainer.GetBindingPath("RoadArea")));
			areaEntry.Keyboard = Keyboard.Numeric;

			Entry qtyEntry = new Entry()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center
			};
			qtyEntry.SetBinding(Entry.TextProperty,
										new Binding(EditValuesContainer.GetBindingPath("TarQty")));
			qtyEntry.Keyboard = Keyboard.Numeric;

			Entry rateEntry = new Entry()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center
			};
			rateEntry.SetBinding(Entry.TextProperty,
										new Binding(EditValuesContainer.GetBindingPath("Rate")));
			rateEntry.Keyboard = Keyboard.Numeric;
			
			layoutGrid.Children.Add(noEntry, 1, 0);
			layoutGrid.Children.Add(widthEntry, 1, 3);
			layoutGrid.Children.Add(lengthEntry, 1, 4);
			layoutGrid.Children.Add(areaEntry, 1, 5);
			layoutGrid.Children.Add(qtyEntry, 1, 6);
			layoutGrid.Children.Add(rateEntry, 1, 7);
		}

	}
}

