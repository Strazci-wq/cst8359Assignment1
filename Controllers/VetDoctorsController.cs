using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Controllers
{
    public class VetDoctorsController : Controller
    {
        private readonly VeterinaryClinicDbContext _context;

        public VetDoctorsController(VeterinaryClinicDbContext context)
        {
            _context = context;
        }

        // GET: VetDoctors
        public async Task<IActionResult> Index()
        {
            return View(await _context.VetDoctors.ToListAsync());
        }

        // GET: VetDoctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vetDoctor = await _context.VetDoctors
                .Include(v => v.Pets)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vetDoctor == null)
            {
                return NotFound();
            }

            return View(vetDoctor);
        }

        // GET: VetDoctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VetDoctors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Specialty")] VetDoctor vetDoctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vetDoctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vetDoctor);
        }

        // GET: VetDoctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vetDoctor = await _context.VetDoctors.FindAsync(id);
            if (vetDoctor == null)
            {
                return NotFound();
            }
            return View(vetDoctor);
        }

        // POST: VetDoctors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Specialty")] VetDoctor vetDoctor)
        {
            if (id != vetDoctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vetDoctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VetDoctorExists(vetDoctor.Id))
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
            return View(vetDoctor);
        }

        // GET: VetDoctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vetDoctor = await _context.VetDoctors
                .Include(v => v.Pets)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vetDoctor == null)
            {
                return NotFound();
            }

            return View(vetDoctor);
        }

        // POST: VetDoctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vetDoctor = await _context.VetDoctors.FindAsync(id);
            if (vetDoctor != null)
            {
                _context.VetDoctors.Remove(vetDoctor);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VetDoctorExists(int id)
        {
            return _context.VetDoctors.Any(e => e.Id == id);
        }
    }
}

