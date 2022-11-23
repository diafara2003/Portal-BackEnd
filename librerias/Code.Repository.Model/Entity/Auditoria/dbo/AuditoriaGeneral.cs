using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity.Auditoria.dbo
{
    [Table("AuditoriaGeneral", Schema = "dbo")]
    public class AuditoriaGeneral
    {
        [Key]
        public int Id { get; set; }
        public int Documento { get;  set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public int IdUsuario { get; set; }
        public int Fecha { get; set; }
        public string Opcion { get; set; }
        public int Hora { get; set; }
        public int TipoAuditoria { get; set; }
        public bool IsDelete { get; set; }
        public bool IsNew { get; set; }
    }
}
