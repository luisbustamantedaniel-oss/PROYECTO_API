using GeoRiskAI.DTOs;
using GeoRiskAI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GeoRiskAI.Controllers
{
    [ApiController]
    [Route("api/risk")]
    public class RiskController : ControllerBase
    {
        private readonly CountryService _countryService;
        private readonly AIService _aiService;

        public RiskController(CountryService countryService, AIService aiService)
        {
            _countryService = countryService;
            _aiService = aiService;
        }

        [HttpGet("{country}")]
        public async Task<ActionResult<RiskResponseDto>> GetRisk(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                return BadRequest("Debe ingresar un país válido");

            try
            {
                // Obtener país
                JsonElement? countryData = await _countryService.GetCountryInfo(country);

                if (!countryData.HasValue)
                    return NotFound("País no encontrado");

                
                JsonElement countryElement = countryData.Value;

                string countryName = countryElement
                    .GetProperty("name")
                    .GetProperty("common")
                    .GetString() ?? "Desconocido";

                string region = countryElement
                    .GetProperty("region")
                    .GetString() ?? "Desconocida";

                // Llamar IA
                string aiSummary = await _aiService.GenerateSummary(countryName);

                var result = new RiskResponseDto
                {
                    Country = countryName,
                    Region = region,
                    Summary = aiSummary
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}