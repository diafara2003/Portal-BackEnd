using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity.apr
{
    [Table("LogAprobaciones", Schema = "apr")]
    public class LogAprobaciones

    {
        [Key]
        public int id { get; set; }
        public int LogIdDocumento { get; set; }
        public int LogIdEmpresa { get; set; }
        public int LogIdUsuario { get; set; }
        public int LogContador { get; set; }
        public DateTime LogFecha { get; set; }
        public bool LogIsAprobacion { get; set; }
        public string LogComentarios { get; set; }
    }
}
