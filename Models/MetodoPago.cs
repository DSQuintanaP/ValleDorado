using System;
using System.Collections.Generic;

namespace ValleDorado.Models;

public partial class MetodoPago
{
    public int IdMetodoPago { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Abono> Abonos { get; set; } = new List<Abono>();
}
