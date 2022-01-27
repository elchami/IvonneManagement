using IvonneManagement.Context;
using IvonneManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IvonneManagement.Services
{
    public class ApartamentoService : IApartamentoService
    {
        private readonly IvonneManagementContext db;
        public ApartamentoService(IvonneManagementContext _db)
        {
            db = _db;
        }

        public async Task<List<Apartamento>> ObtenerApartamentos()
        {
            return await db.Apartamentos.ToListAsync();
        }

        public async Task<Apartamento> ObtenerApartamento(int? id)
        {

            if (id != null)
            {
                var apartamento = await db.Apartamentos.FirstOrDefaultAsync(m => m.IdApt == id);
                if (apartamento != null)
                {
                    return apartamento;
                }
            }
            return null;
        }

        public async Task<bool> CrearApartamento(Apartamento apartamento)
        {
            if (apartamento != null)
            {
                db.Add(apartamento);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditarApartamento(int? id, Apartamento apartamento)
        {
            if (id == apartamento.IdApt)
            {
                try
                {
                    db.Update(apartamento);
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

        public async Task<bool> EliminarApartamento(int? id)
        {
            if (id != null)
            {
                var apartamento = await db.Apartamentos.FindAsync(id);
                db.Apartamentos.Remove(apartamento);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IQueryable<Apartamento> GetQuery()
        {
            return db.Set<Apartamento>();
        }
    }
}
