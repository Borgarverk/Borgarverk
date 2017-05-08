using System;
namespace Borgarverk
{
	public class InfoModel
	{
		public string roadworkid { get; set; }
		public string mobile { get; set; }
		public string station { get; set; }
		public string number { get; set; }
		public string job_number { get; set; }
		public string road_length { get; set; }
		public string road_width { get; set; }
		public string road_area { get; set; }
		public string tar_quantity { get; set; }
		public string rate { get; set; }
		public string degrees { get; set; }
		public string comment { get; set; }
		public DateTime? time_sent { get; set; }
		public DateTime? time_created { get; set; }
		public DateTime? start_time { get; set; }
		public DateTime? end_time { get; set; }
	}
}
