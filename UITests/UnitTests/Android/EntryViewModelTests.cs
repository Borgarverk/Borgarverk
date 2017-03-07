using System;
using Borgarverk.Models;
using Borgarverk.ViewModels;
using NUnit.Framework;
using Xamarin.UITest;

namespace Borgarverk.UITests
{
	[TestFixture(Platform.Android)]
	public class EntryViewModelTests
	{
		IApp app;
		Platform platform;
		public EntryViewModelTests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

		//[Test]
		//public void ValidEntryTrueTest()
		//{
		//	EntryViewModel viewModel = new EntryViewModel(new DataService(), new SendService());
		//	viewModel.Model.Car = "SomeCar";
		//	viewModel.Model.Station = "SomeStation";
		//	viewModel.Model.No = "SomeNo";
		//	viewModel.Model.RoadArea = "4";
		//	viewModel.Model.RoadWidth = "3";
		//	viewModel.Model.RoadLength = "3";
		//	viewModel.Model.TarQty = "3";
		//	viewModel.Model.Rate = "3";

		//	Assert.True(viewModel.ValidEntry());
		//}

		//[Test]
		//public void ValidEntryFalseTest()
		//{
		//	EntryViewModel viewModel = new EntryViewModel(new DataService(), new SendService());
		//	viewModel.Model.Car = "";
		//	viewModel.Model.Station = "SomeStation";
		//	viewModel.Model.No = "SomeNo";
		//	viewModel.Model.RoadArea = "4";
		//	viewModel.Model.RoadWidth = "3";
		//	viewModel.Model.RoadLength = "3";
		//	viewModel.Model.TarQty = "3";
		//	viewModel.Model.Rate = "3";

		//	Assert.False(viewModel.ValidEntry());
		//}
	}
}
