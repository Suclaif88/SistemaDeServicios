using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaServicios.Data;
using SistemaServicios.Models;

namespace SistemaServicios.Controllers
{
    public class SolicitudesServicioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SolicitudesServicioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SolicitudesServicio
        public async Task<IActionResult> Index()
        {
            var solicitudesServicio = _context.SolicitudesServicio
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .Include(s => s.Servicio)
                .ToListAsync();

            return View(await solicitudesServicio);
        }

        // GET: SolicitudesServicio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudServicio = await _context.SolicitudesServicio
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .Include(s => s.Servicio)
                .FirstOrDefaultAsync(m => m.SolicitudId == id);

            if (solicitudServicio == null)
            {
                return NotFound();
            }

            return View(solicitudServicio);
        }


        // GET: SolicitudesServicio/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre");
            ViewData["TecnicoId"] = new SelectList(_context.Tecnicos, "TecnicoId", "Nombre");
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "ServicioId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SolicitudId,ClienteId,TecnicoId,ServicioId,FechaSolicitud,Estado,Comentarios")] SolicitudServicio solicitudServicio)
        {
            // Verificar los valores recibidos
            Console.WriteLine($"ClienteId: {solicitudServicio.ClienteId}, TecnicoId: {solicitudServicio.TecnicoId}, ServicioId: {solicitudServicio.ServicioId}, FechaSolicitud: {solicitudServicio.FechaSolicitud}, Estado: {solicitudServicio.Estado}, Comentarios: {solicitudServicio.Comentarios}");

            if (ModelState.IsValid)
            {
                try
                {
                    // Verificación adicional de campos nulos antes de guardar
                    if (solicitudServicio.ClienteId == null || solicitudServicio.TecnicoId == null || solicitudServicio.ServicioId == null)
                    {
                        ModelState.AddModelError("", "Debe seleccionar un cliente, un técnico y un servicio.");
                    }
                    else
                    {
                        _context.Add(solicitudServicio);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Error al guardar la solicitud de servicio: {ex.Message}");
                    ModelState.AddModelError("", "No se pudo guardar la solicitud de servicio debido a un error de base de datos.");
                }
            }
            else
            {
                // Depurar errores de validación
                Console.WriteLine("Errores de validación:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            // Recargar listas si el modelo no es válido
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", solicitudServicio.ClienteId);
            ViewData["TecnicoId"] = new SelectList(_context.Tecnicos, "TecnicoId", "Nombre", solicitudServicio.TecnicoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "ServicioId", "Nombre", solicitudServicio.ServicioId);
            return View(solicitudServicio);
        }


        // GET: SolicitudesServicio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudServicio = await _context.SolicitudesServicio.FindAsync(id);
            if (solicitudServicio == null)
            {
                return NotFound();
            }

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", solicitudServicio.ClienteId);
            ViewData["TecnicoId"] = new SelectList(_context.Tecnicos, "TecnicoId", "Nombre", solicitudServicio.TecnicoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "ServicioId", "Nombre", solicitudServicio.ServicioId);
            return View(solicitudServicio);
        }

        // POST: SolicitudesServicio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SolicitudId,ClienteId,TecnicoId,ServicioId,FechaSolicitud,Estado,Comentarios")] SolicitudServicio solicitudServicio)
        {
            if (id != solicitudServicio.SolicitudId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitudServicio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudServicioExists(solicitudServicio.SolicitudId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", solicitudServicio.ClienteId);
            ViewData["TecnicoId"] = new SelectList(_context.Tecnicos, "TecnicoId", "Nombre", solicitudServicio.TecnicoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "ServicioId", "Nombre", solicitudServicio.ServicioId);
            return View(solicitudServicio);
        }

        // GET: SolicitudesServicio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudServicio = await _context.SolicitudesServicio
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .Include(s => s.Servicio)
                .FirstOrDefaultAsync(m => m.SolicitudId == id);
            if (solicitudServicio == null)
            {
                return NotFound();
            }

            return View(solicitudServicio);
        }

        // POST: SolicitudesServicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitudServicio = await _context.SolicitudesServicio.FindAsync(id);
            _context.SolicitudesServicio.Remove(solicitudServicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudServicioExists(int id)
        {
            return _context.SolicitudesServicio.Any(e => e.SolicitudId == id);
        }
    }
}
