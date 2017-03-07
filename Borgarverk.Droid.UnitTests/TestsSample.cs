using System;
using Borgarverk.ViewModels;
using NUnit.Framework;

namespace Borgarverk.Droid.UnitTests
{
	[TestFixture]
	public class TestsSample
	{

		[SetUp]
		public void Setup() { }


		[TearDown]
		public void Tear() { }

		[Test]
		public void Pass()
		{
			Console.WriteLine("test1");
			Assert.True(true);
		}

		[Test]
		public void Fail()
		{
			Assert.False(true);
		}

		[Test]
		[Ignore("another time")]
		public void Ignore()
		{
			Assert.True(false);
		}

		[Test]
		public void Inconclusive()
		{
			Assert.Inconclusive("Inconclusive");
		}

		[Test]
		public void ValidEntryTrueTest()
		{
			EntryViewModel viewModel = new EntryViewModel(new DataService(), new SendService());
			viewModel.Model.Car = "SomeCar";
			viewModel.Model.Station = "SomeStation";
			viewModel.Model.No = "SomeNo";
			viewModel.Model.RoadArea = "4";
			viewModel.Model.RoadWidth = "3";
			viewModel.Model.RoadLength = "3";
			viewModel.Model.TarQty = "3";
			viewModel.Model.Rate = "3";

			Assert.True(viewModel.ValidEntry());
		}

		[Test]
		public void ValidEntryFalseTest()
		{
			EntryViewModel viewModel = new EntryViewModel(new DataService(), new SendService());
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
