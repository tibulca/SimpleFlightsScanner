using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightsApp.Lib.Models;
using FlightsApp.Lib.Utils;

namespace FlightsApp.Lib.SearchProviders
{
    public abstract class SearchProviderBase
    {
		protected readonly ILogger logger;
		protected readonly IApiHttpClient apiHttpClient;

        public Airline Airline { get; private set; }

        private List<Airport> AirportsHandled 
        { 
            get 
            { 
                return Configuration.Configuration.AirlineAirports[Airline]; 
            } 
        } 

        protected SearchProviderBase(Airline airline, ILogger logger, IApiHttpClient apiHttpClient)
		{
			this.logger = logger;
			this.apiHttpClient = apiHttpClient;

            Airline = airline;
		}

		public bool CanHandleRoute(Route route)
		{
			return AirportsHandled.Contains(route.Airport1) && AirportsHandled.Contains(route.Airport2);
		}

		public async Task<List<Flight>> Search(SearchCriteria searchCriteria)
		{
			try
			{
                return await SearchFlights(searchCriteria);
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error(ex.StackTrace);
			}

			return await Task.Run(() => new List<Flight>());
		}

        protected abstract Task<List<Flight>> SearchFlights(SearchCriteria searchCriteria);

        protected T Deserialize<T>(string json)
        {
			return JsonSerializer.FromJson<T>(json);
        }
	}
}
