using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightsApp.Lib.Models
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
        public static Airport Dortmund = new Airport("DTM", "Dortmund");

        // Denmark
        public static Airport Billund = new Airport("BLL", "Billund");

        // Sweden
        public static Airport Malmo = new Airport("MMX", "Malmo");

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
        public static Airport Malaga = new Airport("AGP", "Malag");
        public static Airport Valencia = new Airport("VLC", "Valencia");


        public static Airport Catania = new Airport("CTA", "Catania");






        // for ryanair
        public static Airport Lanzarote = new Airport("ACE", "Lanzarote"); 
        public static Airport Amsterdam = new Airport("AMS", "Amsterdam"); 
        public static Airport Athens = new Airport("ATH", "Athens"); 
        public static Airport Barcelona = new Airport("BCN", "Barcelona T2"); 
        public static Airport Milan = new Airport("BGY", "Milan (Bergamo)"); 
        public static Airport Birmingham = new Airport("BHX", "Birmingham"); 
        public static Airport Biarritz = new Airport("BIQ", "Biarritz"); 
        public static Airport Bremen = new Airport("BRE", "Bremen"); 
        public static Airport Bristol = new Airport("BRS", "Bristol"); 
        public static Airport Brussels = new Airport("BRU", "Brussels"); 
        public static Airport Bydgoszcz = new Airport("BZG", "Bydgoszcz"); 
        public static Airport Carcassonne = new Airport("CCF", "Carcassonne"); 
        public static Airport CreteChania = new Airport("CHQ", "Crete (Chania)"); 
        public static Airport Comiso = new Airport("CIY", "Comiso"); 
        public static Airport Copenhagen = new Airport("CPH", "Copenhagen T2");
        public static Airport Cardiff = new Airport("CWL", "Cardiff"); 
        public static Airport Edinburgh = new Airport("EDI", "Edinburgh"); 
        public static Airport EastMidlands = new Airport("EMA", "East Midlands"); 
        public static Airport Faro = new Airport("FAO", "Faro"); 
        public static Airport Fuerteventura = new Airport("FUE", "Fuerteventura"); 
        public static Airport Gdansk = new Airport("GDN", "Gdansk"); 
        public static Airport Glasgow = new Airport("GLA", "Glasgow T2"); 
        public static Airport Grenoble = new Airport("GNB", "Grenoble"); 
        public static Airport BarcelonaGirona = new Airport("GRO", "Girona (Barcelona)"); 
        public static Airport Hamburg = new Airport("HAM", "Hamburg"); 
        public static Airport Ibiza = new Airport("IBZ", "Ibiza"); 
        public static Airport Krakow = new Airport("KRK", "Krakow"); 
        public static Airport Katowice = new Airport("KTW", "Katowice"); 
        public static Airport Kaunas = new Airport("KUN", "Kaunas"); 
        public static Airport LeedsBradford = new Airport("LBA", "Leeds Bradford"); 
        public static Airport Lodz = new Airport("LCJ", "Lodz"); 
        public static Airport Almeria = new Airport("LEI", "Almeria"); 
        public static Airport LondonGatwick = new Airport("LGW", "London (Gatwick)"); 
        public static Airport Lisbon = new Airport("LIS", "Lisbon T2"); 
        public static Airport GranCanaria = new Airport("LPA", "Gran Canaria"); 
        public static Airport Liverpool = new Airport("LPL", "Liverpool"); 
        public static Airport LaRochelle = new Airport("LRH", "La Rochelle"); 
        public static Airport Lublin = new Airport("LUZ", "Lublin"); 
        public static Airport LyonSatolas = new Airport("LYS", "Lyon Satolas Airport"); 
        public static Airport Manchester = new Airport("MAN", "Manchester T3"); 
        public static Airport Murcia = new Airport("MJV", "Murcia"); 
        public static Airport Naples = new Airport("NAP", "Naples"); 
        public static Airport Nice = new Airport("NCE", "Nice T1"); 
        public static Airport Newcastle = new Airport("NCL", "Newcastle"); 
        public static Airport Nantes = new Airport("NTE", "Nantes"); 
        public static Airport Porto = new Airport("OPO", "Porto"); 
        public static Airport Poitiers = new Airport("PIS", "Poitiers"); 
        public static Airport Mallorca = new Airport("PMI", "Mallorca"); 
        public static Airport Palermo = new Airport("PMO", "Palermo"); 
        public static Airport Poznan = new Airport("POZ", "Poznań"); 
        public static Airport Prague = new Airport("PRG", "Prague"); 
        public static Airport Pisa = new Airport("PSA", "Pisa"); 
        public static Airport Marrakesh = new Airport("RAK", "Marrakesh"); 
        public static Airport Rodez = new Airport("RDZ", "Rodez"); 
        public static Airport BarcelonaReus = new Airport("REU", "Reus (Barcelona)"); 
        public static Airport Riga = new Airport("RIX", "Riga"); 
        public static Airport Rzeszow = new Airport("RZE", "Rzeszów"); 
        public static Airport Santander = new Airport("SDR", "Santander"); 
        public static Airport Sofia = new Airport("SOF", "Sofia T2"); 
        public static Airport LondonStansted = new Airport("STN", "London (Stansted)"); 
        public static Airport Seville = new Airport("SVQ", "Seville"); 
        public static Airport Salzburg = new Airport("SZG", "Salzburg"); 
        public static Airport Szczecin = new Airport("SZZ", "Szczecin"); 
        public static Airport TenerifeSouth = new Airport("TFS", "Tenerife (South)"); 
        public static Airport Tallinn = new Airport("TLL", "Tallinn"); 
        public static Airport Turin = new Airport("TRN", "Turin"); 
        public static Airport Tours = new Airport("TUF", "Tours"); 
        public static Airport VeniceMarcoPolo = new Airport("VCE", "Venice (Marco Polo)"); 
        public static Airport Vigo = new Airport("VGO", "Vigo"); 
        public static Airport Vilnius = new Airport("VNO", "Vilnius"); 
        public static Airport WarsawModlin = new Airport("WMI", "Warsaw (Modlin)"); 
        public static Airport Wroclaw = new Airport("WRO", "Wroclaw"); 
        public static Airport Zadar = new Airport("ZAD", "Zadar");

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
