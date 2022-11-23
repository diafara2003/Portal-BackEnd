
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity
{
    [Table("AdjuntosAprobados")]
    public class AdjuntosAprobados
    {
        [Key]
        public int Id { get; set; }
        public int IdAdjuntoTercero { get; set; }
        public int IdConstructora { get; set; }
        public int IdTercero { get; set; }
        public int IdAdjunto { get; set; }
    }
}
