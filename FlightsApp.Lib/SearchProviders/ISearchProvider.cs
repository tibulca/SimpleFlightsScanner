using System.Collections.Generic;
using System.Threading.Tasks;
using FlightsApp.Lib.Models;

namespace FlightsApp.Lib.SearchProviders
{
    public interface ISearchProvider
    {
        Airline Airline { get; }
        bool CanHandleRoute(Route route);
        Task<List<Flight>> Search(SearchCriteria searchCriteria);
    }
}
