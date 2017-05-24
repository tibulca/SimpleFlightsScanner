using System;

namespace FlightsApp.Lib.Utils
{
    public static class DateUtils
    {
        public static int DaysDiff(DateTime startDate, DateTime endDate)
        {
			return (int)TimeSpan.FromTicks(endDate.Ticks - startDate.Ticks).TotalDays;
        }

		public static DateTime Max(DateTime d1, DateTime d2)
		{
			return d1.Ticks > d2.Ticks ? d1 : d2;
		}

        public static DateTime Min(DateTime d1, DateTime d2)
        {
            return d1.Ticks < d2.Ticks ? d1 : d2;
        }
	}
}
