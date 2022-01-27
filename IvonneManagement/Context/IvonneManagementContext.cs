using IvonneManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IvonneManagement.Context
{
    public class IvonneManagementContext : DbContext
    {
        public IvonneManagementContext(DbContextOptions<IvonneManagementContext> options)
            : base(options)
        { }
        public DbSet<Inquilino> Inquilinos { get; set; }
        public DbSet<Apartamento> Apartamentos { get; set; }
        public DbSet<RentaParqueo> RentaParqueos { get; set; }
        public DbSet<PagoMantenimiento> PagoMantenimientos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inquilino>(opt =>
            {
                opt.HasKey(x => x.Id);
                opt.Property(x => x.Nombre).HasMaxLength(30);
                opt.Property(x => x.Apellidos).HasMaxLength(60);
                opt.Property(x => x.Cedula).IsRequired();

            });

            modelBuilder.Entity<Apartamento>(opt =>
            {
                opt.HasKey(x => x.IdApt);

                opt.HasOne(x => x.Inquilino)
                .WithMany(x => x.Apartamentos)
                .HasForeignKey(x => x.InquilinoId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RentaParqueo>(opt =>
            {
                opt.HasKey(x => x.Id);

                opt.HasOne(x => x.Inquilino)
                .WithMany(x => x.RentaParqueos)
                .HasForeignKey(x => x.InquilinoId)
                .OnDelete(DeleteBehavior.Restrict);

                opt.HasOne(x => x.Apartamento)
                .WithMany(x => x.RentaParqueos)
                .HasForeignKey(x => x.IdApt)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PagoMantenimiento>(opt =>
            {
                opt.HasKey(x => x.IdPago);

                opt.HasOne(x => x.Inquilino)
                .WithMany(x => x.PagoMantenimientos)
                .HasForeignKey(x => x.InquilinoId)
                .OnDelete(DeleteBehavior.Restrict);

                opt.HasOne(x => x.Apartamento)
                .WithMany(x => x.PagoMantenimientos)
                .HasForeignKey(x => x.ApartamentoId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
