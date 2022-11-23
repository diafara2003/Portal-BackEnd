using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity.adp_cs
{
    [Table("LicitacionesTerceros", Schema = "ADP_CS")]
    public class LicitacionTercero
    {
        [Key]
        public int LTId { get; set; }
        public int LTLicitacionId { get; set; }
        public int LTTerceroId { get; set; }
        public int LTEstado { get; set; }
        public int LTAsegurado { get; set; }
    }
}
