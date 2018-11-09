using System.Collections.Generic;
using System.Threading.Tasks;
using FlightsApp.Lib.Models;
using FlightsApp.Lib.Utils;

namespace FlightsApp.Lib.Services
{
    public class CurrencyProvider
    {
        private readonly ILogger logger;
        private readonly IApiHttpClient apiHttpClient;

        public CurrencyProvider(ILogger logger, IApiHttpClient apiHttpClient)
        {
            this.logger = logger;
            this.apiHttpClient = apiHttpClient;
        }

        public async Task<CurrencyRates> GetEurRates()
        {
            var httpResult = await apiHttpClient.GetAsync("http://data.fixer.io/api/latest?access_key=34493ea46a5613c620e452d27ed93104&format=1");
            var currencyRates = JsonSerializer.FromJson<CurrencyRates>(httpResult);

            return currencyRates;
        }
    }
}
