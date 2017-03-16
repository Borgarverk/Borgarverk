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
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.True(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest1()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = null;
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest2()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = null;
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest3()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest4()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest5()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest6()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest7()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest8()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest9()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "-4";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest10()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "0";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest11()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "-3";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest12()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "0";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest13()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "-3";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest14()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "0";
			viewModel.TarQty = "3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest15()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "-3";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest16()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "3";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "0";
			viewModel.Rate = "3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest17()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "0";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "-3";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}

		[Test]
		public void ValidEntryFalseTest18()
		{
			EntryViewModel viewModel = new EntryViewModel(new SendService());
			viewModel.Car = new Models.CarModel("Name");
			viewModel.Station = new Models.StationModel("Name");
			viewModel.No = "SomeNo";
			viewModel.RoadArea = "4";
			viewModel.RoadWidth = "0";
			viewModel.RoadLength = "3";
			viewModel.TarQty = "3";
			viewModel.Rate = "0";

			Assert.False(viewModel.ValidEntry());
			viewModel = null;
		}
	}
}
