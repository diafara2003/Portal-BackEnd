using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity
{
    [Table("NotificacionesTerceros", Schema = "ADP_CS")]
    public class NotificacionTercero
    {
        [Key]
        public int NTId { get; set; }
        public int NTConstructora { get; set; }
        public int NTTercero { get; set; }
        public int NTNotificacion { get; set; }
        public bool NTEstado { get; set; }
    }
}
