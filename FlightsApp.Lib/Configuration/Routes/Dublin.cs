using System.Collections.Generic;
using FlightsApp.Lib.Models;

namespace FlightsApp.Lib.Configuration.Routes
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








            Airport.Lanzarote,
			Airport.Amsterdam,
			Airport.Athens,
			Airport.Barcelona,
			Airport.Milan,
			Airport.Birmingham,
			Airport.Biarritz,
			Airport.Bremen,
			Airport.Bristol,
			Airport.Brussels,
			Airport.Bydgoszcz,
			Airport.Carcassonne,
			Airport.CreteChania,
			Airport.Comiso,
			Airport.Copenhagen,
			Airport.Cardiff,
			Airport.Edinburgh,
			Airport.EastMidlands,
			Airport.Faro,
			Airport.Fuerteventura,
			Airport.Gdansk,
			Airport.Glasgow,
			Airport.Grenoble,
			Airport.BarcelonaGirona,
			Airport.Hamburg,
			Airport.Ibiza,
			Airport.Krakow,
			Airport.Katowice,
			Airport.Kaunas,
			Airport.LeedsBradford,
			Airport.Lodz,
			Airport.Almeria,
			Airport.LondonGatwick,
			Airport.Lisbon,
			Airport.GranCanaria,
			Airport.Liverpool,
			Airport.LaRochelle,
			Airport.Lublin,
			Airport.LyonSatolas,
			Airport.Manchester,
			Airport.Murcia,
			Airport.Naples,
			Airport.Nice,
			Airport.Newcastle,
			Airport.Nantes,
			Airport.Porto,
			Airport.Poitiers,
			Airport.Mallorca,
			Airport.Palermo,
			Airport.Poznan,
			Airport.Prague,
			Airport.Pisa,
			Airport.Marrakesh,
			Airport.Rodez,
			Airport.BarcelonaReus,
			Airport.Riga,
			Airport.Rzeszow,
			Airport.Santander,
			Airport.Sofia,
			Airport.LondonStansted,
			Airport.Seville,
			Airport.Salzburg,
			Airport.Szczecin,
			Airport.TenerifeSouth,
			Airport.Tallinn,
			Airport.Turin,
			Airport.Tours,
			Airport.VeniceMarcoPolo,
			Airport.Vigo,
			Airport.Vilnius,
			Airport.WarsawModlin,
			Airport.Wroclaw,
			Airport.Zadar
        };
    }
}
