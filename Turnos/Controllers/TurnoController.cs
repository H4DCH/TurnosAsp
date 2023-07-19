using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class TurnoController : Controller
    {

        private readonly TurnosContext _turnosContext;
        private IConfiguration _configuration;

        public TurnoController(TurnosContext context, IConfiguration configuration) 
        {
            _turnosContext = context;   
            _configuration = configuration;
        
        }
        public IActionResult Index()
        {
            ViewData["IdMedico"] = new SelectList((from Medico in _turnosContext.Medicos.ToList() select new 
            {IdMedico = Medico.IdMedico, NombreCompleto = Medico.NombreMedico + " " + Medico.ApellidoMedico}),
            "IdMedico", "NombreCompleto");

            ViewData["IdPaciente"] = new SelectList((from Paciente in _turnosContext.Pacientes.ToList() select new
           { IdPaciente = Paciente.IdPaciente, NombreCompleto = Paciente.Nombre + " " + Paciente.Apellido}),
            "IdPaciente", "NombreCompleto");
            return View();
        }

        public JsonResult ObtenerTurnos(int IdMedico)
        {
            var turnos = _turnosContext.Turnos.Where(t => t.IdMedico == IdMedico)
                .Select(t => new{
                  t.IdTurno,
                  t.IdMedico,
                  t.IdPaciente,
                  t.FechaHoraInicio,
                  t.FechaHoraFin,
                  paciente = t.Pacientes.Nombre + ", " + t.Pacientes.Apellido
                }).ToList();
            return Json(turnos);    
        }

        [HttpPost]
        public JsonResult GrabarTurnos(Turno Turno)
        {   
            var Ok = false; 
           try
            {
                _turnosContext.Turnos.Add(Turno);
                _turnosContext.SaveChanges();   
                Ok = true;  

            }
            catch (Exception E)
            {
                Console.WriteLine("{0} Excepcion Encontrada", E);

            }

            var jsonresult = new { OK = Ok };
            return Json(jsonresult);    
        }

        [HttpPost]
        public JsonResult EliminarTurno(int IdTurno)
        {
            var Ok = false;
            try
            {
               var turnoEliminar = _turnosContext.Turnos.Where(t => t.IdTurno == IdTurno).FirstOrDefault();
                if (turnoEliminar != null) 
                {
                    _turnosContext.Remove(turnoEliminar);
                    _turnosContext.SaveChanges();
                    Ok=true;
                }

            }
            catch (Exception E)
            {
                Console.WriteLine("{0} Excepcion Encontrada", E);

            }

            var jsonresult = new { OK = Ok };
            return Json(jsonresult);

        }
    }
}
