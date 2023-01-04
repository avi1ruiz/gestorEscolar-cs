using System;
using System.Collections.Generic;

namespace Examen.Models;

public partial class Alumno
{
    public int AlumnoId { get; set; }

    public int NoControl { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public virtual ICollection<Kardex> Kardices { get; } = new List<Kardex>();
}
