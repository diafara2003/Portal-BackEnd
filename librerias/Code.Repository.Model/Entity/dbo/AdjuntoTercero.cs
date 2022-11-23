
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity
{
    [Table("AdjuntoTercero")]
  public  class AdjuntoTercero
    {

      

        [Key]
        public int AdjTerId { get; set; }
        public int AjdTerTerId { get; set; }
        public int AjdTerIdAdjunto { get; set; }
        public int AjdTerTipo { get; set; }
    }
}
