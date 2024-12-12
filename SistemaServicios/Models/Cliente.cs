using System.ComponentModel.DataAnnotations;

namespace SistemaServicios.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(20)]
        public string Documento { get; set; }
    }
}

