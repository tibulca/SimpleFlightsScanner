using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FlightsApp.Lib.Models;
using FlightsApp.Lib.Utils;

namespace FlightsApp.Lib.SearchProviders.Ryanair
{
	public class RyanairSearchProvider : SearchProviderBase, ISearchProvider
	{
        private const int MAX_FLEX_DAYS = 6;

        public RyanairSearchProvider(ILogger logger, IApiHttpClient apiHttpClient)
            : base(Airline.Ryanair, logger, apiHttpClient)
		{
		}

		protected override async Task<List<Flight>> SearchFlights(SearchCriteria searchCriteria)
		{
			var flights = new List<Flight>();
            var currentSearchCriteria = GetCurrentSearchCriteria(searchCriteria.Route, searchCriteria.FromDate, searchCriteria.ToDate);

            while (currentSearchCriteria.FromDate <= searchCriteria.ToDate)
            {
                var airlineFlights = await DownloadFlightsAsync(currentSearchCriteria);
                flights.AddRange(Flight.FromRyanairFlight(airlineFlights));
			
				currentSearchCriteria = GetCurrentSearchCriteria(searchCriteria.Route, currentSearchCriteria.ToDate.AddDays(1), searchCriteria.ToDate);
            }

			return flights;
		}

        private SearchCriteria GetCurrentSearchCriteria(Route route, DateTime startDate, DateTime endDate)
        {
			return new SearchCriteria
			{
				FromDate = startDate,
				ToDate = DateUtils.Min(startDate.AddDays(MAX_FLEX_DAYS), endDate),
				Route = route
			};
        }

		private async Task<RyanairFlights> DownloadFlightsAsync(SearchCriteria searchCriteria)
		{
            var flexDays = DateUtils.DaysDiff(searchCriteria.FromDate, searchCriteria.ToDate);
            var url = $"https://desktopapps.ryanair.com/en-gb/availability?ADT=1&CHD=0&DateIn={searchCriteria.FromDateString}&DateOut={searchCriteria.FromDateString}&Destination={searchCriteria.Route.Airport2.Code}&FlexDaysIn={flexDays}&FlexDaysOut={flexDays}&INF=0&Origin={searchCriteria.Route.Airport1.Code}&RoundTrip=true&ToUs=AGREED&TEEN=0";

			var httpResult = await apiHttpClient.SendAsync(url, HttpMethod.Get, null, null, null);

			return Deserialize<RyanairFlights>(httpResult);
		}
	}
}