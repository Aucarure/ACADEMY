using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AcademiaAPI.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }
        public int IdMatricula { get; set; }

        [Precision(10, 2)]
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; } = DateTime.Now;
        public string MetodoPago { get; set; } = string.Empty;
        public string EstadoPago { get; set; } = string.Empty;

        [ForeignKey(nameof(IdMatricula))]
        [JsonIgnore]
        public Matricula? Matricula { get; set; }
    }
}
