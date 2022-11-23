using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity.dbo
{
    [Table("TipoCuentaBancaria")]
    public class TipoCuentaBancaria
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
    }
}
