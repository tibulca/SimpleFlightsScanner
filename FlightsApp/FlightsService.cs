using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightsApp
{
	public class FlightsService
	{
		private readonly List<ISearchProvider> searchProviders;
		private readonly ILogger logger;
		private readonly IApiHttpClient apiHttpClient;

		public FlightsService(ILogger logger)
		{
			this.logger = logger;
			this.apiHttpClient = new ApiHttpClient();//new ApiHttpClientMock();
			this.logger = logger;
			this.searchProviders = new List<ISearchProvider>
			{
				new WizzairSearchProvider(this.logger, this.apiHttpClient)
			};
		}

		public void Search(DateTime startDate, DateTime endDate)
		{
			logger.Info($"Search flights from date range {startDate.ToString("dd/MMM/yyyy")} - {endDate.ToString("dd/MMM/yyyy")}");
			logger.Info(string.Empty);

			var destinations = new List<Route>
			{
				new Route(Airport.Suceava, Airport.LondonLuton),
				new Route(Airport.Suceava, Airport.RomeCiampino),
				new Route(Airport.Suceava, Airport.Bologna),
				new Route(Airport.Suceava, Airport.MilanBergamo),
				new Route(Airport.Suceava, Airport.VeniceTreviso)
			};

			var flights = new List<Flight>();

			destinations.ForEach(route =>
			{
				logger.Info($"ROUTE: {route.Airport1} - {route.Airport2}");

				var searchCriteria = new SearchCriteria
				{
					Route = route,
					FromDate = startDate,
					ToDate = endDate
				};

				GetSearchProvider(route).ForEach(searchProvider =>
				{
					var routeFlights = searchProvider.Search(searchCriteria).Result;
					flights.AddRange(routeFlights);
					LogFlights(routeFlights, searchProvider, route);
					logger.Info(string.Empty);
				});
			});

			logger.Info("____________________________________");
		}

		private List<ISearchProvider> GetSearchProvider(Route route)
		{
			return this.searchProviders.Where(sp => sp.CanHandleRoute(route)).ToList();
		}

		private void LogFlights(List<Flight> flights, ISearchProvider searchProvider, Route route)
		{
			logger.Info($"\t{searchProvider.Name}");

			LogFlightStatistics(flights.Where(f => f.From.Equals(route.Airport1)).ToList(), "-->");
			LogFlightStatistics(flights.Where(f => f.From.Equals(route.Airport2)).ToList(), "<--");

			// logger.Info(JSONSerializer.ToJSON(flights));
		}

		private void LogFlightStatistics(List<Flight> flights, string direction)
		{
			logger.Info($"\t\t{direction} {flights.Count} flights");
			if (flights.Count > 0)
			{
				logger.Info($"\t\t{direction} [{flights.Min(f => f.Price)} - {flights.Max(f => f.Price)}] {flights.First().CurrencyCode}");
			}
		}
	}
}
