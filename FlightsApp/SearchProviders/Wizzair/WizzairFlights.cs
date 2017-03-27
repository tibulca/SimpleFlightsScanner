using System;
using System.Collections.Generic;

namespace FlightsApp
{
    public class WizzairFlights
    {
        public List<Flight> outboundFlights { get; set; }
        public List<Flight> returnFlights { get; set; }

        public class Flight
        {
            public string arrivalStation { get; set; }
            public DateTime departureDate { get; set; }
            public List<DateTime> departureDates { get; set; }
            public string departureStation { get; set; }
            public Price price { get; set; }
            public string priceType { get; set; }
        }

        public class Price
        {
            public double amount { get; set; }
            public string currencyCode { get; set; }
        }
    }
}
