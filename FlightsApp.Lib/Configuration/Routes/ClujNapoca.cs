using System.Collections.Generic;
using FlightsApp.Lib.Models;

namespace FlightsApp.Lib.Configuration.Routes
{
    public static class ClujNapoca
    {
        public static List<Airport> Routes = new List<Airport>
        {
            // wizzair - for stopover
            Airport.LondonLuton,

            Airport.Bari,
            Airport.RomeCiampino,
            Airport.Bologna,
            Airport.MilanBergamo,
            Airport.VeniceTreviso,

            Airport.BrusselsCharleroi,

            Airport.BerlinSchonefeld,
            Airport.Koln,
            Airport.Memmingen,
            Airport.FrankfurtHahn,

            Airport.ParisBeauvais,

            Airport.Eindhoven,

            Airport.Budapest,

            Airport.Bratislava,

            Airport.Basel,

            Airport.Malta,

            Airport.Alicante,
            Airport.BarcelonaElPrat,
            Airport.Madrid,
            Airport.Malaga,
            Airport.Valencia,

            // tarom,blueair,wizzair - for stopover
            Airport.Bucharest,
        };
    }
}
