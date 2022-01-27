using IvonneManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IvonneManagement.Services
{
    public interface IApartamentoService
    {
        Task<List<Apartamento>> ObtenerApartamentos();
        Task<Apartamento> ObtenerApartamento(int? id);
        Task<bool> CrearApartamento(Apartamento apartamento);
        Task<bool> EditarApartamento(int? id, Apartamento apartamento);
        Task<bool> EliminarApartamento(int? id);
        IQueryable<Apartamento> GetQuery();
    }
}
