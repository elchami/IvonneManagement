using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IvonneManagement.Models
{
    public class PagoMantenimiento
    {
        public int IdPago { get; set; }
        [DisplayName("Nombre de Inquilino")]
        public int InquilinoId { get; set; }
        public double Monto { get; set; }
        [DisplayName("Nombre del Apartamento")]
        public int ApartamentoId { get; set; }
        public string Estado { get; set; }//Al dia, vigente, atrasado....

        public virtual Inquilino Inquilino { get; set; }
        public virtual Apartamento Apartamento { get; set; }
    }
}
