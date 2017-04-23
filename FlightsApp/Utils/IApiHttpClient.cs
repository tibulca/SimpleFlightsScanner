using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlightsApp
{
    public interface IApiHttpClient
    {
        Task<string> SendAsync(string url, HttpMethod httpMethod, string requestBody, string contentType, Dictionary<string, string> headers);
        Task<string> GetAsync(string url);
    }
}
