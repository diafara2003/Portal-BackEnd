using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity
{
    [Table("Ciudades")]
    public class Ciudades
    {
        [Key]
        public int CiudID { get; set; }
        public string CiuCodigo { get; set; }
        public string CiuNombre { get; set; }

    }
}
