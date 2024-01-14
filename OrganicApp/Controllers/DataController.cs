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
        private readonly IMonitoringService _monitoringService;
        private readonly IDataService _dataService;

        public DataController(IHttpClientFactory httpClientFactory, IMonitoringService monitoringService, IDataService dataService, OrganicContext context) : base(context)
        {
            _monitoringService = monitoringService;
            _dataService = dataService;
        }

        [HttpGet("GetRadiationForLastWeek/deviceId?")]
        public async Task<IActionResult> GetRadiationForLastWeek(string deviceId)
        {
            try
            {
                var fetchedData = await _dataService.GetEcoBotRadiationDataAsync(deviceId);
                var city = await _context.Cities.FirstAsync(x => x.DeviceId == deviceId);
                await _monitoringService.CreateFromFetchedData(fetchedData, city.Id);

                return Ok("Monitoring data updated successfuly");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}