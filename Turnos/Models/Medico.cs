using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class Medico
    {
        [Key]
        public int IdMedico { get; set; }

        [Required(ErrorMessage =("Debe Ingresar Un Nombre")),MaxLength(50),Unicode(false),Display(Name ="Nombre")]
        public string? NombreMedico { get; set; }

        [Required(ErrorMessage = ("Debe Ingresar Un Apellido")), MaxLength(50), Unicode(false), Display(Name ="Apellido")]
        public string? ApellidoMedico { get; set; }

        [Required(ErrorMessage = ("Debe Ingresar Una Direccion")), MaxLength(250), Unicode(false), Display (Name ="Direccion")]
        public string? DireccionMedico { get; set; }

        [Required(ErrorMessage = ("Debe Ingresar Una Direccion")), MaxLength(50), Unicode(false)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = ("Debe Ingresar Un Horario")), Unicode(false), Display(Name ="Hora de Atencion Inicial")]
        public DateTime HorarioAtencioDesde { get; set; }

        [Required(ErrorMessage = ("Debe Ingresar Un Horario")), Unicode(false), Display(Name ="Hora de Atencion Final")]
        public DateTime HorarioAtencioHasta { get; set; }
        public List<MedicoEspecialidad>? MedicoEspecialidad { get; set; }
        public List<Turno>? Turnos { get; set; }
    }
}
