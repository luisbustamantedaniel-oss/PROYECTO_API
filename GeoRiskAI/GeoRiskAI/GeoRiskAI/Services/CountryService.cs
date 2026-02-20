using System.Text.Json;

namespace GeoRiskAI.Services
{
    public class CountryService
    {
        private readonly HttpClient _httpClient;

        public CountryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JsonElement?> GetCountryInfo(string country)
        {
            var response = await _httpClient.GetAsync(
                $"https://restcountries.com/v3.1/name/{country}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonDocument.Parse(json);

            if (data.RootElement.GetArrayLength() == 0)
                return null;

            return data.RootElement[0];
        }
    }
}
