using System;
using System.Collections.Generic;

namespace ValleDorado.Models;

public partial class Abono
{
    public int IdAbono { get; set; }

    public int IdReserva { get; set; }

    public DateTime FechaAbono { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Iva { get; set; }

    public decimal Total { get; set; }

    public bool Estado { get; set; }

    public string DocumentoCliente { get; set; } = null!;

    public int IdMetodoPago { get; set; }

    public virtual Cliente DocumentoClienteNavigation { get; set; } = null!;

    public virtual MetodoPago IdMetodoPagoNavigation { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
