using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity.dbo
{
    [Table("TerEstado", Schema ="dbo")]
    public class TerEstado
    {
        [Key]
        public int Id { get; set; }
        public string TerNombre { get; set; }
        public bool TerActivo { get; set; }

    }
}
