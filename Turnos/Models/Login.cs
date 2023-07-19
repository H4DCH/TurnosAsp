using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class Login
    {
        [Key]
        public int login { get; set; }

        [Required(ErrorMessage ="Debe Ingresar un Usuario")]
        public string? Usuario { get; set; }

        [Required(ErrorMessage ="Debe Ingresar Una Contrasena")]
        public string? Password { get; set; }
    }
}
