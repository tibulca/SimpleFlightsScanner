using System.Linq;

namespace FlightsApp.Lib.Models
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

        public bool ContainsAll(params Airport[] airports)
        {
            return airports.All(a => a.Equals(Airport1) || a.Equals(Airport2));
        }

        public override bool Equals(object obj)
        {
            var route = obj as Route;
            return route != null && (
                (route.Airport1.Equals(this.Airport1) && route.Airport2.Equals(this.Airport2)) ||
                (route.Airport1.Equals(this.Airport2) && route.Airport2.Equals(this.Airport1))
            );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[{0} - {1}]", Airport1, Airport2);
        }
    }
}
