using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaServicios.Models
{
    public class Servicio
    {
        [Key]
        public int ServicioId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
        public int? CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        [ValidateNever]
        public CategoriaServicio Categoria { get; set; }
    }
}