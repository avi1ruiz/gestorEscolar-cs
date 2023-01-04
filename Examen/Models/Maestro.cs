using System;
using System.Collections.Generic;

namespace Examen.Models;

public partial class Maestro
{
    public int MaestroId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public int? MateriaId { get; set; }

    public virtual Materia? Materia { get; set; }
}
