using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Code.Repository.Model.Entity.inf
{

    [Table("Informe", Schema = "inf")]
    public class Informe
    {
        [Key]
        public int infid { get; set; }
        public string infTexto { get; set; }
        public string infAPI { get; set; }
        public bool InfPaginacion { get; set; }

    }
}
