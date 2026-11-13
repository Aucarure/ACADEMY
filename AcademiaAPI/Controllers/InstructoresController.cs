using AcademiaAPI.Data;
using AcademiaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademiaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InstructoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Instructores  -> solo instructores activos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instructor>>> GetInstructores()
        {
            return await _context.Instructores
                .Where(i => i.Activo)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Instructores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instructor>> GetInstructor(int id)
        {
            var instructor = await _context.Instructores
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.IdInstructor == id && i.Activo);

            if (instructor == null)
            {
                return NotFound(new { mensaje = $"No existe el instructor con Id {id}" });
            }

            return instructor;
        }

        // POST: api/Instructores
        [HttpPost]
        public async Task<ActionResult<Instructor>> PostInstructor(Instructor instructor)
        {
            instructor.IdInstructor = 0; // lo genera la base de datos
            instructor.Activo = true;    // todo registro nuevo nace activo

            _context.Instructores.Add(instructor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInstructor), new { id = instructor.IdInstructor }, instructor);
        }

        // PUT: api/Instructores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstructor(int id, Instructor instructor)
        {
            if (id != instructor.IdInstructor)
            {
                return BadRequest(new { mensaje = "El Id de la URL no coincide con el del cuerpo" });
            }

            var existente = await _context.Instructores
                .FirstOrDefaultAsync(i => i.IdInstructor == id && i.Activo);

            if (existente == null)
            {
                return NotFound(new { mensaje = $"No existe el instructor con Id {id}" });
            }

            // Solo se actualizan los datos editables; el estado Activo se maneja con DELETE
            existente.Nombres = instructor.Nombres;
            existente.Apellidos = instructor.Apellidos;
            existente.Especialidad = instructor.Especialidad;
            existente.Email = instructor.Email;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Instructores/5  -> eliminado lógico (Activo = false)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var instructor = await _context.Instructores
                .FirstOrDefaultAsync(i => i.IdInstructor == id && i.Activo);

            if (instructor == null)
            {
                return NotFound(new { mensaje = $"No existe el instructor con Id {id}" });
            }

            instructor.Activo = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
