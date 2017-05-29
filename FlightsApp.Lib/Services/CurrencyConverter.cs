using System.Collections.Generic;
using FlightsApp.Lib.Models;

namespace FlightsApp.Lib.Services
{
    public class CurrencyConverter
    {
        private readonly Dictionary<string, double> CurrencyValuesFor1Euro;

        public CurrencyConverter(Dictionary<string, double> currencyValuesFor1Euro)
        {
            this.CurrencyValuesFor1Euro = currencyValuesFor1Euro;
            this.CurrencyValuesFor1Euro["EUR"] = 1;
        }

        public double GetPriceInEuro(Flight flight)
        {
            return flight.Price / this.CurrencyValuesFor1Euro[flight.CurrencyCode];
        }
    }
}
