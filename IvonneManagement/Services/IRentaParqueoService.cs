using IvonneManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IvonneManagement.Services
{
    public interface IRentaParqueoService
    {
        Task<bool> CrearRentaParqueo(RentaParqueo rentaParqueo);
        Task<bool> EditarRentaParqueo(int? id, RentaParqueo rentaParqueo);
        Task<bool> EliminarRentaParqueo(int? id);
        Task<RentaParqueo> ObtenerRentaParqueo(int? id);
        Task<List<RentaParqueo>> ObtenerRentaParqueos();
        IQueryable<RentaParqueo> GetQuery();
    }
}