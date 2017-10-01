using System.Collections.Generic;
using FlightsApp.Lib.Models;

namespace FlightsApp.Lib.Configuration.Airlines
{
    public static class Blueair
    {
        public static List<Airport> Airports = new List<Airport>
        {
            Airport.Bucharest,
            Airport.ClujNapoca,
            Airport.Bacau,
            Airport.Iasi,

            Airport.Dublin
        };
    }
}
