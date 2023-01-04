using System;
using System.Collections.Generic;

namespace Examen.Models;

public partial class Calificacione
{
    public int CalificacionId { get; set; }

    public decimal? ParcialUno { get; set; }

    public decimal? ParcialDos { get; set; }

    public decimal? ParcialTres { get; set; }

    public decimal? Final { get; set; }

    public virtual ICollection<Kardex> Kardices { get; } = new List<Kardex>();
}
