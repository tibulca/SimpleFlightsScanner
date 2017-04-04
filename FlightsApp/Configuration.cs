﻿using System.Collections.Generic;

namespace FlightsApp
{
    public static class Configuration
    {
		public static Dictionary<Airline, List<Airport>> AirlineAirports = new Dictionary<Airline, List<Airport>>
        {
            {
                Airline.Ryanair,
                new List<Airport>
                {
                    Airport.Bucharest,
                    Airport.Oradea,

                    Airport.Dublin,
                    Airport.LondonLuton,
                    Airport.RomeCiampino,
                    Airport.Bologna,
                    Airport.MilanBergamo,
                    Airport.VeniceTreviso
                }
            },
            {
                Airline.Wizzair,
				new List<Airport>
				{
					Airport.Suceava,
					Airport.LondonLuton,
					Airport.RomeCiampino,
					Airport.Bologna,
					Airport.MilanBergamo,
					Airport.VeniceTreviso
				}
            }
        };
        
        public static List<Route> Routes = new List<Route>
        {
            // wizzair: suceava - for stopover
            new Route(Airport.Suceava, Airport.LondonLuton),
            new Route(Airport.Suceava, Airport.RomeCiampino),
            new Route(Airport.Suceava, Airport.Bologna),
            new Route(Airport.Suceava, Airport.MilanBergamo),
            new Route(Airport.Suceava, Airport.VeniceTreviso),

            // ryanair: dublin - direct flights
            new Route(Airport.Dublin, Airport.ClujNapoca),
            new Route(Airport.Dublin, Airport.Bucharest),
            new Route(Airport.Dublin, Airport.Bacau),

            // ryanair: dublin - for stopover
            new Route(Airport.Dublin, Airport.LondonLuton),
            new Route(Airport.Dublin, Airport.RomeCiampino),
            new Route(Airport.Dublin, Airport.Bologna),
            new Route(Airport.Dublin, Airport.MilanBergamo),
            new Route(Airport.Dublin, Airport.VeniceTreviso)
        };
    }
}