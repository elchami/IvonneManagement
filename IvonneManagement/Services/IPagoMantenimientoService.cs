using IvonneManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IvonneManagement.Services
{
    public interface IPagoMantenimientoService
    {
        Task<bool> CrearPagoMantenimiento(PagoMantenimiento pagoMant);
        Task<bool> EditarPagoMantenimiento(int? id, PagoMantenimiento pagoMant);
        Task<bool> EliminarPagoMantenimiento(int? id);
        Task<PagoMantenimiento> ObtenerPagoMantenimiento(int? id);
        Task<List<PagoMantenimiento>> ObtenerPagoMantenimientos();
        IQueryable<PagoMantenimiento> GetQuery();
    }
}