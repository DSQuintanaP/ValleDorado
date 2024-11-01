using System;
using System.Collections.Generic;

namespace ValleDorado.Models;

public partial class HabitacionesInmueble
{
    public int Id { get; set; }

    public int IdHabitacion { get; set; }

    public int IdInmueble { get; set; }

    public virtual Habitacione IdHabitacionNavigation { get; set; } = null!;

    public virtual Inmueble IdInmuebleNavigation { get; set; } = null!;
}
