using System;
using System.Collections.Generic;

namespace ValleDorado.Models;

public partial class Usuario
{
    public string TipoDocumento { get; set; } = null!;

    public string Documento { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Celular { get; set; }

    public string? Direccion { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public bool Estado { get; set; }

    public int IdRol { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;
}
