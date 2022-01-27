using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IvonneManagement.Models
{
    public class RentaParqueo
    {
        public int Id { get; set; }
        [DisplayName("Apartamento Correspondiente")]
        public int IdApt { get; set; }
        [DisplayName("Inquilino a Rentar")]
        public int InquilinoId { get; set; }
        public double Monto { get; set; }
        public string Estado { get; set; }//Disponible, en uso....etc.

        public virtual Inquilino Inquilino { get; set; }
        public virtual Apartamento Apartamento { get; set; }

    }
}
