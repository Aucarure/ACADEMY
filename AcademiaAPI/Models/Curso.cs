using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AcademiaAPI.Models
{
    public class Curso
    {
        [Key]
        public int IdCurso { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        [Precision(10, 2)]
        public decimal Precio { get; set; }
        public int DuracionHoras { get; set; }

        public int IdInstructor { get; set; }
        public bool Activo { get; set; } = true;

        [ForeignKey(nameof(IdInstructor))]
        [JsonIgnore]
        public Instructor? Instructor { get; set; }

        [JsonIgnore]
        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
