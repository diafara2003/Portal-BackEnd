using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity
{
    [Table("TerCamaraComercio")]
    public class TerCamaraComercio
    {
        [Key]
        public int TerCamId { get; set; }
        public int TerCamTerId { get; set; }
        public string TerCamTipoDoc { get; set; }
        public string TerCamDocumento { get; set; }
        public string TerCapNombre { get; set; }
        public string TerCamCargo { get; set; }
    }
}
