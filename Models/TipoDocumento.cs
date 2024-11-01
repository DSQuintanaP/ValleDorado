using System;
using System.Collections.Generic;

namespace ValleDorado.Models;

public partial class TipoDocumento
{
    public int TipoDocumento1 { get; set; }

    public string? NombreTipoDocumento { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
