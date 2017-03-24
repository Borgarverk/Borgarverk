using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Borgarverk
{
	public partial class TimePage : ContentPage
	{
		public TimePage()
		{
			InitializeComponent();
			var viewModel = new TimeViewModel();
			BindingContext = viewModel;
		}
	}
}
