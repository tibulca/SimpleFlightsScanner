using System.Collections.Generic;

namespace FlightsApp.Configuration.Routes
{
    public static class Iasi
    {
        public static List<Airport> Routes = new List<Airport>
        {
            // wizzair - for stopover
            Airport.LondonLuton,
            Airport.RomeCiampino,
            Airport.Bologna,
            Airport.MilanBergamo,
            Airport.VeniceTreviso,

            // tarom - for stopover
            Airport.Bucharest,

            // blueair - for stopover
            Airport.ClujNapoca,
        };
    }
}
