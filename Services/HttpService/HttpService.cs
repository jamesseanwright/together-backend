using System.Net.Http;
using System.Threading.Tasks;

namespace Together.Services.HttpService
{
    public class HttpService
    {
        private HttpClient _httpClient;
        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PostAsync(string url, string body)
        {
            HttpResponseMessage response = await _httpClient.PostAsync(
                url,
                new StringContent(body)
            );

            return await response.Content.ReadAsStringAsync();
        }
    }
}