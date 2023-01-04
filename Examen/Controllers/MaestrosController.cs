using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen.Models;

namespace Examen.Controllers
{
    public class MaestrosController : Controller
    {
        private readonly EscuelaContext _context;

        public MaestrosController(EscuelaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var escuelaContext = _context.Maestros.Include(m => m.Materia);
            return View(await escuelaContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Maestros == null)
            {
                return NotFound();
            }

            var maestro = await _context.Maestros
                .Include(m => m.Materia)
                .FirstOrDefaultAsync(m => m.MaestroId == id);
            if (maestro == null)
            {
                return NotFound();
            }

            return View(maestro);
        }

        public IActionResult Create()
        {
            ViewData["Lista"] = new SelectList(_context.Materias, "MateriaId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaestroId,Nombre,Apellido,Telefono,Correo,MateriaId")] Maestro maestro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maestro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "MateriaId", maestro.MateriaId);
            return View(maestro);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Maestros == null)
            {
                return NotFound();
            }

            var maestro = await _context.Maestros.FindAsync(id);
            if (maestro == null)
            {
                return NotFound();
            }
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "MateriaId", maestro.MateriaId);
            return View(maestro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaestroId,Nombre,Apellido,Telefono,Correo,MateriaId")] Maestro maestro)
        {
            if (id != maestro.MaestroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maestro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaestroExists(maestro.MaestroId))
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
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "MateriaId", maestro.MateriaId);
            return View(maestro);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Maestros == null)
            {
                return NotFound();
            }

            var maestro = await _context.Maestros
                .Include(m => m.Materia)
                .FirstOrDefaultAsync(m => m.MaestroId == id);
            if (maestro == null)
            {
                return NotFound();
            }

            return View(maestro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Maestros == null)
            {
                return Problem("Entity set 'EscuelaContext.Maestros'  is null.");
            }
            var maestro = await _context.Maestros.FindAsync(id);
            if (maestro != null)
            {
                _context.Maestros.Remove(maestro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaestroExists(int id)
        {
          return (_context.Maestros?.Any(e => e.MaestroId == id)).GetValueOrDefault();
        }
    }
}
