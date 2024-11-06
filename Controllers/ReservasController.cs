using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ValleDorado.Models;

namespace ValleDorado.Controllers
{
    public class ReservasController : Controller
    {
        private readonly GoldenvalleyglampingContext _context;

        public ReservasController(GoldenvalleyglampingContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var goldenvalleyglampingContext = _context.Reservas.Include(r => r.DocumentoClienteNavigation);
            return View(await goldenvalleyglampingContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.DocumentoClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["DocumentoCliente"] = new SelectList(_context.Clientes, "Documento", "Documento");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReserva,FechaReserva,FechaInicio,FechaFin,Subtotal,Iva,Total,DocumentoCliente,Estado,IdPaquete,IdServicio,IdHabitacion")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                //_context.Add(reserva.DetalleHabitaciones);
                //_context.Add(_context.DetalleHabitaciones);
                //_context.Add(reserva.DetalleServicios);
                //_context.Add(_context.DetalleServicios);
                //_context.Add(reserva.DetallePaquetes);
                //_context.Add(_context.DetallePaquetes);
                await _context.SaveChangesAsync();

                foreach (var detalleHH in reserva.DetalleHabitaciones)
                {
                    detalleHH.IdDetalleHabitacion = reserva.IdReserva;
                    _context.Add(detalleHH);
                }
                foreach (var detalleSS in reserva.DetalleServicios)
                {
                    detalleSS.IdDetalleServicio = reserva.IdReserva;
                }
                foreach (var detallePP in reserva.DetallePaquetes)
                {
                    detallePP.IdDetallePaquete = reserva.IdReserva;
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            ViewData["DocumentoCliente"] = new SelectList(_context.Clientes, "Documento", "Documento", reserva.DocumentoCliente);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", reserva.DetalleServicios);
            ViewData["IdHabitacion"] = new SelectList(_context.Habitaciones, "IdHabitacion", "IdHabitacion", reserva.DetalleHabitaciones);
            ViewData["IdPaquete"] = new SelectList(_context.PaquetePrincipals, "IdPaquete", "IdPaquete", reserva.DetallePaquetes);
            return View(reserva);
        }
        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Crear(Reserva oReserva, string paqueteSeleccionado, string serviciosSeleccionados)
        {

            ViewBag.PaquetesDisponibles = _context.PaquetePrincipals.Where(s => s.Estado == true)
                    .ToList(); ;
            ViewBag.ServiciosDisponibles = _context.Servicios.Where(s => s.Estado == true && (s.IdServicio != 1 && s.IdServicio != 2 && s.IdServicio != 3))
                .ToList();
            ViewData["Error"] = "True";

            if (string.IsNullOrEmpty(paqueteSeleccionado))
            {
                ModelState.AddModelError("paqueteSeleccionados", "Seleccione un paquete");
                return View(CargarDatosIniciales());
            }

            if (string.IsNullOrEmpty(serviciosSeleccionados) || serviciosSeleccionados == "[]")
            {
                ViewData["ErrorServicio"] = "True";
                return View(CargarDatosIniciales());
            }

            if (!ModelState.IsValid)
            {
                return View(CargarDatosIniciales());
            }

            if (!Existe(oReserva.NroDocumentoCliente))
            {
                ModelState.AddModelError("oReserva.NroDocumentoCliente", "El cliente no existe");
                return View(CargarDatosIniciales());
            }

            var cliente = _context.Clientes.FirstOrDefault(c => c.NroDocumento == oReserva.NroDocumentoCliente);

            if (cliente.Estado == false)
            {
                ModelState.AddModelError("oReserva.NroDocumentoCliente", "El cliente esta inhabilitado");
                return View(CargarDatosIniciales());
            }

            if (cliente.Confirmado == false)
            {
                ModelState.AddModelError("oReserva.NroDocumentoCliente", "El cliente no ha confirmado su correo");
                return View(CargarDatosIniciales());
            }

            if (!ValidarFechas(oReserva))
            {
                return View(CargarDatosIniciales());
            }

            if (oReserva.Descuento == null)
            {
                oReserva.Descuento = 0;
            }

            _context.Reservas.Add(oReserva);
            _context.SaveChanges();

            var listaPaqueteSeleccionado = JsonConvert.DeserializeObject<List<dynamic>>(paqueteSeleccionado.ToString());

            if (listaPaqueteSeleccionado != null && listaPaqueteSeleccionado.Any())
            {
                var paquetes = listaPaqueteSeleccionado.Select(paquete => new Paquete
                {
                    IdPaquete = Convert.ToInt32(paquete.id),
                    Costo = Convert.ToDouble(paquete.costo)
                }).ToList();

                foreach (var paquete in paquetes)
                {
                    var DetalleReservaPaquete = new DetalleReservaPaquete
                    {
                        IdReserva = oReserva.IdReserva,
                        IdPaquete = paquete.IdPaquete,
                        Costo = paquete.Costo
                    };
                    _context.DetalleReservaPaquetes.Add(DetalleReservaPaquete);
                }
            }

            if (!string.IsNullOrEmpty(serviciosSeleccionados))
            {
                var listaServiciosSeleccionados = JsonConvert.DeserializeObject<List<dynamic>>(serviciosSeleccionados.ToString());

                if (listaServiciosSeleccionados != null && listaServiciosSeleccionados.Any())
                {
                    var servicios = listaServiciosSeleccionados.Select(servicio => new Servicio
                    {
                        IdServicio = Convert.ToInt32(servicio.id),
                        NomServicio = servicio.nombre.ToString(),
                        Costo = Convert.ToDouble(servicio.costo)
                    }).ToList();

                    for (int i = 0; i < listaServiciosSeleccionados.Count; i++)
                    {
                        if (listaServiciosSeleccionados[i].cantidad == null)
                        {
                            listaServiciosSeleccionados[i].cantidad = 1;
                        }
                        var DetalleReservaServicio = new DetalleReservaServicio
                        {
                            IdReserva = oReserva.IdReserva,
                            IdServicio = listaServiciosSeleccionados[i].id,
                            Costo = listaServiciosSeleccionados[i].costo,
                            Cantidad = listaServiciosSeleccionados[i].cantidad
                        };
                        _context.DetalleReservaServicios.Add(DetalleReservaServicio);
                    }
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Reservas");

        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["DocumentoCliente"] = new SelectList(_context.Clientes, "Documento", "Documento", reserva.DocumentoCliente);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", reserva.DetalleServicios);
            ViewData["IdHabitacion"] = new SelectList(_context.Habitaciones, "IdHabitacion", "IdHabitacion", reserva.DetalleHabitaciones);
            ViewData["IdPaquete"] = new SelectList(_context.PaquetePrincipals, "IdPaquete", "IdPaquete", reserva.DetallePaquetes);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,FechaReserva,FechaInicio,FechaFin,Subtotal,Iva,Total,DocumentoCliente,Estado")] Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.IdReserva))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DocumentoCliente"] = new SelectList(_context.Clientes, "Documento", "Documento", reserva.DocumentoCliente);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.DocumentoClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.IdReserva == id);
        }
    }
}
