using IvonneManagement.Context;
using IvonneManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IvonneManagement.Services
{
    public class InquilinoService : IInquilinoService
    {
        private readonly IvonneManagementContext db;
        public InquilinoService(IvonneManagementContext _db)
        {
            db = _db;
        }

        public async Task<List<Inquilino>> ObtenerInquilinos()
        {
            return await db.Inquilinos.ToListAsync();
        }

        public async Task<Inquilino> ObtenerInquilino(int? id)
        {

            if (id != null)
            {
                var inquilino = await db.Inquilinos.FirstOrDefaultAsync(m => m.Id == id);
                if (inquilino != null)
                {
                    return inquilino;
                }
            }
            return null;
        }

        public async Task<bool> CrearInquilino(Inquilino inquilino)
        {
            if (inquilino != null)
            {
                db.Add(inquilino);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditarInquilino(int? id, Inquilino inquilino)
        {
            if (id == inquilino.Id)
            {
                try
                {
                    db.Update(inquilino);
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

        public async Task<bool> EliminarInquilino(int? id)
        {
            if (id != null)
            {
                var inquilino = await db.Inquilinos.FindAsync(id);
                db.Inquilinos.Remove(inquilino);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
