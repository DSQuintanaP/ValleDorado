using System;
using System.Collections.Generic;

namespace ValleDorado.Models;

public partial class PaquetePrincipal
{
    public int IdPaquete { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal PrecioTotal { get; set; }

    public int Duracion { get; set; }

    public virtual ICollection<DetallePaquete> DetallePaquetes { get; set; } = new List<DetallePaquete>();

    public virtual ICollection<PaquetesHabitacione> PaquetesHabitaciones { get; set; } = new List<PaquetesHabitacione>();

    public virtual ICollection<PaquetesServicio> PaquetesServicios { get; set; } = new List<PaquetesServicio>();
}
