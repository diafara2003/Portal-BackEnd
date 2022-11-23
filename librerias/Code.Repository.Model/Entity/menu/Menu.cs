using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Code.Repository.Model.Entity
{
    [Table("Menu", Schema ="menu")]
    public class Menu
    {
        [Key]
        public int IdMenu { get; set; }
        public string Mencodigo { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public bool ActMenu { get; set; }
        public string PagAyuda { get; set; }
        public string SVG { get; set; }
        public bool MenRequiereProyecto { get; set; }
    }
}
