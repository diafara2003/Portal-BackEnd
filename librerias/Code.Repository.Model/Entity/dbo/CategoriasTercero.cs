
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity
{
    [Table("EspecialidadesTercero")]
    public class EspecialidadesTercero
    {
        [Key]
        public int EspId { get; set; }
        public int EspIdCategoria { get; set; }
        public string EspTexto { get; set; }
        
    }
}
