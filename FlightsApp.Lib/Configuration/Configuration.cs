using System.Collections.Generic;
using System.Linq;
using FlightsApp.Lib.Configuration.Airlines;
using FlightsApp.Lib.Configuration.Routes;
using FlightsApp.Lib.Models;

namespace FlightsApp.Lib.Configuration
{
    public static class Configuration
    {
		public static Dictionary<Airline, List<Airport>> AirlineAirports = new Dictionary<Airline, List<Airport>>
        {
            { Airline.Ryanair, Ryanair.Airports },
            { Airline.Wizzair, Wizzair.Airports },
            { Airline.Tarom, Tarom.Airports },
            { Airline.Blueair, Blueair.Airports }
        };

        public static List<Route> Routes = Enumerable.Empty<Route>()
                                                     .Union(ToRouteList(Airport.Bucharest, Bucuresti.Routes))
                                                     .Union(ToRouteList(Airport.ClujNapoca, ClujNapoca.Routes))
                                                     .Union(ToRouteList(Airport.Dublin, Dublin.Routes))
                                                     .Union(ToRouteList(Airport.Iasi, Iasi.Routes))
                                                     .Union(ToRouteList(Airport.Suceava, Suceava.Routes))
                                                     .ToList();
            
        private static IEnumerable<Route> ToRouteList(Airport fromAirport, List<Airport> toAirports)
        {
            return toAirports.Select(toAirport => new Route(fromAirport, toAirport));
        }
    }
}
