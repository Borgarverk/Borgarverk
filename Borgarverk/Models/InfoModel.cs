using System;
namespace Borgarverk
{
	public class InfoModel
	{
		public int roadworkid { get; set; }
		public int mobile { get; set; }
		public string station { get; set; }
		public string number { get; set; }
		public int job_number { get; set; }
		public Double road_length { get; set; }
		public Double road_width { get; set; }
		public Double road_area { get; set; }
		public Double tar_quantity { get; set; }
		public Double rate { get; set; }
		public Double degrees { get; set; }
		public string comment { get; set; }
		public DateTime? time_sent { get; set; }
		public DateTime? time_created { get; set; }
		public DateTime? start_time { get; set; }
		public DateTime? end_time { get; set; }
	}
}
