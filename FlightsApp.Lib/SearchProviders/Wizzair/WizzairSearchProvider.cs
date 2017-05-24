using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FlightsApp.Lib.Models;
using FlightsApp.Lib.Utils;

namespace FlightsApp.Lib.SearchProviders.Wizzair
{
	public class WizzairSearchProvider : SearchProviderBase, ISearchProvider
    {
        public WizzairSearchProvider(ILogger logger, IApiHttpClient apiHttpClient)
            : base(Airline.Wizzair, logger, apiHttpClient)
        {
        }

		protected override async Task<List<Flight>> SearchFlights(SearchCriteria searchCriteria)
        {
            var airlineFlights = await DownloadFlightsAsync(searchCriteria);

            var flights = airlineFlights.outboundFlights
                                        .Union(airlineFlights.returnFlights)
                                        .SelectMany((flightsOnDate) => Flight.FromWizzairFlight(flightsOnDate))
                                        .ToList();

            return flights;
        }

        private async Task<WizzairFlights> DownloadFlightsAsync(SearchCriteria searchCriteria)
        {
            var url = "https://be.wizzair.com/5.1.4/Api/search/timetable";
            var contentType = "application/json";
            var requestBody = GetRequestBody(searchCriteria);
            var headers = GetRequestHeaders();

            var httpResult = await apiHttpClient.SendAsync(url, HttpMethod.Post, requestBody, contentType, headers);

			return Deserialize<WizzairFlights>(httpResult);
        }

        private Dictionary<string, string> GetRequestHeaders()
        {
			return new Dictionary< string, string>
			{
				{ "accept", "application/json, text/plain, */*"},
                { "accept-language", "en-US,en;q=0.8,ro;q=0.6,de-DE;q=0.4,de;q=0.2"},
                { "cache-control", "no-cache"},
                { "origin", "https://wizzair.com"},
                { "pragma", "no-cache"},
                { "referer", "https://wizzair.com/en-gb/information-and-services/destinations/timetable"},
                { "user-agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.98 Safari/537.36"}

			};
        }

		private string GetRequestBody(SearchCriteria searchCriteria)
        {
			return $@"
{{
    'flightList': [
        {{
            'arrivalStation': '{searchCriteria.Route.Airport2.Code}',
            'departureStation': '{searchCriteria.Route.Airport1.Code}',
            'from': '{searchCriteria.FromDateString}',
            'to': '{searchCriteria.ToDateString}'
        }},
        {{
            'arrivalStation': '{searchCriteria.Route.Airport1.Code}',
            'departureStation': '{searchCriteria.Route.Airport2.Code}',
            'from': '{searchCriteria.FromDateString}',
            'to': '{searchCriteria.ToDateString}'
        }}
    ],
    'priceType': 'wdc'
}}".Replace("'", "\"");
        }
    }
}