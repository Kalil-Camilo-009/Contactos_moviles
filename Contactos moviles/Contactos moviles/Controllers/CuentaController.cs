using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Contactos_moviles.Models;

namespace Contactos_moviles.Controllers
{
    public class CuentaController : Controller
    {
        private readonly PlataformaDbContext _context;

        public CuentaController(PlataformaDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string nombreUsuario, string clave)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.NombreUsuario == nombreUsuario && u.Clave == clave);

            if (usuario != null)
            {
                // Simulando sesión
                TempData["usuario"] = usuario.NombreUsuario;
                return RedirectToAction("Index", "Contactoes");
            }
            else
            {
                ViewBag.Error = "Credenciales incorrectas";
                return View();
            }
        }

        public IActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Login");
        }
    }
}
