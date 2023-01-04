using Examen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Examen.Controllers
{
    public class KardexController : Controller
    {
        private readonly EscuelaContext _context;

        public KardexController(EscuelaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            // Para la correcion de enlaces que requieren un id
            TempData["noControl"] = id;
            var MateriasLista = await _context.Kardexs
                .Where(x => x.NoControl == id)
                .Include(x => x.Materia)
                .Include(x => x.Calificacion).ToListAsync();

            return View(MateriasLista);
        }

        public IActionResult Create(int id) {

            ViewData["Lista"] = new SelectList(_context.Materias, "MateriaId", "Nombre");
            TempData["noControl"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, Kardex kardex)
        {
            TempData["noControl"] = id;
            var materiaExistente = _context.Kardexs.Where(x => x.NoControl == id && x.MateriaId == kardex.MateriaId).Any();

            if (ModelState.IsValid && !materiaExistente)
            {
                var nuevoKardex = new Calificacione();
                _context.Add(nuevoKardex);
                await _context.SaveChangesAsync();

                var nuevaMateria = new Kardex()
                {
                    MateriaId = kardex.MateriaId,
                    NoControl = id,
                    CalificacionId = nuevoKardex.CalificacionId
                };
                _context.Add(nuevaMateria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id });
            }
            ViewData["Lista"] = new SelectList(_context.Materias, "MateriaId", "Nombre", kardex.MateriaId);
            return View(kardex);
        }
    }
}
