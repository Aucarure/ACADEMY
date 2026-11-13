using AcademiaAPI.Data;
using AcademiaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademiaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstudiantesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Estudiantes  -> solo estudiantes activos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes()
        {
            return await _context.Estudiantes
                .Where(e => e.Activo)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Estudiantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.IdEstudiante == id && e.Activo);

            if (estudiante == null)
            {
                return NotFound(new { mensaje = $"No existe el estudiante con Id {id}" });
            }

            return estudiante;
        }

        // POST: api/Estudiantes
        [HttpPost]
        public async Task<ActionResult<Estudiante>> PostEstudiante(Estudiante estudiante)
        {
            estudiante.IdEstudiante = 0;            // lo genera la base de datos
            estudiante.Activo = true;               // todo registro nuevo nace activo
            estudiante.FechaRegistro = DateTime.Now; // la fecha la asigna el servidor

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.IdEstudiante }, estudiante);
        }

        // PUT: api/Estudiantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, Estudiante estudiante)
        {
            if (id != estudiante.IdEstudiante)
            {
                return BadRequest(new { mensaje = "El Id de la URL no coincide con el del cuerpo" });
            }

            var existente = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.IdEstudiante == id && e.Activo);

            if (existente == null)
            {
                return NotFound(new { mensaje = $"No existe el estudiante con Id {id}" });
            }

            // Solo se actualizan los datos editables; FechaRegistro y Activo no se tocan
            existente.Nombres = estudiante.Nombres;
            existente.Apellidos = estudiante.Apellidos;
            existente.Email = estudiante.Email;
            existente.Telefono = estudiante.Telefono;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Estudiantes/5  -> eliminado lógico (Activo = false)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.IdEstudiante == id && e.Activo);

            if (estudiante == null)
            {
                return NotFound(new { mensaje = $"No existe el estudiante con Id {id}" });
            }

            estudiante.Activo = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
