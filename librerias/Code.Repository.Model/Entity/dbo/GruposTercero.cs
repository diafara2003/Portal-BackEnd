
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity
{
    [Table("GruposTercero")]
    public class GruposTercero
    {
        [Key]
        public int GruId { get; set; }        
        public string GruTexto { get; set; }
        
    }
}
