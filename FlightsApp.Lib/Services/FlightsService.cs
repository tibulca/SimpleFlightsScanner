﻿using System;
using System.Collections.Generic;
using System.Linq;
using FlightsApp.Lib.Models;
using FlightsApp.Lib.SearchProviders;
using FlightsApp.Lib.SearchProviders.Ryanair;
using FlightsApp.Lib.SearchProviders.Tarom;
using FlightsApp.Lib.SearchProviders.Wizzair;
using FlightsApp.Lib.Utils;

namespace FlightsApp.Lib.Services
{
    public class FlightsService
    {
        private readonly List<ISearchProvider> searchProviders;
        private readonly ILogger logger;
        private readonly IApiHttpClient apiHttpClient;

        public FlightsService(ILogger logger)
        {
            this.logger = logger;

            apiHttpClient = new ApiHttpClient();//new ApiHttpClientMock();
            searchProviders = new List<ISearchProvider>
            {
                new WizzairSearchProvider(logger, apiHttpClient),
                new RyanairSearchProvider(logger, apiHttpClient),
				new TaromSearchProvider(logger, apiHttpClient)
            };
        }

        public List<Flight> Search(Airport from, Airport to, List<Airline> airlines, DateTime startDate, DateTime endDate)
        {
            logger.Info($"Search flights from date range {startDate.ToString("ddd dd/MMM/yyyy")} - {endDate.ToString("ddd dd/MMM/yyyy")}");
            logger.Info(string.Empty);

            var activeSearchProviders = searchProviders.Where(sp => airlines.Contains(sp.Airline)).ToList();

            var flights = new List<Flight>();

            GetMatchingRoutes(from, to).ForEach(route =>
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

            return flights;
        }

        private List<Route> GetMatchingRoutes(Airport from, Airport to)
        {
            var allRoutes = Configuration.Configuration.Routes;
            var directRoutes = allRoutes.Where(r => r.ContainsAll(from, to));

            var startRoutes = allRoutes.Where(r => r.ContainsAll(from) && !r.ContainsAll(from, to)).ToList();
            var endRoutes = allRoutes.Where(r => r.ContainsAll(to) && !r.ContainsAll(from, to)).ToList();
            var routesWithStopover = new List<Route>();
            startRoutes.ForEach(startRoute =>
            {
                var matchingEndRoute = endRoutes.FirstOrDefault(endRoute => startRoute.Airport1 == endRoute.Airport1 || 
                                                                            startRoute.Airport2 == endRoute.Airport2 || 
                                                                            startRoute.Airport1 == endRoute.Airport2);
                if (matchingEndRoute != null)
                {
                    routesWithStopover.Add(startRoute);
                    routesWithStopover.Add(matchingEndRoute);
                }
            });

            return directRoutes.Union(routesWithStopover).ToList();
        }

        private List<ISearchProvider> GetSearchProvider(List<ISearchProvider> providers, Route route)
        {
            return providers.Where(sp => sp.CanHandleRoute(route)).ToList();
        }

        private void LogFlights(List<Flight> flights, ISearchProvider searchProvider, Route route)
        {
            logger.Info($"\t{searchProvider.Airline.Name}");

            LogFlightStatistics(flights.Where(f => f.From.Equals(route.Airport1)).ToList(), "-->");
            LogFlightStatistics(flights.Where(f => f.From.Equals(route.Airport2)).ToList(), "<--");
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