using System.ComponentModel.DataAnnotations;

namespace SistemaServicios.Models
{
    public class CategoriaServicio
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }
}
