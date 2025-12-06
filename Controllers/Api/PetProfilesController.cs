using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Controllers.Api
{
    [Route("api/petprofiles")]
    [ApiController]
    public class PetProfilesController : ControllerBase
    {
        private readonly VeterinaryClinicDbContext _context;

        public PetProfilesController(VeterinaryClinicDbContext context)
        {
            _context = context;
        }

        // GET: api/petprofiles/5
        [HttpGet("{petId}")]
        public async Task<ActionResult<PetProfile>> GetPetProfile(int petId)
        {
            var petProfile = await _context.PetProfiles
                .FirstOrDefaultAsync(p => p.PetId == petId);

            if (petProfile == null)
                return NotFound();

            return petProfile;
        }

        // POST: api/petprofiles
        [HttpPost]
        public async Task<ActionResult<PetProfile>> PostPetProfile(PetProfile petProfile)
        {
            var existing = await _context.PetProfiles.FirstOrDefaultAsync(p => p.PetId == petProfile.PetId);
            if (existing != null)
            {
                existing.VetNotes = petProfile.VetNotes;
                await _context.SaveChangesAsync();
                return Ok(existing);
            }

            _context.PetProfiles.Add(petProfile);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPetProfile), new { petId = petProfile.PetId }, petProfile);
        }

        // PUT: api/petprofiles/5
        [HttpPut("{petId}")]
        public async Task<IActionResult> PutPetProfile(int petId, PetProfile petProfile)
        {
            if (petId != petProfile.PetId)
                return BadRequest();

            var existing = await _context.PetProfiles.FirstOrDefaultAsync(p => p.PetId == petId);
            if (existing == null)
                return NotFound();

            existing.VetNotes = petProfile.VetNotes;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/petprofiles/5
        [HttpDelete("{petId}")]
        public async Task<IActionResult> DeletePetProfile(int petId)
        {
            var petProfile = await _context.PetProfiles.FirstOrDefaultAsync(p => p.PetId == petId);
            if (petProfile == null)
                return NotFound();

            _context.PetProfiles.Remove(petProfile);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

