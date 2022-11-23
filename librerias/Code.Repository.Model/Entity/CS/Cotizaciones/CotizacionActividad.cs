using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity.CS.Cotizaciones
{
    [Table("CotizacionesActividades", Schema = "CS")]
    public class CotizacionActividad
    {

        public CotizacionActividad()
        {
            CAId = -1;
            CACotizacionId = -1;
            CAActividadId = -1;
            CAValor = 0;
        }

        [Key]
        public int CAId { get; set; }
        public int CACotizacionId { get; set; }
        public int CAActividadId { get; set; }
        public decimal CAValor { get; set; }
    }
}
