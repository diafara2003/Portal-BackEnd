using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity.inf
{
    [Table("ColumnasInforme", Schema = "inf")]
    public class ColumnasInforme
    {
        [Key]
        public int ColId { get; set; }
        public int ColIdInforme { get; set; }
        public string ColNombre { get; set; }
        public string ColNombreAPI { get; set; }
        public string ColAlign { get; set; }
        public bool ColFormatoNumerico { get; set; }
        public bool ColOpenLink { get; set; }

    }
}
