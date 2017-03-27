using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace FlightsApp
{
    public class WizzairSearchProvider : ISearchProvider
    {
        private static readonly List<Airport> AirportsHandled = new List<Airport>
        {
            Airport.Suceava,
            Airport.LondonLuton,
            Airport.RomeCiampino,
            Airport.Bologna,
            Airport.MilanBergamo,
            Airport.VeniceTreviso
        };

        private readonly ILogger logger;
        private readonly IApiHttpClient apiHttpClient;

        public string Name { get { return "Wizzair"; } }

        public WizzairSearchProvider(ILogger logger, IApiHttpClient apiHttpClient)
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
                var airlineFlights = await DownloadFlightsAsync(searchCriteria);

                var flights = airlineFlights.outboundFlights
                                            .Union(airlineFlights.returnFlights)
                                            .SelectMany((flightsOnDate) => Flight.FromWizzairFlight(flightsOnDate))
                                            .ToList();

                return flights;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return await Task.Run(() => new List<Flight>());
        }

        private async Task<WizzairFlights> DownloadFlightsAsync(SearchCriteria searchCriteria)
        {
            var url = "https://be.wizzair.com/4.2.0/Api/search/timetable";
            var contentType = "application/json";
            var requestBody = $@"
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
            
            var headers = new Dictionary<string, string>
            {
                {"accept", "application/json, text/plain, */*"},
                {"accept-language", "en-US,en;q=0.8,ro;q=0.6,de-DE;q=0.4,de;q=0.2"},
                {"cache-control", "no-cache"},
                {"origin", "https://wizzair.com"},
                {"pragma", "no-cache"},
                {"referer", "https://wizzair.com/en-gb/information-and-services/destinations/timetable"},
                {"user-agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.98 Safari/537.36"}

            };

            var httpResult = await apiHttpClient.SendAsync(url, HttpMethod.Post, requestBody, contentType, headers);

            return JSONSerializer.FromJSON<WizzairFlights>(httpResult); ;
        }
    }
}