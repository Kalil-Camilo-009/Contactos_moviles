using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Contactos_moviles.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;

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
            if (id == null) return NotFound();

            var contacto = await _context.Contactos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacto == null) return NotFound();

            return View(contacto);
        }

        // GET: Contactoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contactoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Telefono,Correo,Fecha,Direccion")] Contacto contacto)
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
            if (id == null) return NotFound();

            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null) return NotFound();

            return View(contacto);
        }

        // POST: Contactoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Telefono,Correo,Fecha,Direccion")] Contacto contacto)
        {
            if (id != contacto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExists(contacto.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contacto);
        }

        // GET: Contactoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var contacto = await _context.Contactos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacto == null) return NotFound();

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
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ContactoExists(int id)
        {
            return _context.Contactos.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Buscar(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
            {
                return RedirectToAction("Index");
            }

            var resultados = await _context.Contactos
                .Where(c =>
                    c.Nombre.Contains(filtro) ||
                    c.Apellido.Contains(filtro) ||
                    c.Telefono.Contains(filtro))
                .ToListAsync();

            return View(resultados);
        }

        
        public IActionResult ImprimirPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var contactos = _context.Contactos.ToList();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);

                    page.Header()
                        .Text("Listado de Contactos")
                        .FontSize(20)
                        .SemiBold().FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // Nombre
                                columns.RelativeColumn(); // Apellido
                                columns.RelativeColumn(); // Teléfono
                                columns.RelativeColumn(); // Correo
                                columns.RelativeColumn(); // Dirección
                            });

                            // Encabezados
                            table.Header(header =>
                            {
                                header.Cell().Text("Nombre").SemiBold();
                                header.Cell().Text("Apellido").SemiBold();
                                header.Cell().Text("Teléfono").SemiBold();
                                header.Cell().Text("Correo").SemiBold();
                                header.Cell().Text("Dirección").SemiBold();
                            });

                            // Datos
                            foreach (var c in contactos)
                            {
                                table.Cell().Text(c.Nombre);
                                table.Cell().Text(c.Apellido);
                                table.Cell().Text(c.Telefono);
                                table.Cell().Text(c.Correo);
                                table.Cell().Text(c.Direccion ?? "—");
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generado el ");
                            x.Span(DateTime.Now.ToString("dd/MM/yyyy")).SemiBold();
                        });
                });
            });

            byte[] pdf = document.GeneratePdf();
            return File(pdf, "application/pdf", "Contactos.pdf");
        }
    }
}


