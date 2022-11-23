using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity.dbo
{
    [Table("Bancos")]
    public class Bancos
    {
        [Key]
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Texto { get; set; }
        public bool Estado { get; set; }
    }
}
