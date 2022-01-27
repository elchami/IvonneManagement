using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IvonneManagement.Models
{
    public class Inquilino
    {
        public int Id { get; set; }
        [DisplayName("Nombre del Inquilino")]
        public string Nombre { get; set; }
        [DisplayName("Apellidos del Inquilino")]
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        [DisplayName("Numero Telefonico")]
        public string NumeroTelefono { get; set; }
        [DisplayName("Estado")]
        public string EstadoInquilino { get; set; }

        public virtual IEnumerable<RentaParqueo> RentaParqueos { get; set; }
        public virtual IEnumerable<Apartamento> Apartamentos { get; set; }
        public virtual IEnumerable<PagoMantenimiento> PagoMantenimientos { get; set; }
    }
}
