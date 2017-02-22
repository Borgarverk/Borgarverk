using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace Borgarverk
{
	public partial class NewEntryPage : ContentPage
	{
		public NewEntryPage()
		{
			InitializeComponent();
			BindingContext = new EntryViewModel();
		}

		private void ValidEntries(object sender, PropertyChangedEventArgs e)
		{
			//confirmButton.IsEnabled = validWidth.IsValid && validLength.IsValid && validArea.IsValid
			//	&& validQty.IsValid && validRate.IsValid && validNo.IsValid;
		}
	}
}
