using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity.Auditoria.dbo
{
    [Table("TipoAuditoria", Schema = "dbo")]
    public class TipoAuditoriaGeneral
    {
        [Key]
        public int IdTipoAuditoria { get; set; }
        public string NombreAuditoria { get; set; }
        public string GlosarioTipoAuditoria { get; set; }

    }
}
