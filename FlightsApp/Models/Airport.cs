using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightsApp
{
    public class Airport
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public static List<Airport> All = new List<Airport>();

        // Romania
        public static Airport Suceava = new Airport("SCV", "Suceava");
        public static Airport ClujNapoca = new Airport("CLJ", "Cluj-Napoca");
        public static Airport Bucharest = new Airport("OTP", "Bucharest");
        public static Airport Iasi = new Airport("IAS", "Iasi");
        public static Airport Bacau = new Airport("BCM", "Bacau");
        public static Airport Oradea = new Airport("OMR", "Oradea");

        // Ireland
        public static Airport Dublin = new Airport("DUB", "Dublin");

        // UK
        public static Airport LondonLuton = new Airport("LTN", "London Luton");

        // Italy
        public static Airport Bari = new Airport("BRI", "Bari");
        public static Airport RomeCiampino = new Airport("CIA", "Rome Ciampino");
        public static Airport Bologna = new Airport("BLQ", "Bologna");
        public static Airport MilanBergamo = new Airport("BGY", "Milan Bergamo");
        public static Airport VeniceTreviso = new Airport("TSF", "Venice Treviso");

        // Belgium
        public static Airport BrusselsCharleroi = new Airport("CRL", "Brussels Charleroi");


        // Germany
        public static Airport BerlinSchonefeld = new Airport("SXF", "Berlin Schönefeld");
        public static Airport Koln = new Airport("CGN", "Köln");
        public static Airport FrankfurtHahn = new Airport("HHN", "Frankfurt Hahn");
        public static Airport Memmingen = new Airport("FMM", "Memmingen");

        // France
        public static Airport ParisBeauvais = new Airport("BVA", "Paris Beauvais");

        // Holland
        public static Airport Eindhoven = new Airport("EIN", "Eindhoven");

        // Hungary
        public static Airport Budapest = new Airport("BUD", "Budapest");

        // Slovakia
        public static Airport Bratislava = new Airport("BTS", "Bratislava");

        // Switzerland
        public static Airport Basel = new Airport("BSL", "Basel");

        // Malta
        public static Airport Malta = new Airport("MLA", "Malta");

        // Spain
        public static Airport Alicante = new Airport("ALC", "Alicante");
        public static Airport BarcelonaElPrat = new Airport("BCN", "BarcelonaElPrat");
        public static Airport Madrid = new Airport("MAD", "Madrid");
        public static Airport Malaga = new Airport("AGP", "Malaga");
        public static Airport Valencia = new Airport("VLC", "Valencia");

        private Airport(string code, string name)
        {
            this.Code = code;
            this.Name = name;

            All.Add(this);
        }

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
