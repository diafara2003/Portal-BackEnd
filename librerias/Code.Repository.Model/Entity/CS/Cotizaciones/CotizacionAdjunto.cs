using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity.CS.Cotizaciones
{
    [Table("CotizacionesAdjuntos", Schema = "CS")]
    public class CotizacionAdjunto
    {
        [Key]
        public int CAId { get; set; }
        public int CACotizacionId { get; set; }
        public int CADocId { get; set; }
        public string CANombre { get; set; }
        public string CARuta { get; set; }
        public string CAObservacion { get; set; }

    }
}
