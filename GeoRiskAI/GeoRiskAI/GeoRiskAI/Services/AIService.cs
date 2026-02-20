using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GeoRiskAI.Services
{
    public class AIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GenerateSummary(string country)
        {
            var apiKey = _configuration["OpenAI:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("API Key no configurada en appsettings.json");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = $"Haz un resumen corto del país {country} y menciona 3 eventos importantes que se realizan allí."
                    }
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(
                "https://api.openai.com/v1/chat/completions",
                content);

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error OpenAI: {json}");

            using var doc = JsonDocument.Parse(json);

            return doc.RootElement
                      .GetProperty("choices")[0]
                      .GetProperty("message")
                      .GetProperty("content")
                      .GetString();
        }
    }
}