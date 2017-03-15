using System;
using System.Collections.Generic;
using Borgarverk.ViewModels;
using Xamarin.Forms;

namespace Borgarverk
{
	public partial class CustomEditRowView : ContentView
	{
		public EntryListViewModel ViewModel { get; set; }

		public CustomEditRowView()
		{
			InitializeComponent();
		}
	}
}
