namespace OrganicApp.Models.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; } = null!;

        public string Name { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        public List<MonitoringData> MonitoringData { get; set; } = null!;
    }
}