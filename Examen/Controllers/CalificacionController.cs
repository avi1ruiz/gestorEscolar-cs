using Examen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examen.Controllers
{
    public class CalificacionController : Controller
    {
        private readonly EscuelaContext _context;

        public CalificacionController(EscuelaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            // Comprobacion de id nulo no necesaria

            // Buscan calificaciones asociadas a la fila del kardex
            var calificaciones = await _context.Calificaciones.FindAsync(id);

            // No de Control para retornar a la vista del kardex del alumno
            // Debe haber un modo de ahorrar busquedas innecesarias, a lo mejor con campo de clase
            // aun no entiendo patrones de diseño pero a lo mejor con repository?
            var alumno = _context.Kardexs.Where(x => x.CalificacionId == id).Single();
            TempData["noControl"] = alumno.NoControl;
            return View(calificaciones);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, [Bind("CalificacionId,ParcialUno,ParcialDos,ParcialTres")] Calificacione calificacion, int noControl)
        {

            // Se realiza el calculo de la calificación final
            if (calificacion.ParcialUno != null && calificacion.ParcialDos != null && calificacion.ParcialTres != null)
            {
                uint final = Convert.ToUInt32((calificacion.ParcialUno + calificacion.ParcialDos + calificacion.ParcialTres) / 3);
                calificacion.Final = final;
            }

            _context.Update(calificacion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Kardex", new { id = noControl });
        }
    }
}
