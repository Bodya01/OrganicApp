namespace OrganicApp.Models.Entities
{
    public class MonitoringData
    {
        public Guid Id { get; set; }

        public float RadiationLevel { get; set; }
        public string Status { get; set; } = null!;
        public DateTime Date { get; set; }

        public Guid CityId { get; set; }
        public City City { get; set; } = null!;

        public void DetermineStatus()
        {
            if (RadiationLevel >= 0 && RadiationLevel <= 0.1)
            {
                Status = "Good";
            }
            else if (RadiationLevel > 0.1 && RadiationLevel <= 0.2)
            {
                Status = "Moderate";
            }
            else if (RadiationLevel > 0.2 && RadiationLevel <= 0.3)
            {
                Status = "Slightly Elevated";
            }
            else if (RadiationLevel > 0.3 && RadiationLevel <= 0.5)
            {
                Status = "Elevated";
            }
            else if (RadiationLevel > 0.5 && RadiationLevel <= 2)
            {
                Status = "High";
            }
            else if (RadiationLevel > 2.1)
            {
                Status = "Very High";
            }
        }
    }
}