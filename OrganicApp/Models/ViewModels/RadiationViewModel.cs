using OrganicApp.Models.Entities;

namespace OrganicApp.Models.ViewModels
{
    public class RadiationViewModel
    {
        public City SelectedCity { get; set; }
        public List<MonitoringData> MonitoringData { get; set; }
        public List<City> Cities { get; set; }
    }
}