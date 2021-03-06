﻿using System.Collections.Generic;
using FlightsApp.Lib.Models;

namespace FlightsApp.Lib.Configuration.Routes
{
    public static class Suceava
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
        };
    }
}
