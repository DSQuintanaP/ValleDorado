using System;
using System.Collections.Generic;

namespace ValleDorado.Models;

public partial class TipoServicio
{
    public int IdTipo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
