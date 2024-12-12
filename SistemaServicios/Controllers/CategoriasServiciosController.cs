using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaServicios.Data;
using SistemaServicios.Models;

namespace SistemaServicios.Controllers
{
    public class CategoriasServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _context.CategoriasServicio.ToListAsync();
            return View(categorias);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("El ID no puede ser nulo.");
            }

            var categoriaServicio = await _context.CategoriasServicio
                .FirstOrDefaultAsync(m => m.CategoriaId == id);
            if (categoriaServicio == null)
            {
                return NotFound("Categoría no encontrada.");
            }

            return View(categoriaServicio);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaId,Nombre,Descripcion")] CategoriaServicio categoriaServicio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(categoriaServicio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear la categoría: " + ex.Message);
                }
            }
            return View(categoriaServicio);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest("El ID no puede ser nulo.");
            }

            var categoriaServicio = await _context.CategoriasServicio.FindAsync(id);
            if (categoriaServicio == null)
            {
                return NotFound("Categoría no encontrada.");
            }
            return View(categoriaServicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaId,Nombre,Descripcion")] CategoriaServicio categoriaServicio)
        {
            if (id != categoriaServicio.CategoriaId)
            {
                return BadRequest("El ID proporcionado no coincide.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaServicio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaServicioExists(categoriaServicio.CategoriaId))
                    {
                        return NotFound("Categoría no encontrada.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(categoriaServicio);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("El ID no puede ser nulo.");
            }

            var categoriaServicio = await _context.CategoriasServicio
                .FirstOrDefaultAsync(m => m.CategoriaId == id);
            if (categoriaServicio == null)
            {
                return NotFound("Categoría no encontrada.");
            }

            return View(categoriaServicio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var categoriaServicio = await _context.CategoriasServicio.FindAsync(id);
                if (categoriaServicio != null)
                {
                    _context.CategoriasServicio.Remove(categoriaServicio);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest("Error al eliminar la categoría: " + ex.Message);
            }
        }

        private bool CategoriaServicioExists(int id)
        {
            return _context.CategoriasServicio.Any(e => e.CategoriaId == id);
        }
    }
}
