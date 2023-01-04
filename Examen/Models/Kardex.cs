using System;
using System.Collections.Generic;

namespace Examen.Models;

public partial class Kardex
{
    public int Id { get; set; }

    public int? MateriaId { get; set; }

    public int? NoControl { get; set; }

    public int? CalificacionId { get; set; }

    public virtual Calificacione? Calificacion { get; set; }

    public virtual Materia? Materia { get; set; }

    public virtual Alumno? NoControlNavigation { get; set; }
}
