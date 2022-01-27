using IvonneManagement.Context;
using IvonneManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IvonneManagement.Services
{
    public class PagoMantenimientoService : IPagoMantenimientoService
    {
        private readonly IvonneManagementContext db;
        public PagoMantenimientoService(IvonneManagementContext _db)
        {
            db = _db;
        }

        public async Task<List<PagoMantenimiento>> ObtenerPagoMantenimientos()
        {
            return await db.PagoMantenimientos.ToListAsync();
        }

        public async Task<PagoMantenimiento> ObtenerPagoMantenimiento(int? id)
        {

            if (id != null)
            {
                var pagoMant = await db.PagoMantenimientos.FirstOrDefaultAsync(m => m.IdPago == id);
                if (pagoMant != null)
                {
                    return pagoMant;
                }
            }
            return null;
        }

        public async Task<bool> CrearPagoMantenimiento(PagoMantenimiento pagoMant)
        {
            if (pagoMant != null)
            {
                db.Add(pagoMant);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditarPagoMantenimiento(int? id, PagoMantenimiento pagoMant)
        {
            if (id == pagoMant.IdPago)
            {
                try
                {
                    db.Update(pagoMant);
                    await db.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return false;
        }

        public async Task<bool> EliminarPagoMantenimiento(int? id)
        {
            if (id != null)
            {
                var pagoMant = await db.PagoMantenimientos.FindAsync(id);
                db.PagoMantenimientos.Remove(pagoMant);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IQueryable<PagoMantenimiento> GetQuery()
        {
            return db.Set<PagoMantenimiento>();
        }
    }
}
