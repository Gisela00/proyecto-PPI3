using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Berum_WebAPI.Models;

public partial class ReparacionesDbContext : DbContext
{
    public ReparacionesDbContext()
    {
    }

    public ReparacionesDbContext(DbContextOptions<ReparacionesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<HistorialEstado> HistorialEstados { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<OrdenReparacion> OrdenReparacions { get; set; }

    public virtual DbSet<Tecnico> Tecnicos { get; set; }

    public virtual DbSet<TipoEquipo> TipoEquipos { get; set; }

    public virtual DbSet<TipoFalla> TipoFallas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D5946642B87C437C");

            entity.ToTable("Cliente");

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.IdEquipo).HasName("PK__Equipo__D805240805CC7A58");

            entity.ToTable("Equipo");

            entity.Property(e => e.Modelo)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipo_Marca");

            entity.HasOne(d => d.IdTipoEquipoNavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.IdTipoEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipo_TipoEquipo");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC1B251083F");

            entity.ToTable("Estado");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HistorialEstado>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PK__Historia__9CC7DBB4FFA0CDC0");

            entity.ToTable("HistorialEstado");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.HistorialEstados)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Estado");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.HistorialEstados)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Orden");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marca__4076A8875362D602");

            entity.ToTable("Marca");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrdenReparacion>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("PK__OrdenRep__C38F300D9254ECD0");

            entity.ToTable("OrdenReparacion");

            entity.HasIndex(e => e.IdCliente, "IX_Orden_Cliente");

            entity.HasIndex(e => e.IdEstadoActual, "IX_Orden_Estado");

            entity.HasIndex(e => e.IdTecnico, "IX_Orden_Tecnico");

            entity.HasIndex(e => e.NumeroServicio, "UQ_NumeroServicio").IsUnique();

            entity.Property(e => e.DescripcionFalla)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NumeroServicio)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.OrdenReparacions)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orden_Cliente");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.OrdenReparacions)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orden_Equipo");

            entity.HasOne(d => d.IdEstadoActualNavigation).WithMany(p => p.OrdenReparacions)
                .HasForeignKey(d => d.IdEstadoActual)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orden_Estado");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.OrdenReparacions)
                .HasForeignKey(d => d.IdTecnico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orden_Tecnico");

            entity.HasOne(d => d.IdTipoFallaNavigation).WithMany(p => p.OrdenReparacions)
                .HasForeignKey(d => d.IdTipoFalla)
                .HasConstraintName("FK_Orden_TipoFalla");
        });

        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.HasKey(e => e.IdTecnico).HasName("PK__Tecnico__BF289893AF8CCBB0");

            entity.ToTable("Tecnico");

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Especialidad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoEquipo>(entity =>
        {
            entity.HasKey(e => e.IdTipoEquipo).HasName("PK__TipoEqui__B62FEC672687DC84");

            entity.ToTable("TipoEquipo");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoFalla>(entity =>
        {
            entity.HasKey(e => e.IdTipoFalla).HasName("PK__TipoFall__7864242949E1622C");

            entity.ToTable("TipoFalla");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF978BB6B0DC");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Usuario1, "UQ__Usuario__E3237CF7B5B6B889").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Usuario");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTecnico)
                .HasConstraintName("FK_Usuario_Tecnico");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
