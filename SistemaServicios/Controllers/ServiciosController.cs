using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaServicios.Data;
using SistemaServicios.Models;

namespace SistemaServicios.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servicios
        public async Task<IActionResult> Index()
        {
            var servicios = await _context.Servicios.Include(s => s.Categoria).ToListAsync();
            return View(servicios);
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("El ID no puede ser nulo.");
            }

            var servicio = await _context.Servicios
                .Include(s => s.Categoria)
                .FirstOrDefaultAsync(m => m.ServicioId == id);

            if (servicio == null)
            {
                return NotFound("Servicio no encontrado.");
            }

            return View(servicio);
        }

        // GET: Servicios/Create
        public IActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(_context.CategoriasServicio, "CategoriaId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicioId,Nombre,Descripcion,Precio,CategoriaId")] Servicio servicio)
        {
            try
            {
                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"No se pudo guardar el servicio. Error: {ex.Message}");
            }

            ViewBag.CategoriaId = new SelectList(_context.CategoriasServicio, "CategoriaId", "Nombre", servicio.CategoriaId);
            return View(servicio);
        }







        // Acción GET para editar
        public async Task<IActionResult> Edit(int? id)
        {
            // Verifica si el ID es nulo
            if (id == null)
            {
                return BadRequest("El ID no puede ser nulo.");
            }

            // Busca el servicio por ID
            var servicio = await _context.Servicios.FindAsync(id);

            // Si el servicio no se encuentra, devuelve NotFound
            if (servicio == null)
            {
                return NotFound("Servicio no encontrado.");
            }

            // Cargar categorías para el dropdown
            var categorias = await _context.CategoriasServicio.ToListAsync();

            // Crear el SelectList con las categorías disponibles y asignar el valor seleccionado
            ViewBag.CategoriaId = new SelectList(categorias, "CategoriaId", "Nombre", servicio.CategoriaId);

            return View(servicio);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicioId,Nombre,Descripcion,Precio,CategoriaId")] Servicio servicio)
        {
            // Depuración: revisar el estado del modelo
            Console.WriteLine("Errores del modelo:");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Error: {error.ErrorMessage}");
            }

            // Verifica si el ID proporcionado coincide con el del servicio
            if (id != servicio.ServicioId)
            {
                return BadRequest("El ID proporcionado no coincide.");
            }

            // Eliminar la validación para CategoriaId (por ser nullable)
            ModelState.Remove("CategoriaId");

            // Verifica que el modelo sea válido
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Modelo no válido después de eliminar la validación de CategoriaId:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }

                var categorias = await _context.CategoriasServicio.ToListAsync();
                ViewBag.CategoriaId = new SelectList(categorias, "CategoriaId", "Nombre", servicio.CategoriaId);

                return View(servicio); // Si el modelo no es válido, retorna la vista con los errores
            }

            try
            {
                var servicioExistente = await _context.Servicios.FindAsync(id);
                if (servicioExistente == null)
                {
                    return NotFound("Servicio no encontrado.");
                }

                servicioExistente.Nombre = servicio.Nombre;
                servicioExistente.Descripcion = servicio.Descripcion;
                servicioExistente.Precio = servicio.Precio;
                servicioExistente.CategoriaId = servicio.CategoriaId;

                _context.Update(servicioExistente);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index)); // Redirige a la vista de índice (lista)
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Error de concurrencia: {ex.Message}");
                return NotFound("Error al actualizar el servicio.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return StatusCode(500, "Error interno del servidor.");
            }
        }











































        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("El ID no puede ser nulo.");
            }

            var servicio = await _context.Servicios
                .Include(s => s.Categoria)
                .FirstOrDefaultAsync(m => m.ServicioId == id);

            if (servicio == null)
            {
                return NotFound("Servicio no encontrado.");
            }

            return View(servicio);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var servicio = await _context.Servicios.FindAsync(id);
                if (servicio != null)
                {
                    _context.Servicios.Remove(servicio);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al eliminar el servicio: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicios.Any(e => e.ServicioId == id);
        }
    }
}
