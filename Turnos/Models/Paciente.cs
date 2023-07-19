using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class Paciente
    {
        [Key]
        public  int IdPaciente { get; set; }

        [Required(ErrorMessage =("Debe Ingresar Un Nombre"))]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = ("Debe Ingresar Un Apellido"))]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = ("Debe Ingresar Una Direccion"))]
        public string? Direccion { get;set; }

        [Required(ErrorMessage = ("Debe Ingresar Un Telefono"))]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage =("No es un correo valido")), MaxLength(50),Required(ErrorMessage =("Debe Ingresar Un correo")),Unicode(false)]
        public string? Email { get; set;}

        public List<Turno>? Turnos { get; set; } 

    }
}
