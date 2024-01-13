using Microsoft.EntityFrameworkCore;
using OrganicApp.Models.Entities;

namespace OrganicApp.Context
{
    public class OrganicContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<MonitoringData> MonitoringData { get; set; }
        public OrganicContext(DbContextOptions<OrganicContext> options) : base(options) { }
    }
}