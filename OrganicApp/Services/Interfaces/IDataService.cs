namespace OrganicApp.Services.Interfaces
{
    public interface IDataService
    {
        Task<Dictionary<DateTime, float>> GetEcoBotRadiationDataAsync(string deviceId);
    }
}
