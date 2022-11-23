using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Code.Repository.Model.Entity
{
    [Table("Navegacion", Schema = "menu")]
    public class Navegacion
    {
        [Key]
        public int NavId { get; set; }
        public int NavUsu { get; set; }
        public int NavPagina { get; set; }
        public DateTime NavFecha { get; set; }
        public string NavIP { get; set; }        
    }
}
