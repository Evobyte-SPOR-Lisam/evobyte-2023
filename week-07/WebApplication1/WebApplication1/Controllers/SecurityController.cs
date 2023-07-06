using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Framework.Identity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly WebApplication1Context _context;

        public ProfilesController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: api/Profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfile()
        {
            Framework.Identity.Identity identity = ControllerContext.GetIdentity();


            if (_context.Profile == null)
            {
                return NotFound();
            }
            return await _context.Profile.ToListAsync();
        }

        // GET: api/Profiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfile(Guid id)
        {
            if (_context.Profile == null)
            {
                return NotFound();
            }
            var profile = await _context.Profile.FindAsync(id);

            if (profile == null)
            {
                return NotFound();
            }

            return profile;
        }

        // PUT: api/Profiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(Guid id, Profile profile)
        {
            if (id != profile.ID)
            {
                return BadRequest();
            }

            _context.Entry(profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        {
            if (_context.Profile == null)
            {
                return Problem("Entity set 'WebApplication1Context.Profile'  is null.");
            }
            _context.Profile.Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfile", new { id = profile.ID }, profile);
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            if (_context.Profile == null)
            {
                return NotFound();
            }
            var profile = await _context.Profile.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profile.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(Guid id)
        {
            return (_context.Profile?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
