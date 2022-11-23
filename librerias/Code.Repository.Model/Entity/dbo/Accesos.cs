using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity
{
    [Table("Acceso", Schema = "dbo")]
    public class Accesos
    {
        [Key]
        public int Id { get; set; }
        public int IdMenu { get; set; }
        public int IdNivel { get; set; }
    }
}
