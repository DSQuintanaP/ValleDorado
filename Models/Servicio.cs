using System;
using System.Collections.Generic;

namespace ValleDorado.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdTipoServicio { get; set; }

    public bool Estado { get; set; }

    public decimal Precio { get; set; }

    public virtual ICollection<DetalleServicio> DetalleServicios { get; set; } = new List<DetalleServicio>();

    public virtual TipoServicio IdTipoServicioNavigation { get; set; } = null!;

    public virtual ICollection<PaquetesServicio> PaquetesServicios { get; set; } = new List<PaquetesServicio>();
}
