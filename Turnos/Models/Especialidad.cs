using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turnos.Models
{
    public class Especialidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Debe ingresar una descripcion"),StringLength(200, ErrorMessage ="El campo solo acepta 200 caracteres como maximo")]
        public string? Descripcion { get; set; } 

        public List<MedicoEspecialidad>? MedicoEspecialidad { get; set; }    
    }
}
