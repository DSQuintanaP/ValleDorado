using System;
using System.Collections.Generic;

namespace ValleDorado.Models;

public partial class PaquetesServicio
{
    public int IdPaqueteServicio { get; set; }

    public int? IdPaquete { get; set; }

    public int? IdServicio { get; set; }

    public virtual PaquetePrincipal? IdPaqueteNavigation { get; set; }

    public virtual Servicio? IdServicioNavigation { get; set; }
}
