using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Examen.Models;

public partial class EscuelaContext : DbContext
{
    public EscuelaContext()
    {
    }

    public EscuelaContext(DbContextOptions<EscuelaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Calificacione> Calificaciones { get; set; }

    public virtual DbSet<Kardex> Kardexs { get; set; }

    public virtual DbSet<Maestro> Maestros { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("SERVER=localhost;Database=Escuela;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.AlumnoId).HasName("PK__alumnos__D35EA68A13B42B6E");

            entity.ToTable("alumnos");

            entity.HasIndex(e => e.NoControl, "UQ__alumnos__11772D89EE372489").IsUnique();

            entity.Property(e => e.AlumnoId).HasColumnName("alumno_id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("datetime")
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Genero)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("genero");
            entity.Property(e => e.NoControl).HasColumnName("no_control");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Calificacione>(entity =>
        {
            entity.HasKey(e => e.CalificacionId).HasName("PK__califica__C87A8F18923C1F98");

            entity.ToTable("calificaciones");

            entity.Property(e => e.CalificacionId).HasColumnName("calificacion_id");
            entity.Property(e => e.Final)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("final");
            entity.Property(e => e.ParcialDos)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("parcial_dos");
            entity.Property(e => e.ParcialTres)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("parcial_tres");
            entity.Property(e => e.ParcialUno)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("parcial_uno");
        });

        modelBuilder.Entity<Kardex>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__kardexs__3213E83F4BCC2FDC");

            entity.ToTable("kardexs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CalificacionId).HasColumnName("calificacion_id");
            entity.Property(e => e.MateriaId).HasColumnName("materia_id");
            entity.Property(e => e.NoControl).HasColumnName("no_control");

            entity.HasOne(d => d.Calificacion).WithMany(p => p.Kardices)
                .HasForeignKey(d => d.CalificacionId)
                .HasConstraintName("FK__kardexs__calific__30F848ED");

            entity.HasOne(d => d.Materia).WithMany(p => p.Kardices)
                .HasForeignKey(d => d.MateriaId)
                .HasConstraintName("FK__kardexs__materia__2F10007B");

            entity.HasOne(d => d.NoControlNavigation).WithMany(p => p.Kardices)
                .HasPrincipalKey(p => p.NoControl)
                .HasForeignKey(d => d.NoControl)
                .HasConstraintName("FK__kardexs__no_cont__300424B4");
        });

        modelBuilder.Entity<Maestro>(entity =>
        {
            entity.HasKey(e => e.MaestroId).HasName("PK__maestros__108F5D83F1734362");

            entity.ToTable("maestros");

            entity.Property(e => e.MaestroId).HasColumnName("maestro_id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.MateriaId).HasColumnName("materia_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.Materia).WithMany(p => p.Maestros)
                .HasForeignKey(d => d.MateriaId)
                .HasConstraintName("FK__maestros__materi__276EDEB3");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.MateriaId).HasName("PK__materias__9AD87C738E68B207");

            entity.ToTable("materias");

            entity.HasIndex(e => e.Nombre, "UQ__materias__72AFBCC6636B3142").IsUnique();

            entity.Property(e => e.MateriaId).HasColumnName("materia_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
