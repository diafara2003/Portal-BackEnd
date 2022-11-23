using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Code.Repository.Model.Entity.dbo
{
    [Table("TerInfoTributaria")]
    public class TerInfoTributaria
    {
        [Key]
        public int Id { get; set; }
        public int IdTercero { get; set; }
        public bool ResponsableIVA { get; set; }
        public bool Autorretenedor { get; set; }
        public bool Declarante { get; set; }
        public bool GranContribuyente { get; set; }
        public bool AutoRetenedorICA { get; set; }
    }
}
