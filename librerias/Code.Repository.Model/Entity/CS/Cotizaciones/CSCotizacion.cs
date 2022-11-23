using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity.CS.Cotizaciones
{
    [Table("Cotizaciones", Schema = "CS")]
    public class CSCotizacion
    {
        [Key]
        public int CotId { get; set; }
        public int CotLicitacionId { get; set; }
        public int CotProveedorId { get; set; }
        public string CotFecha { get; set; }
        public int CotEstado { get; set; }
        public int CotValor { get; set; }
        public decimal CotIVA { get; set; }
        public bool CotAIU { get; set; }
        public decimal CotPorcAdministracion { get; set; }
        public decimal CotPorcImprevistos { get; set; }
        public decimal CotPorcUtilidad { get; set; }
        public int CotOrigen { get; set; }
        public int CotFormaPago { get; set; }
    }
}
