using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaServicios.Models
{
    public class Horario
    {
        public int HorarioId { get; set; }

        public int TecnicoId { get; set; }
        public Tecnico Tecnico { get; set; }

        [Required]
        [StringLength(20)]
        public string Dia { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }
    }
}

