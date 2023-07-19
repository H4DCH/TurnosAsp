using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class PacienteController : Controller
    {
        private readonly TurnosContext _turnosContext;  

        public PacienteController(TurnosContext turnosContext)
        {
            _turnosContext = turnosContext; 
        }

        public async Task<IActionResult> Index()
        {
            return View(await _turnosContext.Pacientes.ToListAsync());

        }

        public async Task<IActionResult> Detalles(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var Paciente = await _turnosContext.Pacientes.FirstOrDefaultAsync(p => p.IdPaciente == Id);

            if (Paciente == null)
            {
                return NotFound();  
            }
            return View(Paciente);  
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("IdPaciente, Nombre, Apellido,Direccion,Telefono,Email")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _turnosContext.Add(paciente);
                await _turnosContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(paciente);  

        }

        public async Task<IActionResult> Editar(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var Paciente = await _turnosContext.Pacientes.FindAsync(Id);

            if (Paciente == null) 
            {
                return NotFound();
            }
            return View(Paciente);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int? Id,[Bind("IdPaciente,Nombre,Apellido,Direccion,Telefono,Email")] Paciente paciente)
        {
            if(Id != paciente.IdPaciente)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _turnosContext.Update(paciente);
                await _turnosContext.SaveChangesAsync();    
                return RedirectToAction(nameof(Index)); 
            }

            return View(paciente);
        }

        public async Task<IActionResult>Eliminar(int? Id)
        {
            if (Id == null) 
            {
                return NotFound();
            
            }
            var paciente =await _turnosContext.Pacientes.FirstAsync(p=>p.IdPaciente==Id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        [HttpPost,ValidateAntiForgeryToken,ActionName("Eliminar")]
        public async Task<IActionResult> EliminarC(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            var Paciente = await _turnosContext.Pacientes.FindAsync(Id);

            if(Paciente == null)
            {
                return NotFound();
            }
            _turnosContext.Remove(Paciente);
            await _turnosContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
           
        }
         
        
    }
}
