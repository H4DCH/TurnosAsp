using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class MedicoController : Controller
    {
        private readonly TurnosContext _context;

        public MedicoController(TurnosContext context)
        {
            _context = context;
        }

        // GET: Medico
        public async Task<IActionResult> Index()
        {
              return _context.Medicos != null ? 
                          View(await _context.Medicos.ToListAsync()) :
                          Problem("Entity set 'TurnosContext.Medicos'  is null.");
        }

        // GET: Medico/Details/5
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .Where(m => m.IdMedico == id).Include(me => me.MedicoEspecialidad)
                .ThenInclude(e => e.especialidades).FirstOrDefaultAsync();
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medico/Create
        public IActionResult Crear()
        {
            ViewData["ListaEspecialidades"] = new SelectList(_context.Especialidades, "Id", "Descripcion");
            return View();

        }

        // POST: Medico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("IdMedico,NombreMedico,ApellidoMedico,DireccionMedico" +
            ",Email,HorarioAtencioDesde,HorarioAtencioHasta")] Medico medico, int Id)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medico);
                await _context.SaveChangesAsync();

                var medicoespecialidad = new MedicoEspecialidad();

                medicoespecialidad.IdMedico = medico.IdMedico;
                medicoespecialidad.IdEspecialidad = Id;

                _context.Add(medicoespecialidad);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medico/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.Where(m => m.IdMedico == id).
                Include(me => me.MedicoEspecialidad).FirstOrDefaultAsync();
            if (medico == null)
            {
                return NotFound();
            }
            ViewData["listaEspecialidades"] = new SelectList(_context.Especialidades, "Id", "Descripcion", 
                medico.MedicoEspecialidad[0].IdEspecialidad);
            return View(medico);
        }

        // POST: Medico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Editar(int id, [Bind("IdMedico,NombreMedico,ApellidoMedico,DireccionMedico,Email,HorarioAtencioDesde," +
            "HorarioAtencioHasta")] Medico medico, int IdEspecialidad)
        {
            if (id != medico.IdMedico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();

                    var medicoEspecialidad = await _context.MedicoEspecialidad
                        .FirstOrDefaultAsync(me => me.IdMedico == id);

                     _context.Remove(medicoEspecialidad);
                    await _context.SaveChangesAsync();

                    medicoEspecialidad.IdEspecialidad = IdEspecialidad;
                    
                     _context.Add(medicoEspecialidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.IdMedico))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medico/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.IdMedico == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medico/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirmacioneliminacion(int id)
        {
            var medicoEspecialidad = await _context.MedicoEspecialidad
                .FirstOrDefaultAsync(me => me.IdMedico == id);

            _context.MedicoEspecialidad.Remove(medicoEspecialidad);
            await _context.SaveChangesAsync();

            if (_context.Medicos == null)
            {
                return Problem("Entity set 'TurnosContext.Medicos'  is null.");
            }
            var medico = await _context.Medicos.FindAsync(id);
            if (medico != null)
            {
                _context.Medicos.Remove(medico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
          return (_context.Medicos?.Any(e => e.IdMedico == id)).GetValueOrDefault();
        }

        public string TraerHorarioDesde(int IdMedico)
        {
            var HorarioDesde = _context.Medicos.Where(m => m.IdMedico == IdMedico).FirstOrDefault().HorarioAtencioDesde;

            return HorarioDesde.Hour + ":" + HorarioDesde.Minute;
        }
        public string TraerHorarioHasta(int IdMedico)
        {
            var HorarioHasta = _context.Medicos.Where(m => m.IdMedico == IdMedico).FirstOrDefault().HorarioAtencioHasta;

            return HorarioHasta.Hour + ":" + HorarioHasta.Minute;
        }
    }
}
