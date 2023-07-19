using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class Turno
    {
        [Key]
        public int IdTurno { get; set; }
        [Required]  
        public int IdPaciente { get; set; }
        [Required]
        public int IdMedico { get; set; }
        [Display(Name = "Fecha Hora Ini."),Required]
        public DateTime FechaHoraInicio { get; set; }
        [Display(Name = "Fecha Hora Fin."), Required]
        public DateTime FechaHoraFin{ get; set; }
        public Paciente? Pacientes { get; set; }
        public Medico? Medicos { get; set; }




    }
}
