using System.ComponentModel.DataAnnotations;

namespace SistemaServicios.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(50)]
        public required string NombreUsuario { get; set; }

        [Required]
        [StringLength(256)]
        public required string Contrasena { get; set; }

        [Required]
        [StringLength(20)]
        public required string Rol { get; set; }
    }
}
