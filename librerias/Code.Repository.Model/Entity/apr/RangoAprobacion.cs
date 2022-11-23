using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity.apr
{
    [Table("RangoAprobacion",Schema ="apr")]
    public class RangoAprobacion
    {
        [Key]
        public int RAId { get; set; }
        public int RAIdEmpresa { get; set; }
        public int RAIdTipo { get; set; }
        public string RAprTipo { get; set; }
    }
}
