using Microsoft.EntityFrameworkCore;
using OrganicApp.Context;
using OrganicApp.Parsers;
using OrganicApp.Services.Interfaces;

namespace OrganicApp.Services.Implementations
{
    public class DataService : IDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly OrganicContext _context;

        public DataService(IHttpClientFactory httpClientFactory, OrganicContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        public async Task<Dictionary<DateTime, float>> GetEcoBotRadiationDataAsync(string deviceId)
        {
            try
            {
                var actualId = deviceId.Replace("device_", "");
                
                var client = _httpClientFactory.CreateClient();

                // Adjust the URL based on your needs
                var apiUrl = $"https://www.saveecobot.com/en/maps/marker.json?marker_id={actualId}&marker_type=device&pollutant=gamma&is_wide=1&is_iframe=0&is_widget=0";

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var city = await _context.Cities.FirstAsync(x => x.DeviceId == deviceId);

                    var parsedData = EcoBotJsonParser.ParseHistory(json);

                    return parsedData;
                }

                throw new Exception("Failed to fetch data from EcoBot");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}