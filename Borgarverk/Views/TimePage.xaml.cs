using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Borgarverk
{
	public partial class TimePage : ContentPage
	{
		private TimeViewModel viewModel;
		public TimePage()
		{
			InitializeComponent();
			viewModel = new TimeViewModel(this.Navigation);
			BindingContext = viewModel;
		}
	}
}
