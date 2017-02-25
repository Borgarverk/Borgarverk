using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Borgarverk
{
	public partial class EntryListPage : ContentPage
	{
		public EntryListPage()
		{
			InitializeComponent();
			BindingContext = new EntryListViewModel(new DataService());
		}
	}
}
