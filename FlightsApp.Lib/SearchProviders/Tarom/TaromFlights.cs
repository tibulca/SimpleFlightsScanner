using System;
using System.Collections.Generic;

namespace FlightsApp.Lib.SearchProviders.Tarom
{
    public class TaromFlights
    {
        public List<List<Flight>> matrix { get; set; }

        public class Flight
        {
            public DateTime outboundDate { get; set; } // todo: datetime
            public Price outboundPrice { get; set; }
        }

        public class Price 
        {
            public double totalAmount { get; set; }
            public Currency currency { get; set; }
        }

        public class Currency
        {
            public string code { get; set; }
        }
    }
}
