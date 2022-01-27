using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IvonneManagement.Models
{
    public class Apartamento
    {
        public int IdApt { get; set; }
        [DisplayName("Nombre del Apartamento")]
        public string Nombre { get; set; }
        [DisplayName("Nombre del Inquilino")]
        public int InquilinoId { get; set; }
        public string Estado { get; set; }//Rentado, Disponible...etc

        public virtual Inquilino  Inquilino { get; set; }
        public virtual IEnumerable<PagoMantenimiento> PagoMantenimientos { get; set; }
        public virtual IEnumerable<RentaParqueo> RentaParqueos { get; set; }


    }
}
