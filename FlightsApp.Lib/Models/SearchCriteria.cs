using System;

namespace FlightsApp.Lib.Models
{
    public class SearchCriteria
    {
        public Route Route;

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
