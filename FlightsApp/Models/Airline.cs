using System.Collections.Generic;

namespace FlightsApp
{
	public class Airline
    {
        public string Name { get; private set; }

		private Airline(string name)
        {
            this.Name = name;

            All.Add(this);
        }

		public static List<Airline> All = new List<Airline>();

		public static Airline Wizzair = new Airline("Wizzair");
		public static Airline Ryanair = new Airline("Ryanair");
		public static Airline Blueair = new Airline("Blueair");
		public static Airline Tarom = new Airline("Tarom");

        public override string ToString()
        {
            return Name;
        }
    }
}
