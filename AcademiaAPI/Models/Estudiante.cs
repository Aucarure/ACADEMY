using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AcademiaAPI.Models
{
    public class Estudiante
    {
        [Key]
        public int IdEstudiante { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;

        [JsonIgnore]
        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
