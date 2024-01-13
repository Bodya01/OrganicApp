using OrganicApp.Context;
using OrganicApp.Models.Entities;

namespace OrganicApp.Data
{
    public static class DataSeeder
    {
        public static void Seed(OrganicContext context)
        {
            SeedCities(context);
            SeedMonitoringData(context);
        }

        private static void SeedCities(OrganicContext context)
        {
            if (!context.Cities.Any())
            {
                var cities = new List<City>
                {
                    new City { Name = "Kyiv", Latitude = 50.4501, Longtitude = 30.5234 },
                    new City { Name = "Lviv", Latitude = 49.8429, Longtitude = 24.0315 },
                    // Add more cities as needed
                };

                context.Cities.AddRange(cities);
                context.SaveChanges();
            }
        }

        private static void SeedMonitoringData(OrganicContext context)
        {
            if (!context.MonitoringData.Any())
            {
                var cityKyiv = context.Cities.FirstOrDefault(c => c.Name == "Kyiv");
                var cityLviv = context.Cities.FirstOrDefault(c => c.Name == "Lviv");

                if (cityKyiv != null && cityLviv != null)
                {
                    var monitoringData = new List<MonitoringData>
                    {
                        new MonitoringData { RadiationLevel = 0.1f, Status = "Good", Date = new DateTime(2024, 1, 13), CityId = cityKyiv.Id },
                        new MonitoringData { RadiationLevel = 0.2f, Status = "Moderate", Date = new DateTime(2024, 1, 13), CityId = cityLviv.Id },
                        new MonitoringData { RadiationLevel = 0.15f, Status = "Good", Date = new DateTime(2024, 1, 14), CityId = cityKyiv.Id },
                        new MonitoringData { RadiationLevel = 0.25f, Status = "Moderate", Date = new DateTime(2024, 1, 14), CityId = cityLviv.Id },
                        new MonitoringData { RadiationLevel = 0.18f, Status = "Good", Date = new DateTime(2024, 1, 15), CityId = cityKyiv.Id },
                        new MonitoringData { RadiationLevel = 0.3f, Status = "Bad", Date = new DateTime(2024, 1, 15), CityId = cityLviv.Id }
                        // Add more monitoring data as needed
                    };

                    context.MonitoringData.AddRange(monitoringData);
                    context.SaveChanges();
                }
            }
        }

    }
}
