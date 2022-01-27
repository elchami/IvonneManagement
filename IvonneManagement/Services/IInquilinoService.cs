using IvonneManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IvonneManagement.Services
{
    public interface IInquilinoService
    {
        Task<bool> CrearInquilino(Inquilino inquilino);
        Task<bool> EditarInquilino(int? id, Inquilino inquilino);
        Task<bool> EliminarInquilino(int? id);
        Task<Inquilino> ObtenerInquilino(int? id);
        Task<List<Inquilino>> ObtenerInquilinos();
    }
}