using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaServicios.Data;
using SistemaServicios.Models;

namespace SistemaServicios.Controllers
{
    public class HorariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HorariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Horarios
        public async Task<IActionResult> Index()
        {
            var horarios = await _context.Horarios
                .Include(h => h.Tecnico)  // Incluir la relación con Técnico
                .ToListAsync();
            return View(horarios);
        }

        // GET: Horarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario = await _context.Horarios
                .Include(h => h.Tecnico)  // Incluir la relación con Técnico
                .FirstOrDefaultAsync(m => m.HorarioId == id);
            if (horario == null)
            {
                return NotFound();
            }

            return View(horario);
        }

        // GET: Horarios/Create
        public IActionResult Create()
        {
            ViewData["TecnicoId"] = new SelectList(_context.Tecnicos, "TecnicoId", "Nombre");
            return View();
        }

        // POST: Horarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioId,TecnicoId,Dia,HoraInicio,HoraFin")] Horario horario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TecnicoId"] = new SelectList(_context.Tecnicos, "TecnicoId", "Nombre", horario.TecnicoId);
            return View(horario);
        }

        // GET: Horarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }
            ViewData["TecnicoId"] = new SelectList(_context.Tecnicos, "TecnicoId", "Nombre", horario.TecnicoId);
            return View(horario);
        }

        // POST: Horarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioId,TecnicoId,Dia,HoraInicio,HoraFin")] Horario horario)
        {
            if (id != horario.HorarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioExists(horario.HorarioId))
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
            ViewData["TecnicoId"] = new SelectList(_context.Tecnicos, "TecnicoId", "Nombre", horario.TecnicoId);
            return View(horario);
        }

        // GET: Horarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario = await _context.Horarios
                .Include(h => h.Tecnico)  // Incluir la relación con Técnico
                .FirstOrDefaultAsync(m => m.HorarioId == id);
            if (horario == null)
            {
                return NotFound();
            }

            return View(horario);
        }

        // POST: Horarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioExists(int id)
        {
            return _context.Horarios.Any(e => e.HorarioId == id);
        }
    }
}
