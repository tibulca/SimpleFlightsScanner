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

            var directFlights = GetDirectFlights(fromAirport, toAirport, flights);
            trips.AddRange(directFlights.Select(f => new List<Flight> { f }));

            var flightsWithStopover = GetFlightsWithStopover(fromAirport, toAirport, flights);
            trips.AddRange(flightsWithStopover);

            //var fromFlights = flights.Where(f => f.From == fromAirport && f.To != toAirport);
            //var toFlights = flights.Where(f => f.From != fromAirport && f.To == toAirport);
            //foreach (var fromFlight in fromFlights)
            //{
            //    var matchingToFlights1 = toFlights.Where(t => t.From == fromFlight.To).ToList();
            //    var matchingToFlights2 = toFlights.Where(t => t.From == fromFlight.To && t.DateFrom.Date == fromFlight.DateTo.Date).ToList();
            //    var matchingToFlights = toFlights.Where(t => t.From == fromFlight.To &&
            //                                            t.DateFrom.Date == fromFlight.DateTo.Date &&
            //                                            t.DateFrom > fromFlight.DateTo.AddMinutes(MinimumStopoverInMinutes));
            //    if (matchingToFlights.Any())
            //    {
            //        trips.AddRange(matchingToFlights.Select(t => new List<Flight> { fromFlight, t }));
            //    }
            //}

            if (!trips.Any())
            {
                logger.Info($"\tNO SEARCH RESULTS!");
                return;
            }

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

        private IEnumerable<Flight> GetDirectFlights(Airport fromAirport, Airport toAirport, List<Flight> flights)
        {
            return flights.Where(f => f.From == fromAirport && f.To == toAirport);
        }

        private List<List<Flight>> GetFlightsWithStopover(Airport fromAirport, Airport toAirport, List<Flight> flights)
        {
            var matches = new List<List<Flight>>();
            var fromFlights = flights.Where(f => f.From == fromAirport && f.To != toAirport);
            var toFlights = flights.Where(f => f.From != fromAirport && f.To == toAirport);
            foreach (var fromFlight in fromFlights)
            {
                var matchingToFlights1 = toFlights.Where(t => t.From == fromFlight.To).ToList();
                var matchingToFlights2 = toFlights.Where(t => t.From == fromFlight.To && t.DateFrom.Date == fromFlight.DateTo.Date).ToList();
                var matchingToFlights = toFlights.Where(t => t.From == fromFlight.To &&
                                                        t.DateFrom.Date == fromFlight.DateTo.Date &&
                                                        t.DateFrom > fromFlight.DateTo.AddMinutes(MinimumStopoverInMinutes));
                matchingToFlights.ToList().ForEach(t =>
                {
                    matches.Add(new List<Flight> { fromFlight, t });
                });
            }

            return matches;
        }

        private void Log(List<Flight> trip)
        {
            var flights = string.Join(" --> ", trip.Select(f => $"{f.DateFrom.ToString("HH:mm")} {f.From.Name} - {f.To.Name} {f.DateTo.ToString("HH:mm")}"));
            var airlines = string.Join(", ", trip.Select(f => f.Airline.Name));

            var price = trip.Sum(f => PriceInEur(f));

            logger.Info($"\t{flights}, ({airlines}), {(int)price} EUR");
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
