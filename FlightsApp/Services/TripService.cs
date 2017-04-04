using System.Collections.Generic;
using System.Linq;

namespace FlightsApp
{
    public class TripService
    {
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

            var minPrice = trips.Select(t => t.Sum(f => f.Price)).Min();

            trips.GroupBy(t => t.First().DateFrom.Date)
                 .OrderBy(g => g.Key)
				 .ToList()
                 .ForEach(g =>
                 {
                     logger.Info(string.Empty);
                     logger.Info($"{g.Key.ToString("dd.MM.yyyy")}:");
                     g.OrderBy(f => f.First().DateFrom).ToList().ForEach(Log);
                 });

            logger.Info($"min price: {minPrice}");
        }

        private void Log(List<Flight> trip)
        {
			var airports = string.Join(" --> ", trip.Select(f => $"{f.From.Name} --> {f.To.Name}"));
            var airlines = string.Join(", ", trip.Select(f => f.Airline.Name));
            // todo: make sure all prices are in euro
            var price = trip.Sum(f => f.Price);

            logger.Info($"{trip.First().DateFrom}: {airports}, ({airlines}), {price}{trip.First().CurrencyCode}");
		}
    }
}
