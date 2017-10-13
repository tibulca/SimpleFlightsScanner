using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightsApp.Lib.Models;
using FlightsApp.Lib.Utils;

namespace FlightsApp.Lib.SearchProviders.Ryanair
{
    public class BlueairSearchProvider : SearchProviderBase, ISearchProvider
    {
        public BlueairSearchProvider(ILogger logger, IApiHttpClient apiHttpClient)
            : base(Airline.Blueair, logger, apiHttpClient)
        {
        }

        protected override async Task<List<Flight>> SearchFlights(SearchCriteria searchCriteria)
        {
            var flights = new List<Flight>();

            var startMonth = new DateTime(searchCriteria.FromDate.Year, searchCriteria.FromDate.Month, 1);
            var endMonth = new DateTime(searchCriteria.ToDate.Year, searchCriteria.ToDate.Month, 1);

            while (startMonth <= endMonth)
            {
                var url = $"https://booking.blueairweb.com/Flight/InternalSelect?o1={searchCriteria.Route.Airport1.Code}&d1={searchCriteria.Route.Airport2.Code}&dd1={startMonth.ToString("yyyy-MM-dd")}&dd2={startMonth.ToString("yyyy-MM-dd")}&c=true&s=false&r=true&bc=EUR";
                var httpResult = await apiHttpClient.GetAsync(url);

                var monthFlights = ExtractFlights(httpResult, startMonth, searchCriteria);
                flights.AddRange(monthFlights);

                startMonth = startMonth.AddMonths(1);
            }

            await GetTimetable(searchCriteria);
            // set the time values from timetable to flights

            return flights.Where(f => f.DateFrom >= searchCriteria.FromDate
                                 && f.DateFrom <= searchCriteria.ToDate)
                          .ToList();
        }


        private IEnumerable<Flight> ExtractFlights(string html, DateTime month, SearchCriteria searchCriteria)
        {
            var returnFlightsStart = html.LastIndexOf("low-fare-cal mdl-shadow--2dp", StringComparison.OrdinalIgnoreCase);

            const string dayTag = "<span class=\"low-fare-cal-day-num\">";
            const string priceTag = "<span class=\"low-fare-cal-day-text\">";

            var pos = 0;

            while (pos >= 0)
            {
                pos = html.IndexOf(dayTag, pos, StringComparison.OrdinalIgnoreCase);
                if (pos > 0)
                {
                    var dayStartPos = pos + dayTag.Length;
                    var day = html.Substring(dayStartPos, html.IndexOf("<", dayStartPos) - dayStartPos);

                    pos = html.IndexOf(priceTag, pos, StringComparison.OrdinalIgnoreCase);
                    var priceStartPos = pos + priceTag.Length + 1;
                    var price = html.Substring(priceStartPos, html.IndexOf("<", priceStartPos) - priceStartPos);

                    var flight = new Flight
                    {
                        Airline = Airline.Blueair,
                        CurrencyCode = "EUR",
                        DateFrom = month.AddDays(int.Parse(day) - 1),
                        DateTo = month.AddDays(int.Parse(day) - 1), // get the time
                        From = pos < returnFlightsStart ? searchCriteria.Route.Airport1 : searchCriteria.Route.Airport2,
                        To = pos < returnFlightsStart ? searchCriteria.Route.Airport2 : searchCriteria.Route.Airport1,
                        Price = double.Parse(price),
                        PriceInEuro = double.Parse(price),
                    };

                    yield return flight;
                }
            }
        }

        private async Task GetTimetable(SearchCriteria searchCriteria)
        {
            await GetTimetable(searchCriteria.Route.Airport1, searchCriteria.Route.Airport2);
            await GetTimetable(searchCriteria.Route.Airport2, searchCriteria.Route.Airport1);

        }

        private async Task GetTimetable(Airport from, Airport to)
        {
            var url = $"https://webapi.blueairweb.com/api/RetrieveSchedule?o={from.Code}&d={to.Code}";
            var httpResult = await apiHttpClient.GetAsync(url);
            var timetable = Deserialize<BlueairTimetable>(httpResult);
        }
    }

    public class BlueairTimetable
    {
        public BlueairTimetableSchedule basicSchedule { get; set; }
    }

    public class BlueairTimetableSchedule
    {
        public List<BlueairTimetableEntry> summer { get; set; }
        public List<BlueairTimetableEntry> winter { get; set; }
        public DateTime summerStart { get; set; }
        public DateTime summerEnd { get; set; }
    }

    public class BlueairTimetableEntry
    {
        public string dayOfWeek { get; set; }
        public int numberOfStops { get; set; }
        public string startHour { get; set; }
        public string arrivalHour { get; set; }
    }
}