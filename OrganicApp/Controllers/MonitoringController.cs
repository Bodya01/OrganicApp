using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganicApp.Context;
using OrganicApp.Controllers.Base;
using OrganicApp.Models.ViewModels;

namespace OrganicApp.Controllers
{
    public class MonitoringController : OrganicController
    {
        public MonitoringController(OrganicContext context) : base(context) { }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(Radiation));

        [HttpGet]
        public async Task<IActionResult> Radiation(Guid? cityId, DateTime date)
        {
            var city = cityId == null || cityId == Guid.Empty
                ? await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Kyiv")
                : await _context.Cities.FindAsync(cityId);

            var monitoringRecords = await _context.MonitoringData
                .Where(x => x.Date.Date == date.Date && x.CityId == cityId)
                .ToListAsync();

            var cities = await _context.Cities.ToListAsync();

            var viewModel = new RadiationViewModel
            {
                SelectedCity = city,
                MonitoringData = monitoringRecords,
                Cities = cities
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetMonitoringData(Guid cityId, DateTime date)
        {
            var records = await _context.MonitoringData
                .Where(x => x.Date.Date == date.Date && x.CityId == cityId)
                .ToListAsync();

            return Ok(records);
        }
    }

}
