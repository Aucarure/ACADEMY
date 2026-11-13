using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AcademiaAPI.Models
{
    public class Matricula
    {
        [Key]
        public int IdMatricula { get; set; }
        public int IdEstudiante { get; set; }
        public int IdCurso { get; set; }
        public DateTime FechaMatricula { get; set; } = DateTime.Now;
        public string Estado { get; set; } = string.Empty;

        [Precision(10, 2)]
        public decimal MontoTotal { get; set; }

        [ForeignKey(nameof(IdEstudiante))]
        [JsonIgnore]
        public Estudiante? Estudiante { get; set; }

        [ForeignKey(nameof(IdCurso))]
        [JsonIgnore]
        public Curso? Curso { get; set; }

        [JsonIgnore]
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
