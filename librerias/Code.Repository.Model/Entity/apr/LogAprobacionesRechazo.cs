using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity.apr
{

    [Table("LogAprobacionesRechazo", Schema = "apr")]
    public class LogAprobacionesRechazo
    {
        [Key]
        public int id { get; set; }
        public int idMotivo { get; set; }
        public int idApr { get; set; }
    }
}
