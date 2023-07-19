namespace Turnos.Models
{
    public class MedicoEspecialidad
    {
        public int IdMedico { get;set; }
        public int IdEspecialidad { get;set; }  
        public Medico? Medicos { get;set; }
        public Especialidad? especialidades { get;set; } 
       

    }
}
