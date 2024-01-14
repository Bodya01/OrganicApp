namespace OrganicApp.Services.Interfaces
{
    public interface IMonitoringService
    {
        public Task CreateFromFetchedData(Dictionary<DateTime, float> jsonData, Guid cityId);
    }
}