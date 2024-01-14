using OrganicApp.Context;
using OrganicApp.Models.Entities;
using OrganicApp.Services.Interfaces;

namespace OrganicApp.Services.Implementations
{
    public class MonitoringService : IMonitoringService
    {
        private readonly OrganicContext _context;

        public MonitoringService(OrganicContext context)
        {
            _context = context;
        }

        public async Task CreateFromFetchedData(Dictionary<DateTime, float> jsonData, Guid cityId)
        {
            foreach (var entry in jsonData)
            {
                var entryDateTime = entry.Key;

                if (!_context.MonitoringData.Any(x => x.CityId == cityId && x.Date == entryDateTime))
                {
                    var newRecord = new MonitoringData
                    {
                        Id = Guid.NewGuid(),
                        RadiationLevel = entry.Value / 1000,
                        Date = entryDateTime,
                        CityId = cityId,
                    };
                    newRecord.DetermineStatus();

                    _context.MonitoringData.Add(newRecord);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}