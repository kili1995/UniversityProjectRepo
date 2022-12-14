using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Api.Helpers;
using University.DataAccess.Context;
using University.DataAccess.Models.DataModels;

namespace University.Api.Controllers.Curricula
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CurriculaController : ControllerBase
    {
        private readonly UniversityDBContext _context;

        public CurriculaController(UniversityDBContext context)
        {
            _context = context;
        }

        // GET: api/Curricula
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curriculum>>> GetCurricula()
        {
            if (_context.Curricula == null)
            {
                return NotFound();
            }
            return await _context.Curricula.ToListAsync();
        }

        // GET: api/Curricula/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Curriculum>> GetCurriculum(int id)
        {
            if (_context.Curricula == null)
            {
                return NotFound();
            }
            var curriculum = await _context.Curricula.FindAsync(id);

            if (curriculum == null)
            {
                return NotFound();
            }

            return curriculum;
        }

        // PUT: api/Curricula/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.ADMIN_ROLE)]
        public async Task<IActionResult> PutCurriculum(int id, Curriculum curriculum)
        {
            if (id != curriculum.Id)
            {
                return BadRequest();
            }

            _context.Entry(curriculum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurriculumExists(id))
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

        // POST: api/Curricula
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.ADMIN_ROLE)]
        public async Task<ActionResult<Curriculum>> PostCurriculum(Curriculum curriculum)
        {
            if (_context.Curricula == null)
            {
                return Problem("Entity set 'UniversityDBContext.Curricula'  is null.");
            }
            _context.Curricula.Add(curriculum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurriculum", new { id = curriculum.Id }, curriculum);
        }

        // DELETE: api/Curricula/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.ADMIN_ROLE)]
        public async Task<IActionResult> DeleteCurriculum(int id)
        {
            if (_context.Curricula == null)
            {
                return NotFound();
            }
            var curriculum = await _context.Curricula.FindAsync(id);
            if (curriculum == null)
            {
                return NotFound();
            }

            _context.Curricula.Remove(curriculum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurriculumExists(int id)
        {
            return (_context.Curricula?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
