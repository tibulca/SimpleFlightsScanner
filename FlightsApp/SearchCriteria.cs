using System;

namespace FlightsApp
{
	public class SearchCriteria
	{
		public string From { get; set; }
		public string To { get; set; }

		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }

		public string FromDateString
		{
			get { return FromDate.ToString("yyyy-MM-dd"); }
		}
		public string ToDateString
		{
			get { return ToDate.ToString("yyyy-MM-dd"); }
		}
	}
}
