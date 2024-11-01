using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ValleDorado.Models;

public partial class GoldenvalleyglampingContext : DbContext
{
    public GoldenvalleyglampingContext()
    {
    }

    public GoldenvalleyglampingContext(DbContextOptions<GoldenvalleyglampingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abono> Abonos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleHabitacione> DetalleHabitaciones { get; set; }

    public virtual DbSet<DetallePaquete> DetallePaquetes { get; set; }

    public virtual DbSet<DetalleServicio> DetalleServicios { get; set; }

    public virtual DbSet<Habitacione> Habitaciones { get; set; }

    public virtual DbSet<HabitacionesInmueble> HabitacionesInmuebles { get; set; }

    public virtual DbSet<Inmueble> Inmuebles { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<PaquetePrincipal> PaquetePrincipals { get; set; }

    public virtual DbSet<PaquetesHabitacione> PaquetesHabitaciones { get; set; }

    public virtual DbSet<PaquetesServicio> PaquetesServicios { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPermiso> RolesPermisos { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoHabitacione> TipoHabitaciones { get; set; }

    public virtual DbSet<TipoServicio> TipoServicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            //        => optionsBuilder.UseSqlServer("Server=DESKTOP-4VIBFLD\\SQLEXPRESS;Database=GOLDENVALLEYGLAMPING;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abono>(entity =>
        {
            entity.HasKey(e => e.IdAbono).HasName("PK__Abonos__A4693DA7612D2F74");

            entity.Property(e => e.DocumentoCliente)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaAbono)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("IVA");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.DocumentoClienteNavigation).WithMany(p => p.Abonos)
                .HasForeignKey(d => d.DocumentoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Abonos__Document__72C60C4A");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Abonos)
                .HasForeignKey(d => d.IdMetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Abonos__IdMetodo__73BA3083");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Abonos)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Abonos__IdReserv__74AE54BC");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Documento).HasName("PK__Clientes__AF73706C1ED3E6A7");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Clientes__531402F3C3A8F905").IsUnique();

            entity.Property(e => e.Documento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clientes__IdRol__48CFD27E");

            entity.HasOne(d => d.TipoDocumentoNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.TipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clientes__TipoDo__49C3F6B7");
        });

        modelBuilder.Entity<DetalleHabitacione>(entity =>
        {
            entity.HasKey(e => e.IdDetalleHabitacion).HasName("PK__DetalleH__15D58EF9544CE048");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.DetalleHabitaciones)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleHa__IdHab__02FC7413");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.DetalleHabitaciones)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleHa__IdRes__02084FDA");
        });

        modelBuilder.Entity<DetallePaquete>(entity =>
        {
            entity.HasKey(e => e.IdDetallePaquete).HasName("PK__DetalleP__4DD779D70830C871");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.DetallePaquetes)
                .HasForeignKey(d => d.IdPaquete)
                .HasConstraintName("FK__DetallePa__IdPaq__7E37BEF6");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.DetallePaquetes)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePa__IdRes__7D439ABD");
        });

        modelBuilder.Entity<DetalleServicio>(entity =>
        {
            entity.HasKey(e => e.IdDetalleServicio).HasName("PK__DetalleS__0BFF94E6A9B6ADCC");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.DetalleServicios)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleSe__IdRes__787EE5A0");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.DetalleServicios)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleSe__IdSer__797309D9");
        });

        modelBuilder.Entity<Habitacione>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__8BBBF901208D19D2");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdTipoHabitacionNavigation).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.IdTipoHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__IdTip__534D60F1");
        });

        modelBuilder.Entity<HabitacionesInmueble>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Habitaci__3214EC07D05D2D0D");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.HabitacionesInmuebles)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__IdHab__5BE2A6F2");

            entity.HasOne(d => d.IdInmuebleNavigation).WithMany(p => p.HabitacionesInmuebles)
                .HasForeignKey(d => d.IdInmueble)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__IdInm__5CD6CB2B");
        });

        modelBuilder.Entity<Inmueble>(entity =>
        {
            entity.HasKey(e => e.IdInmueble).HasName("PK__Inmueble__6B8E7D3EDCC8C8B2");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TipoInmueble)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__MetodoPa__6F49A9BE342EECC1");

            entity.ToTable("MetodoPago");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaquetePrincipal>(entity =>
        {
            entity.HasKey(e => e.IdPaquete).HasName("PK__PaqueteP__DE278F8B55C1D52A");

            entity.ToTable("PaquetePrincipal");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrecioTotal).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<PaquetesHabitacione>(entity =>
        {
            entity.HasKey(e => e.IdPaqueteHabitacion).HasName("PK__Paquetes__C0EF4AF0D66C65CD");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.PaquetesHabitaciones)
                .HasForeignKey(d => d.IdHabitacion)
                .HasConstraintName("FK__PaquetesH__IdHab__619B8048");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.PaquetesHabitaciones)
                .HasForeignKey(d => d.IdPaquete)
                .HasConstraintName("FK__PaquetesH__IdPaq__628FA481");
        });

        modelBuilder.Entity<PaquetesServicio>(entity =>
        {
            entity.HasKey(e => e.IdPaqueteServicio).HasName("PK__Paquetes__DE5C2BC23154B706");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.PaquetesServicios)
                .HasForeignKey(d => d.IdPaquete)
                .HasConstraintName("FK__PaquetesS__IdPaq__66603565");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.PaquetesServicios)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("FK__PaquetesS__IdSer__656C112C");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__Permisos__0D626EC84DD0CB0E");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.NombrePermiso)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reservas__0E49C69DED39DC2F");

            entity.Property(e => e.DocumentoCliente)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.FechaReserva)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("IVA");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.DocumentoClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.DocumentoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__Docume__6E01572D");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2A49584C82875CDE");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolesPermiso>(entity =>
        {
            entity.HasKey(e => e.IdRolPermiso).HasName("PK__RolesPer__0CC73B1B93753814");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.RolesPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .HasConstraintName("FK__RolesPerm__IdPer__3D5E1FD2");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolesPermisos)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__RolesPerm__IdRol__3C69FB99");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__2DCCF9A29B747458");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdTipoServicioNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.IdTipoServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TipoServicio");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.TipoDocumento1).HasName("PK__TipoDocu__FE2CE66AFDB5BD8C");

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.TipoDocumento1).HasColumnName("TipoDocumento");
            entity.Property(e => e.NombreTipoDocumento).HasMaxLength(80);
        });

        modelBuilder.Entity<TipoHabitacione>(entity =>
        {
            entity.HasKey(e => e.IdTipoHabitacion).HasName("PK__TipoHabi__AB75E87CDFAA2800");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.HasKey(e => e.IdTipo).HasName("PK__TipoServ__9E3A29A5BD252648");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Documento).HasName("PK__Usuarios__AF73706CD217BBB0");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuarios__531402F37D8C7C33").IsUnique();

            entity.Property(e => e.Documento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__IdRol__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
