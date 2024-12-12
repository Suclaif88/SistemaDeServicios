using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using SistemaServicios.Data;
using SistemaServicios.Models;

namespace SistemaServicios.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccesoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario modelo)
        {
            var contrasenaEncriptada = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: modelo.Contrasena,
                salt: System.Text.Encoding.ASCII.GetBytes("salt"),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == modelo.NombreUsuario && u.Contrasena == contrasenaEncriptada);

            if (usuario != null)
            {
                // Autenticar al usuario
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos");
                return View(modelo);
            }
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuario modelo)
        {
            if (ModelState.IsValid)
            {
                var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == modelo.NombreUsuario);

                if (usuarioExistente == null)
                {
                    // Encriptar la contraseña
                    var contrasenaEncriptada = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: modelo.Contrasena,
                        salt: System.Text.Encoding.ASCII.GetBytes("salt"),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));

                    modelo.Contrasena = contrasenaEncriptada;

                    _context.Usuarios.Add(modelo);
                    _context.SaveChanges();

                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "El nombre de usuario ya existe");
                    return View(modelo);
                }
            }

            return View(modelo);
        }
    }
}