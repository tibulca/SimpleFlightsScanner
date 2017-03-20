using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightsApp
{
	public class Flight
	{
		public string From { get; set; }
		public string To { get; set; }
		public double Price { get; set; }
		public string CurrencyCode { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }

		public static List<Flight> FromWizzairFlight(WizzairFlights.Flight flight)
		{
			if (flight.priceType == "soldOut")
			{
				return new List<Flight>();
			}

			return flight.departureDates
						 .Select((date) => new Flight
						 {
						 	From = flight.departureStation,
							To = flight.arrivalStation,
							DateFrom = date,
							DateTo = DateTime.MinValue,
							Price = flight.price.amount,
							CurrencyCode = flight.price.currencyCode
						 }).ToList();
		}
	}
}
