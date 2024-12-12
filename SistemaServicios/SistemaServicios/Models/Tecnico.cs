using System.ComponentModel.DataAnnotations;

namespace SistemaServicios.Models
{
    public class Tecnico
    {
        public int TecnicoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Especialidad { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefono { get; set; }
    }
}