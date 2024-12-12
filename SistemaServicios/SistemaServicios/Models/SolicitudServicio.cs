using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaServicios.Models
{
    public class SolicitudServicio
    {
        [Key]  // Define explícitamente la clave primaria
        public int SolicitudId { get; set; }

        public int? ClienteId { get; set; }  // Permite valor nullable
        [ValidateNever]
        public Cliente Cliente { get; set; }

        public int? TecnicoId { get; set; }  // Permite valor nullable
        [ValidateNever]
        public Tecnico Tecnico { get; set; }

        public int? ServicioId { get; set; }  // Permite valor nullable
        [ValidateNever]
        public Servicio Servicio { get; set; }

        [Required]  // Asegura que esta propiedad no sea null
        public DateTime FechaSolicitud { get; set; }

        [StringLength(20)]
        public string Estado { get; set; } = "Pendiente";  // Valor por defecto

        [StringLength(500)]  // Limitar la longitud de comentarios si es necesario
        public string Comentarios { get; set; }
    }
}