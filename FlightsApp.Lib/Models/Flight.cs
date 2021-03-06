using System;
using System.Collections.Generic;
using System.Linq;
using FlightsApp.Lib.SearchProviders.Ryanair;
using FlightsApp.Lib.SearchProviders.Wizzair;

namespace FlightsApp.Lib.Models
{
    public class Flight
    {
        public Airport From { get; set; }
        public Airport To { get; set; }
        public double Price { get; set; }
        public string CurrencyCode { get; set; }
        public double PriceInEuro { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
		public Airline Airline { get; set; }

        public static List<Flight> FromWizzairFlight(WizzairFlights.Flight flight)
        {
            var flights = new List<Flight>();

            if (flight.priceType != "soldOut")
            {
                for (int i = 0; i < flight.departureDates.Count; i++)
                {
                    flights.Add(new Flight
                    {
                        From = Airport.FromCode(flight.departureStation),
                        To = Airport.FromCode(flight.arrivalStation),
                        DateFrom = flight.departureDates[i],
                        DateTo = flight.arrivalDates[i],
                        Price = flight.price.amount,
                        CurrencyCode = flight.price.currencyCode,
                        Airline = Airline.Wizzair
                    });
                }
            }

            return flights;
        }

        public static List<Flight> FromRyanairFlight(RyanairFlights flights)
        {
            if (flights.trips == null)
            {
                return new List<Flight>();
            }

            return flights.trips
                          .SelectMany(trip => trip.dates
                          .SelectMany(flightDate => flightDate.flights
                          .Where(flight => flight.regularFare != null && flight.regularFare.fares != null && flight.regularFare.fares.Any())
                          .Select(flight =>
                              new Flight
                              {
                                  From = Airport.FromCode(trip.origin),
                                  To = Airport.FromCode(trip.destination),
                                  DateFrom = flight.time[0],
                                  DateTo = flight.time[1],
								  Price = flight.regularFare.fares.Min(f => f.amount),
                                  CurrencyCode = flights.currency,
         						  Airline = Airline.Ryanair
                              }))
                           ).ToList();
        }
    }
}
