using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity.dbo
{
    [Table("Novedades", Schema = "dbo")]
    public class Novedades
    {
        [Key]
        public int NovId { get; set; }
        public int NovIdTercero { get; set; }
        public int NovIdConstructora { get; set; }
        public string NovObservaciones { get; set; }
        public DateTime NovFecha { get; set; }
        public int NovEstado { get; set; }
        public int NovVisto { get; set; }
        public int NovLogAprid { get; set; }
        public int NovTipo { get; set; }
        public string NovAsunto { get; set; }
    }
}
