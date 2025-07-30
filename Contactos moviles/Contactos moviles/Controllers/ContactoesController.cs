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
    public class ContactoesController : Controller
    {
        private readonly PlataformaDbContext _context;

        public ContactoesController(PlataformaDbContext context)
        {
            _context = context;
        }

        // GET: Contactoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contactos.ToListAsync());
        }

        // GET: Contactoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // GET: Contactoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contactoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Telefono,Correo,Fecha")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contacto);
        }

        // GET: Contactoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }

        // POST: Contactoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Telefono,Correo,Fecha")] Contacto contacto)
        {
            if (id != contacto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExists(contacto.Id))
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
            return View(contacto);
        }

        // GET: Contactoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // POST: Contactoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto != null)
            {
                _context.Contactos.Remove(contacto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactoExists(int id)
        {
            return _context.Contactos.Any(e => e.Id == id);
        }
    }
}
