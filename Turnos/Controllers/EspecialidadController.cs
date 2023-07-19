using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.FileProviders;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class EspecialidadController : Controller
    {

        private readonly TurnosContext _turnosContext;
        public EspecialidadController(TurnosContext context) 
        { 
           _turnosContext = context;
        
        } 
        public async Task<IActionResult> Index()
        {
            return View(await _turnosContext.Especialidades.ToListAsync()); // Como un select * from
        }

        public async Task<IActionResult> Editar(int? Id)
        {
            if (Id == null ) 
            {
                return NotFound();
                
            }
            var Especialidad =await _turnosContext.Especialidades.FindAsync(Id); // Es como un select where
            if (Especialidad== null) 
            {
                return NotFound();
            }

            return View(Especialidad);

        }
        [HttpPost]
        public async Task<IActionResult> Editar(int? Id,[Bind("Id","Descripcion")] Especialidad especialidad)
        {   if (Id != especialidad.Id ) 
            {
                return NotFound();
            
            }
            if (ModelState.IsValid)
            {
                _turnosContext.Update(especialidad);
               await _turnosContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 
            return View(especialidad);
        }

        public async Task<IActionResult> Eliminar(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var especialidad =await _turnosContext.Especialidades.FirstAsync( x => x.Id == Id);  

            if (especialidad == null)
            {
                return NotFound();

            }
            return View(especialidad);
        }
        [HttpPost]
        public async Task<IActionResult> Eliminar(int Id)
        {
            var especialidad = await _turnosContext.Especialidades.FindAsync(Id);
            _turnosContext.Especialidades.Remove(especialidad); // como un delete from where
             await _turnosContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Crear()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Crear([Bind("Id, Descripcion")] Especialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                _turnosContext.Add(especialidad);
                await _turnosContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
      
    }
}
