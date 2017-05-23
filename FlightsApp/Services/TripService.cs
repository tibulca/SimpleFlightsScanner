using System.Collections.Generic;
using System.Linq;

namespace FlightsApp
{
    public class TripService
    {
        private const int MinimumStopoverInMinutes = 60;
		private readonly ILogger logger;

		public TripService(ILogger logger)
        {
            this.logger = logger;
        }

		public void FindFightMatches(Airport fromAirport, Airport toAirport, List<Flight> flights)
        {
			logger.Info($"TRIP from {fromAirport.Name} to {toAirport.Name}");

            var trips = new List<List<Flight>>();

            var directFlights = flights.Where(f => f.From == fromAirport && f.To == toAirport);
            trips.AddRange(directFlights.Select(f => new List<Flight> { f }));

            var fromFlights = flights.Where(f => f.From == fromAirport && f.To != toAirport);
            var toFlights = flights.Where(f => f.From != fromAirport && f.To == toAirport);
            foreach (var fromFlight in fromFlights)
            {
                var matchingToFlights1 = toFlights.Where(t => t.From == fromFlight.To).ToList();
                var matchingToFlights2 = toFlights.Where(t => t.From == fromFlight.To && t.DateFrom.Date == fromFlight.DateTo.Date).ToList();
                var matchingToFlights = toFlights.Where(t => t.From == fromFlight.To &&
                                                        t.DateFrom.Date == fromFlight.DateTo.Date &&
                                                        t.DateFrom > fromFlight.DateTo.AddMinutes(MinimumStopoverInMinutes));
                if (matchingToFlights.Any())
                {
                    trips.AddRange(matchingToFlights.Select(t => new List<Flight> { fromFlight, t }));
                }
            }

            //var flightsWithStopover = fromFlights.Select(f => toFlights.FirstOrDefault(t => t.) f.From == fromAirport && f.To == toAirport);

            var minPrice = trips.Select(t => t.Sum(f => PriceInEur(f)))
                                .Min();
            logger.Info($"\tmin price: {(int)minPrice}");

            trips.GroupBy(t => t.First().DateFrom.Date)
                 .OrderBy(g => g.Key)
				 .ToList()
                 .ForEach(g =>
                 {
                     logger.Info(string.Empty);
                     logger.Info($"{g.Key.ToString("dd.MM.yyyy")}:");
                     g.OrderBy(f => f.First().DateFrom).ToList().ForEach(Log);
                 });

        }

        private void Log(List<Flight> trip)
        {
			var airports = string.Join(" --> ", trip.Select(f => $"{f.From.Name} --> {f.To.Name}"));
            var airlines = string.Join(", ", trip.Select(f => f.Airline.Name));

            var price = trip.Sum(f => PriceInEur(f));

            logger.Info($"\t{trip.First().DateFrom.ToString("HH:mm")} - {airports}, ({airlines}), {(int)price} EUR");
		}

        private double PriceInEur(Flight f)
        {
            // todo: implement better converter
            switch (f.CurrencyCode)
            {
                case "RON":
                    return f.Price / 4.5709;
                case "GBP":
                    return f.Price / 0.85782;
                default:
                    return f.Price;
            }
        }
    }
}
