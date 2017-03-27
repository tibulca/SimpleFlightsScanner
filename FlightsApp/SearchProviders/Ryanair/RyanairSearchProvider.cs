using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FlightsApp
{
	public class RyanairSearchProvider : ISearchProvider
	{
        private const int MAX_FLEX_DAYS = 6;
		private static readonly List<Airport> AirportsHandled = new List<Airport>
		{
			Airport.Bucharest,
            Airport.Oradea,

            Airport.Dublin,
            Airport.LondonLuton,
            Airport.RomeCiampino,
            Airport.Bologna,
            Airport.MilanBergamo,
            Airport.VeniceTreviso
        };

		private readonly ILogger logger;
		private readonly IApiHttpClient apiHttpClient;

		public string Name { get { return "Ryanair"; } }

		public RyanairSearchProvider(ILogger logger, IApiHttpClient apiHttpClient)
		{
			this.logger = logger;
			this.apiHttpClient = apiHttpClient;
		}

		public bool CanHandleRoute(Route route)
		{
			return AirportsHandled.Contains(route.Airport1) && AirportsHandled.Contains(route.Airport2);
		}

		public async Task<List<Flight>> Search(SearchCriteria searchCriteria)
		{

			try
			{
				var flights = new List<Flight>();

                var searchCriteriaWithLimit = new SearchCriteria
                {
                    FromDate = searchCriteria.FromDate,
					ToDate = DateUtils.Min(searchCriteria.FromDate.AddDays(MAX_FLEX_DAYS), searchCriteria.ToDate),
                    Route = searchCriteria.Route
                };

                while (searchCriteriaWithLimit.FromDate <= searchCriteria.ToDate)
                {
					var airlineFlights = await DownloadFlightsAsync(searchCriteriaWithLimit);

                    searchCriteriaWithLimit.FromDate = searchCriteriaWithLimit.ToDate.AddDays(1);
					searchCriteriaWithLimit.ToDate = DateUtils.Min(searchCriteriaWithLimit.FromDate.AddDays(MAX_FLEX_DAYS), searchCriteria.ToDate);

                    flights.AddRange(Flight.FromRyanairFlight(airlineFlights));
                }

				return flights;
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error(ex.StackTrace);
			}

			return await Task.Run(() => new List<Flight>());
		}

		private async Task<RyanairFlights> DownloadFlightsAsync(SearchCriteria searchCriteria)
		{
            var flexDays = DateUtils.DaysDiff(searchCriteria.FromDate, searchCriteria.ToDate);
            var url = $"https://desktopapps.ryanair.com/en-gb/availability?ADT=1&CHD=0&DateIn={searchCriteria.FromDateString}&DateOut={searchCriteria.FromDateString}&Destination={searchCriteria.Route.Airport2.Code}&FlexDaysIn={flexDays}&FlexDaysOut={flexDays}&INF=0&Origin={searchCriteria.Route.Airport1.Code}&RoundTrip=true&TEEN=0";

			var httpResult = await apiHttpClient.SendAsync(url, HttpMethod.Get, null, null, null);

			return JSONSerializer.FromJSON<RyanairFlights>(httpResult);
		}
	}
}