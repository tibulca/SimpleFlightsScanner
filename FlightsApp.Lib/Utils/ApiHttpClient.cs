using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlightsApp.Lib.Utils
{
    public class ApiHttpClient : IApiHttpClient
    {
        public async Task<string> GetAsync(string url)
        {
            return await this.SendAsync(url, HttpMethod.Get, null, null, null);
        }

        public async Task<string> SendAsync(string url, HttpMethod httpMethod, string requestBody, string contentType, Dictionary<string, string> headers)
        {
            var baseAddress = new Uri(new Uri(url).GetLeftPart(UriPartial.Authority));
            using (var handler = new HttpClientHandler { UseCookies = false })
            {
                using (var client = new HttpClient(handler))
                {
                    var request = new HttpRequestMessage(httpMethod, url);

                    if (requestBody != null)
                    {
                        request.Content = new StringContent(requestBody, Encoding.UTF8, contentType);
                    }

                    if (headers != null)
                    {
                        headers.ToList().ForEach(h => request.Headers.Add(h.Key, h.Value));
                    }

                    using (var response = await client.SendAsync(request))
                    {
                        using (var responseContent = response.Content)
                        {
                            return await responseContent.ReadAsStringAsync();
                        }
                    }
                }
            }
        }
    }
}
