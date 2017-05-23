using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightsApp
{
    public class Airport
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        private Airport(string code, string name)
        {
            this.Code = code;
            this.Name = name;

            All.Add(this);
        }

        public static List<Airport> All = new List<Airport>();

        public static Airport Suceava = new Airport("SCV", "Suceava");
        public static Airport ClujNapoca = new Airport("CLJ", "Cluj-Napoca");
        public static Airport Bucharest = new Airport("OTP", "Bucharest");
        public static Airport Iasi = new Airport("IAS", "Iasi");
        public static Airport Bacau = new Airport("BCM", "Bacau");
        public static Airport Oradea = new Airport("OMR", "Oradea");

        public static Airport Dublin = new Airport("DUB", "Dublin");
        public static Airport LondonLuton = new Airport("LTN", "London Luton");
        public static Airport RomeCiampino = new Airport("CIA", "Rome Ciampino");
        public static Airport Bologna = new Airport("BLQ", "Bologna");
        public static Airport MilanBergamo = new Airport("BGY", "Milan Bergamo");
        public static Airport VeniceTreviso = new Airport("TSF", "Venice Treviso");

        public static Airport FromCode(string code)
        {
            return All.First(a => a.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }

        public override bool Equals(object obj)
        {
            var airport2 = obj as Airport;

            return airport2 != null && string.Equals(airport2.Code, this.Code);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
