
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity
{
    [Table("CategoriasTercero")]
    public class CategoriasTercero
    {
        [Key]
        public int CatId { get; set; }
        public int CatIdGrupo { get; set; }
        public string CatTexto { get; set; }
        
    }
}
