using IvonneManagement.Context;
using IvonneManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IvonneManagement.Services
{
    public class RentaParqueoService : IRentaParqueoService
    {
        private readonly IvonneManagementContext db;
        public RentaParqueoService(IvonneManagementContext _db)
        {
            db = _db;
        }

        public async Task<List<RentaParqueo>> ObtenerRentaParqueos()
        {
            return await db.RentaParqueos.ToListAsync();
        }

        public async Task<RentaParqueo> ObtenerRentaParqueo(int? id)
        {

            if (id != null)
            {
                var rentaParqueo = await db.RentaParqueos.FirstOrDefaultAsync(m => m.Id == id);
                if (rentaParqueo != null)
                {
                    return rentaParqueo;
                }
            }
            return null;
        }

        public async Task<bool> CrearRentaParqueo(RentaParqueo rentaParqueo)
        {
            if (rentaParqueo != null)
            {
                db.Add(rentaParqueo);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditarRentaParqueo(int? id, RentaParqueo rentaParqueo)
        {
            if (id == rentaParqueo.Id)
            {
                try
                {
                    db.Update(rentaParqueo);
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

        public async Task<bool> EliminarRentaParqueo(int? id)
        {
            if (id != null)
            {
                var rentaParqueo = await db.RentaParqueos.FindAsync(id);
                db.RentaParqueos.Remove(rentaParqueo);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IQueryable<RentaParqueo> GetQuery()
        {
            return db.Set<RentaParqueo>();
        }
    }
}
