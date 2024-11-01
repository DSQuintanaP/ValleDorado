using System;
using System.Collections.Generic;

namespace ValleDorado.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public DateTime FechaReserva { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Iva { get; set; }

    public decimal Total { get; set; }

    public string DocumentoCliente { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Abono> Abonos { get; set; } = new List<Abono>();

    public virtual ICollection<DetalleHabitacione> DetalleHabitaciones { get; set; } = new List<DetalleHabitacione>();

    public virtual ICollection<DetallePaquete> DetallePaquetes { get; set; } = new List<DetallePaquete>();

    public virtual ICollection<DetalleServicio> DetalleServicios { get; set; } = new List<DetalleServicio>();

    public virtual Cliente DocumentoClienteNavigation { get; set; } = null!;
}
