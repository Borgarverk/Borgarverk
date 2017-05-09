using System;
using Borgarverk.Models;

namespace Borgarverk
{
	public class InfoFactory
	{
		public InfoFactory()
		{
		}

		public InfoModel ConstructInfo (EntryModel model, int mobileId)
		{
			var info = new InfoModel();

			info.roadworkid = model.ID;
			info.mobile = mobileId;
			info.station = model.Station;
			info.number = model.No;
			info.job_number = model.JobNo;
			info.road_length = model.RoadLength;
			info.road_width = model.RoadWidth;
			info.road_area = model.RoadArea;
			info.tar_quantity = model.TarQty;
			info.rate = model.Rate;
			info.degrees = model.Degrees;
			info.comment = model.Comment;
			info.time_sent = model.TimeSent;
			info.time_created = model.TimeCreated;
			info.start_time = model.StartTime;
			info.end_time = model.EndTime;

			return info;
		}
	}
}

