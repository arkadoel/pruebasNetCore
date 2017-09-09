using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace consolaYentityFramework.Models
{
    public partial class dbpruebasContext : DbContext
    {
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Telefono> Telefono { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql(@"Host=localhost;Database=dbpruebas;Username=postgres;Password=toor");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("persona");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Telefono>(entity =>
            {
                entity.HasKey(e => e.Idtelefono);

                entity.ToTable("telefono");

                entity.Property(e => e.Idtelefono).HasColumnName("idtelefono");

                entity.Property(e => e.Idpersona)
                    .HasColumnName("idpersona")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Phonenumber).HasColumnName("phonenumber");
            });
        }
    }
}
