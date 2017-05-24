using System;
using System.Collections.Generic;

namespace FlightsApp.Lib.SearchProviders.Ryanair
{
    public class RyanairFlights
    {
        public string currency { get; set; }
        public List<FlightRoute> trips { get; set; }

        public class FlightRoute
        {
            public List<FlightDate> dates { get; set; }
            public string destination { get; set; }
            public string destinationName { get; set; }
            public string origin { get; set; }
            public string originName { get; set; }
        }
            
        public class FlightDate
        {
            public DateTime dateOut { get; set; }
            public List<Flight> flights { get; set; }
        }

        public class Flight
        {
            public int faresLeft { get; set; }
            public string duration { get; set; }
            public string flightNumber { get; set; }
            public List<DateTime> time { get; set; }
            public List<DateTime> timeUTC { get; set; }
            public Fare businessFare { get; set; }
            public Fare leisureFare { get; set; }
            public Fare regularFare { get; set; }
        }

        public class Fare
        {
            public List<FareDetails> fares { get; set; }
        }

        public class FareDetails
        {
            public double amount { get; set; }
            public double discountInPercent { get; set; }
            public bool hasDiscount { get; set; }
            public bool hasPromoDiscount { get; set; }
        }
    }
}
