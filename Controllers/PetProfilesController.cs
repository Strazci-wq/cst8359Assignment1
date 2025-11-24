using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Controllers
{
    public class PetProfilesController : Controller
    {
        private readonly VeterinaryClinicDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PetProfilesController(VeterinaryClinicDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: PetProfiles
        public async Task<IActionResult> Index()
        {
            var profiles = await _context.PetProfiles
                .Include(p => p.Pet)
                .ToListAsync();
            return View(profiles);
        }

        // GET: PetProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petProfile = await _context.PetProfiles
                .Include(p => p.Pet)
                .ThenInclude(p => p!.VetDoctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petProfile == null)
            {
                return NotFound();
            }

            return View(petProfile);
        }

        // GET: PetProfiles/Create
        public IActionResult Create(int? petId = null)
        {
            ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Name", petId);
            return View();
        }

        // POST: PetProfiles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PetId,VetNotes")] PetProfile petProfile)
        {
            if (ModelState.IsValid)
            {
                // Check if pet already has a profile
                var existingProfile = await _context.PetProfiles
                    .FirstOrDefaultAsync(p => p.PetId == petProfile.PetId);
                
                if (existingProfile != null)
                {
                    ModelState.AddModelError("PetId", "This pet already has a profile. Please edit the existing profile instead.");
                    ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Name", petProfile.PetId);
                    return View(petProfile);
                }

                _context.Add(petProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Name", petProfile.PetId);
            return View(petProfile);
        }

        // GET: PetProfiles/CreateFromFile
        public IActionResult CreateFromFile(int? petId = null)
        {
            ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Name", petId);
            return View();
        }

        // POST: PetProfiles/CreateFromFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromFile(int PetId, IFormFile? file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "Please select a file to upload.");
                ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Name", PetId);
                return View();
            }

            // Check if file is .txt
            if (!file.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("file", "Please upload a .txt file only.");
                ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Name", PetId);
                return View();
            }

            // Check if pet already has a profile
            var existingProfile = await _context.PetProfiles
                .FirstOrDefaultAsync(p => p.PetId == PetId);
            
            if (existingProfile != null)
            {
                ModelState.AddModelError("PetId", "This pet already has a profile. Please edit the existing profile instead.");
                ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Name", PetId);
                return View();
            }

            // Read file content
            string vetNotes;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                vetNotes = await reader.ReadToEndAsync();
            }

            // Create PetProfile
            var petProfile = new PetProfile
            {
                PetId = PetId,
                VetNotes = vetNotes
            };

            if (ModelState.IsValid)
            {
                _context.Add(petProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Name", PetId);
            return View();
        }

        // GET: PetProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petProfile = await _context.PetProfiles.FindAsync(id);
            if (petProfile == null)
            {
                return NotFound();
            }
            ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Name", petProfile.PetId);
            return View(petProfile);
        }

        // POST: PetProfiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PetId,VetNotes")] PetProfile petProfile)
        {
            if (id != petProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetProfileExists(petProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Name", petProfile.PetId);
            return View(petProfile);
        }

        // GET: PetProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petProfile = await _context.PetProfiles
                .Include(p => p.Pet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petProfile == null)
            {
                return NotFound();
            }

            return View(petProfile);
        }

        // POST: PetProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var petProfile = await _context.PetProfiles.FindAsync(id);
            if (petProfile != null)
            {
                _context.PetProfiles.Remove(petProfile);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PetProfileExists(int id)
        {
            return _context.PetProfiles.Any(e => e.Id == id);
        }
    }
}

