using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{

    public partial class BdHcuchContext : DbContext
    {
        public BdHcuchContext()
        {
        }

        public BdHcuchContext(DbContextOptions<BdHcuchContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TPaciente> TPacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TPaciente>(entity =>
            {
                entity.HasKey(e => e.IdPaciente);

                entity.ToTable("T_Pacientes");

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
