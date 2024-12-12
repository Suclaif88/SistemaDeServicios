using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaServicios.Data;
using SistemaServicios.Models;

namespace SistemaServicios.Controllers
{
    public class TecnicosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TecnicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Tecnicos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnico = await _context.Tecnicos
                .FirstOrDefaultAsync(m => m.TecnicoId == id);
            if (tecnico == null)
            {
                return NotFound();
            }

            return View(tecnico);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TecnicoId,Nombre,Especialidad,Telefono")] Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tecnico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tecnico);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnico = await _context.Tecnicos.FindAsync(id);
            if (tecnico == null)
            {
                return NotFound();
            }

            return View(tecnico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TecnicoId,Nombre,Especialidad,Telefono")] Tecnico tecnico)
        {
            if (id != tecnico.TecnicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tecnico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TecnicoExists(tecnico.TecnicoId))
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
            return View(tecnico);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnico = await _context.Tecnicos
                .FirstOrDefaultAsync(m => m.TecnicoId == id);
            if (tecnico == null)
            {
                return NotFound();
            }

            return View(tecnico);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tecnico = await _context.Tecnicos.FindAsync(id);
            if (tecnico == null)
            {
                return NotFound();
            }

            _context.Tecnicos.Remove(tecnico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TecnicoExists(int id)
        {
            return _context.Tecnicos.Any(e => e.TecnicoId == id);
        }
    }
}
