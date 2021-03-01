using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace OpenIDConnect.Data
{
    public class WeatherForecastService
    {
        private readonly HttpClient httpClient;
        private readonly TokenProvider tokenProvider;

        public WeatherForecastService(IHttpClientFactory httpClientFactory, TokenProvider tokenProvider)
        {
            httpClient = httpClientFactory.CreateClient();
            this.tokenProvider = tokenProvider;
        }
        public async Task<WeatherForecast[]> GetForecastAsync()
        {

            var token = tokenProvider.AccessToken;
            var request = new HttpRequestMessage(HttpMethod.Get,
                       $"https://localhost:44397/api/WeatherForecast?startDate={DateTime.Now}");
            // request.Headers.Add("Authorization", $"Bearer {token}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<WeatherForecast[]>();
        }

    }
}
