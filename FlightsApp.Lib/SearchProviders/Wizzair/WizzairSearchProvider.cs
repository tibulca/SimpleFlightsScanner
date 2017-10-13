using System;
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
            var apiBaseUrl = await GetApiBaseUrl();
            var url = $"{apiBaseUrl}/search/timetable";
            var contentType = "application/json";
            var requestBody = GetTimetableRequestBody(searchCriteria);
            var headers = GetRequestHeaders();

            var httpResult = await apiHttpClient.SendAsync(url, HttpMethod.Post, requestBody, contentType, headers);

			var flights = Deserialize<WizzairFlights>(httpResult);

            if (flights.outboundFlights.Any())
            {
                var durations = await GetFlightsDuration(searchCriteria,
                                                         flights.outboundFlights.FirstOrDefault()?.departureDate,
                                                         flights.returnFlights.FirstOrDefault()?.departureDate);

                flights.outboundFlights.ForEach(f => {
                    f.arrivalDates = f.departureDates.Select(d => d.AddMinutes(durations.Item1)).ToList();
                });

                flights.returnFlights.ForEach(f => {
                    f.arrivalDates = f.departureDates.Select(d => d.AddMinutes(durations.Item2)).ToList();
                });
            }

            return flights;
        }

        private async Task<Tuple<int, int>> GetFlightsDuration(SearchCriteria searchCriteria, DateTime? outboundDate, DateTime? inboundDate)
        {
            var apiBaseUrl = await GetApiBaseUrl();
            var url = $"{apiBaseUrl}/search/search";
            var contentType = "application/json";
            var requestBody = GetSearchRequestBody(searchCriteria, outboundDate, inboundDate);
            var headers = GetRequestHeaders();

            var httpResult = await apiHttpClient.SendAsync(url, HttpMethod.Post, requestBody, contentType, headers);

            var flights = Deserialize<WizzairDayFlights>(httpResult);

            return new Tuple<int, int>(GetDurationOrDefault(flights.outboundFlights.FirstOrDefault(), 120),
                                       GetDurationOrDefault(flights.returnFlights.FirstOrDefault(), 120));
        }

        private int GetDurationOrDefault(WizzairDayFlights.Flight flight, int defaultDuration)
        {
            if (flight == null)
            {
                return defaultDuration;
            }

            return (int)flight.arrivalDateTime.Subtract(flight.departureDateTime).TotalMinutes;
        }

        private async Task<string> GetApiBaseUrl()
        {
            var httpResult = await apiHttpClient.GetAsync("https://wizzair.com/static/metadata.json");
            var metadata = Deserialize<Dictionary<string, string>>(httpResult);

            return metadata["apiUrl"];
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

        private string GetTimetableRequestBody(SearchCriteria searchCriteria)
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

        private string GetSearchRequestBody(SearchCriteria searchCriteria, DateTime? outboundDate, DateTime? inboundDate)
        {
            return $@"
{{
    'adultCount': 1,
    'childCount': 0,
    'flightList': [
        {{
            'arrivalStation': '{searchCriteria.Route.Airport2.Code}',
            'departureDate': '{outboundDate.GetValueOrDefault(searchCriteria.FromDate).ToString("yyyy-MM-dd")}',
            'departureStation': '{searchCriteria.Route.Airport1.Code}'
        }},
        {{
            'arrivalStation': '{searchCriteria.Route.Airport1.Code}',
            'departureDate': '{inboundDate.GetValueOrDefault(searchCriteria.FromDate).ToString("yyyy-MM-dd")}',
            'departureStation': '{searchCriteria.Route.Airport2.Code}'
        }}
    ],
    'infantCount': 0,
    'isFlightChange': false,
    'isSeniorOrStudent': false,
    'rescueFareCode': '',
    'wdc': true
}}".Replace("'", "\"");
        }
    }
}