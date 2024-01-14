using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganicApp.Context;
using OrganicApp.Controllers.Base;
using OrganicApp.Parsers;
using OrganicApp.Services.Interfaces;

namespace OrganicApp.Controllers
{
    [Route("[controller]")]
    public class DataController : OrganicController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMonitoringService _monitoringService;

        public DataController(IHttpClientFactory httpClientFactory, IMonitoringService monitoringService, OrganicContext context) : base(context)
        {
            _httpClientFactory = httpClientFactory;
            _monitoringService = monitoringService;
        }

        [HttpGet("GetRadiationForLastWeek")]
        public async Task<IActionResult> GetRadiationForLastWeek()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                // Adjust the URL based on your needs
                var apiUrl = "https://www.saveecobot.com/en/maps/marker.json?marker_id=3824&marker_type=device&pollutant=gamma&is_wide=1&is_iframe=0&is_widget=0&rand=2024-01-14T09-40";

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var deviceId = EcoBotJsonParser.GetDeviceId(json);

                    var city = await _context.Cities.FirstAsync(x => x.DeviceId == deviceId);

                    var parsedData = EcoBotJsonParser.ParseHistory(json);

                    _monitoringService.CreateFromFetchedData(parsedData, city.Id);

                    return Ok("Data was updated Successfuly!");
                }

                return BadRequest($"Failed to fetch data. Status: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}