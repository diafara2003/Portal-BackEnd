
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity
{
    [Table("Adjuntos")]
    public class Adjuntos
    {
        [Key]
        public int AjdId { get; set; }
        public string Adjruta { get; set; }
        public string AjdNombre { get; set; }
        public string AjdExtension { get; set; }
        public string AjdTipo { get; set; }
        public int AjdIdUsuario { get; set; }
        public DateTime AjdFechaCreacion { get; set; }
    }
}
