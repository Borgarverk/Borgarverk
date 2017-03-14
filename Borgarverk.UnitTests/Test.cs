using NUnit.Framework;
using System;
using Borgarverk.ViewModels;

namespace Borgarverk.UnitTests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestCase()
		{
		}

		[Test]
		public void ValidEntryTrueTest()
		{
			// TODO: testa þetta rétt

			//EntryViewModel viewModel = new EntryViewModel(new SendService());
			//viewModel.Model.Car = "SomeCar";
			//viewModel.Model.Station = "SomeStation";
			//viewModel.Model.No = "SomeNo";
			//viewModel.Model.RoadArea = "4";
			//viewModel.Model.RoadWidth = "3";
			//viewModel.Model.RoadLength = "3";
			//viewModel.Model.TarQty = "3";
			//viewModel.Model.Rate = "3";

			//Assert.True(viewModel.ValidEntry());
		}

		[Test]
		public void ValidEntryFalseTest()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Model.Car = "";
			viewModel.Model.Station = "SomeStation";
			viewModel.Model.No = "SomeNo";
			viewModel.Model.RoadArea = "4";
			viewModel.Model.RoadWidth = "3";
			viewModel.Model.RoadLength = "3";
			viewModel.Model.TarQty = "3";
			viewModel.Model.Rate = "3";

			Assert.False(viewModel.ValidEntry());
		}
	}
}
