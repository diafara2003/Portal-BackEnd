using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity.inf
{
    [Table("ParametrosInforme", Schema = "inf")]
    public  class ParametrosInforme
    {
        [Key]
        public int ParmId { get; set; }
        public int ParamIdInforme { get; set; }
        public string ParamTexto { get; set; }
        public string ParamValorDefault { get; set; }
    }
}
