using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity
{

    [Table("TipoAdjuntoTercero")]
  public  class TipoAdjuntoTercero
    {
        [Key]
        public int TipAdjId { get; set; }
        public string TipAdjTexto { get; set; }
    }
}
