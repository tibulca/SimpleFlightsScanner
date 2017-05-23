using System.Collections.Generic;

namespace FlightsApp.Configuration.Routes
{
    public static class Dublin
    {
        public static List<Airport> Routes = new List<Airport>
        {
            // blueair - direct flights
            Airport.ClujNapoca,
            Airport.Bacau,

            // ryanair, blueair - for stopover
            Airport.Bucharest,

            // ryanair - for stopover
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
        };
    }
}
