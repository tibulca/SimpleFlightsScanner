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
            var httpResult = await apiHttpClient.GetAsync("http://api.fixer.io/latest");
            var currencyRates = JsonSerializer.FromJson<CurrencyRates>(httpResult);

            return currencyRates;
        }
    }
}
