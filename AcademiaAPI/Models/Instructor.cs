using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AcademiaAPI.Models
{
    public class Instructor
    {
        [Key]
        public int IdInstructor { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;

        [JsonIgnore]
        public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
    }
}
