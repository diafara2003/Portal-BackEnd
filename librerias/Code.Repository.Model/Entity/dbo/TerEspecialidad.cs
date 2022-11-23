using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity
{
    [Table("TerEspecialidad")]
    public class TerEspecialidad
    {
        [Key]
        public int id { get; set; }
        public int TerId { get; set; }
        public int EspId { get; set; }
    }
}
