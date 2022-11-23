using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity.adp_cs
{
    [Table("Cotizaciones", Schema = "ADP_CS")]
    public class CSCotizaciones
    {
        [Key]
        public int CLId { get; set; }        
        public int CLCotizacion { get; set; }
        public int CLTerceroId { get; set; }
        public int CLLicitacionId { get; set; }
        public decimal CLValor { get; set; }
        public string CLFormaPago { get; set; }
        public string CLTipoTributo { get; set; }
        public int CLEstado { get; set; }


    }
}
