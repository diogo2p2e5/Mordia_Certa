using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MordidaCerta.WebAPI.Models;

namespace MordidaCerta.WebAPI.BdContextLanches;

public partial class LanchesContext : DbContext
{
    public LanchesContext()
    {
    }

    public LanchesContext(DbContextOptions<LanchesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Comida> Comidas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MordidaCerta_DB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10EAD34BE3");
        });

        modelBuilder.Entity<Comida>(entity =>
        {
            entity.HasKey(e => e.IdComida).HasName("PK__Comida__055A1AA3F69A9F4E");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Comida).HasConstraintName("FK__Comida__IdCatego__5FB337D6");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF979B716B0E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
