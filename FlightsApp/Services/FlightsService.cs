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
                new WizzairSearchProvider(this.logger, this.apiHttpClient),
                new RyanairSearchProvider(this.logger, this.apiHttpClient),
            };
        }

        public void Search(List<string> airlines, DateTime startDate, DateTime endDate)
        {
            logger.Info($"Search flights from date range {startDate.ToString("dd/MMM/yyyy")} - {endDate.ToString("dd/MMM/yyyy")}");
            logger.Info(string.Empty);

            var activeSearchProviders = this.searchProviders.Where(sp => airlines.Contains(sp.Name)).ToList();

            var flights = new List<Flight>();

            RouteConfiguration.Routes.ForEach(route =>
            {
                logger.Info($"ROUTE: {route.Airport1} - {route.Airport2}");

                var searchCriteria = new SearchCriteria
                {
                    Route = route,
                    FromDate = startDate,
                    ToDate = endDate
                };

                GetSearchProvider(activeSearchProviders, route).ForEach(searchProvider =>
                {
                    var routeFlights = searchProvider.Search(searchCriteria).Result;
                    flights.AddRange(routeFlights);
                    LogFlights(routeFlights, searchProvider, route);
                    logger.Info(string.Empty);
                });
            });

            logger.Info("____________________________________");
        }

        private List<ISearchProvider> GetSearchProvider(List<ISearchProvider> providers, Route route)
        {
            return providers.Where(sp => sp.CanHandleRoute(route)).ToList();
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

