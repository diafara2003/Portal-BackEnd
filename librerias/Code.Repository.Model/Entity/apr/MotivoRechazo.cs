using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity.apr
{


    [Table("MotivoRechazo", Schema = "apr")]
    public class MotivoRechazo
    {
        [Key]
        public int id { get; set; }
        public string MotCodigo { get; set; }
        public string MotTexto { get; set; }
    }
}
