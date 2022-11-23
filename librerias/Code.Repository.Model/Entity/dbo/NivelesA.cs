using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity
{
    [Table("nivelesA", Schema = "dbo")]
    public class NivelesA
    {
        [Key]
        public int NivId { get; set; }
        public int NivEmpresa { get; set; }
        public string NivNombre { get; set; }
        public bool NivEstado { get; set; }
        public string NivTipo { get; set; }

    }
}
