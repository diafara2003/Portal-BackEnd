

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity
{
    [Table("TercerosConstructora", Schema = "dbo")]
    public  class TercerosConstructora
    {
        [Key]
        public int IdTerCons { get; set; }
        public int IdTercero { get; set; }
        public int IdConstructora { get; set; }
        public int Estado { get; set; }
    }
}
