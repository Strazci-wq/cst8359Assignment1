using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VeterinaryClinic.Data;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VeterinaryClinicDbContext _context;

        public HomeController(ILogger<HomeController> logger, VeterinaryClinicDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stats = new
            {
                VetDoctorsCount = await _context.VetDoctors.CountAsync(),
                PetsCount = await _context.Pets.CountAsync(),
                PetProfilesCount = await _context.PetProfiles.CountAsync()
            };
            ViewBag.Stats = stats;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

