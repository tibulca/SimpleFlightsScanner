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
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
		public Airline Airline { get; set; }

        public static List<Flight> FromWizzairFlight(WizzairFlights.Flight flight)
        {
            if (flight.priceType == "soldOut")
            {
                return new List<Flight>();
            }

            return flight.departureDates
                         .Select((date) => new Flight
                         {
                            From = Airport.FromCode(flight.departureStation),
                            To = Airport.FromCode(flight.arrivalStation),
                            DateFrom = date,
                            DateTo = date.AddHours(2), // todo: get the correct hour using normal search? 
                            Price = flight.price.amount,
                            CurrencyCode = flight.price.currencyCode,
                            Airline = Airline.Wizzair
                         }).ToList();
        }

        public static List<Flight> FromRyanairFlight(RyanairFlights flights)
        {
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
