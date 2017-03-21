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
	}
}
