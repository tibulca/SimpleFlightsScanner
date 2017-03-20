using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace FlightsApp
{
	public class Wizzair
	{
		private readonly Logger logger;
		private readonly IApiHttpClient apiHttpClient;

		public Wizzair(Logger logger, IApiHttpClient apiHttpClient)
		{
			this.logger = logger;
			this.apiHttpClient = apiHttpClient;
		}

		public async Task<List<Flight>> Search(SearchCriteria searchCriteria)
		{

			try
			{
				var wizzairFlights = await DownloadFlightsAsync(searchCriteria);

				var flights = wizzairFlights.outboundFlights
											.Union(wizzairFlights.returnFlights)
											.SelectMany((flightsOnDate) => Flight.FromWizzairFlight(flightsOnDate))
											.ToList();

				logger.Info("Wizzair: " + searchCriteria.From + " - " + searchCriteria.To + ", " + searchCriteria.FromDate + " - " + searchCriteria.ToDate);
	            logger.Info("flights: " + flights.Count);
				if (flights.Count > 0)
				{
					logger.Info("cheapest: " + flights.Min(f => f.Price));
					logger.Info("most expensive: " + flights.Max(f => f.Price));
					logger.Info("currency: " + flights.First().CurrencyCode);
				}
				logger.Info(JSONSerializer.ToJSON(flights));

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
			var url = "https://be.wizzair.com/4.1.0/Api/search/timetable";
			var contentType = "application/json";
			var requestBody = "{\"flightList\":[{\"departureStation\":\"" + searchCriteria.From +
							  "\",\"arrivalStation\":\"" + searchCriteria.To +
                              "\",\"from\":\"" + searchCriteria.FromDateString +
							  "\",\"to\":\"" + searchCriteria.ToDateString +
							  "\"},{\"departureStation\":\"" + searchCriteria.To +
							  "\",\"arrivalStation\":\"" + searchCriteria.From +
                              "\",\"from\":\"" + searchCriteria.FromDateString +
							  "\",\"to\":\"" + searchCriteria.ToDateString +
							  "\"}],\"priceType\":\"wdc\"}";
			
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