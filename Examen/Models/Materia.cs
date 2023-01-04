using System;
using System.Collections.Generic;

namespace Examen.Models;

public partial class Materia
{
    public int MateriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Kardex> Kardices { get; } = new List<Kardex>();

    public virtual ICollection<Maestro> Maestros { get; } = new List<Maestro>();
}
