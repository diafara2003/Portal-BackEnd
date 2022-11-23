using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity
{
    [Table("Categorias", Schema = "dbo")]
    public class Categoria
    {
        [Key]
        public int CatId { get; set; }
        public string CatDesc { get; set; }
        public bool CatEstado { get; set; }

    }
}
