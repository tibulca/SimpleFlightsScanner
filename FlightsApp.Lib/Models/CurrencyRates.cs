using System;
using System.Collections.Generic;

namespace FlightsApp.Lib.Models
{
    public class CurrencyRates
    {
        public string Base { get; set; }

        public DateTime Date { get; set; }

        public Dictionary<string, double> Rates { get; set; }
    }
}
