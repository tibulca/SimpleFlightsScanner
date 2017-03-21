using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightsApp
{
	public interface ISearchProvider
	{
		string Name { get; }
		bool CanHandleRoute(Route route);
		Task<List<Flight>> Search(SearchCriteria searchCriteria);
	}
}
