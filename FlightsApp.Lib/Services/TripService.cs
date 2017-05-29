using System.Collections.Generic;
using System.Linq;
using FlightsApp.Lib.Models;
using FlightsApp.Lib.Utils;

namespace FlightsApp.Lib.Services
{
    public class TripService
    {
        private const int MinimumStopoverInMinutes = 60;
		private readonly ILogger logger;

		public TripService(ILogger logger)
        {
            this.logger = logger;
        }

		public List<Trip> FindFightMatches(Airport fromAirport, Airport toAirport, List<Flight> flights)
        {
            var trips = new List<Trip>();

            var directFlights = GetDirectFlights(fromAirport, toAirport, flights);
            trips.AddRange(directFlights.Select(f => new Trip(new List<Flight> { f })));

            var flightsWithStopover = GetFlightsWithStopover(fromAirport, toAirport, flights);
            trips.AddRange(flightsWithStopover);

            return trips;
        }

        private IEnumerable<Flight> GetDirectFlights(Airport fromAirport, Airport toAirport, List<Flight> flights)
        {
            return flights.Where(f => f.From == fromAirport && f.To == toAirport);
        }

        private List<Trip> GetFlightsWithStopover(Airport fromAirport, Airport toAirport, List<Flight> flights)
        {
            var matches = new List<Trip>();
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
                    matches.Add(new Trip(new List<Flight> { fromFlight, t }));
                });
            }

            return matches;
        }
    }
}
