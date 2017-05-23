using System.Linq;

namespace FlightsApp
{
    public class Route
    {
        public Airport Airport1 { get; private set; }
        public Airport Airport2 { get; private set; }

        public Route(Airport airport1, Airport airport2)
        {
            Airport1 = airport1;
            Airport2 = airport2;
        }

        public bool ContainsAny(params Airport[] airports)
        {
            return airports.Any(a => a.Equals(Airport1) || a.Equals(Airport2));
        }
    }
}
