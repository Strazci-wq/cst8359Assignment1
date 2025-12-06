using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Controllers.Api
{
    [Route("api/vetdoctors")]
    [ApiController]
    public class VetDoctorsController : ControllerBase
    {
        private readonly VeterinaryClinicDbContext _context;

        public VetDoctorsController(VeterinaryClinicDbContext context)
        {
            _context = context;
        }

        // GET: api/vetdoctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VetDoctor>>> GetVetDoctors()
        {
            return await _context.VetDoctors.ToListAsync();
        }

        // GET: api/vetdoctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VetDoctor>> GetVetDoctor(int id)
        {
            var vetDoctor = await _context.VetDoctors
                .Include(v => v.Pets)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vetDoctor == null)
            {
                return NotFound();
            }

            return vetDoctor;
        }

        // POST: api/vetdoctors
        [HttpPost]
        public async Task<ActionResult<VetDoctor>> PostVetDoctor(VetDoctor vetDoctor)
        {
            _context.VetDoctors.Add(vetDoctor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVetDoctor), new { id = vetDoctor.Id }, vetDoctor);
        }

        // PUT: api/vetdoctors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVetDoctor(int id, VetDoctor vetDoctor)
        {
            if (id != vetDoctor.Id)
                return BadRequest();

            _context.Entry(vetDoctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/vetdoctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVetDoctor(int id)
        {
            var vetDoctor = await _context.VetDoctors.FindAsync(id);
            if (vetDoctor == null)
            {
                return NotFound();
            }

            _context.VetDoctors.Remove(vetDoctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VetDoctorExists(int id)
        {
            return _context.VetDoctors.Any(e => e.Id == id);
        }
    }
}

