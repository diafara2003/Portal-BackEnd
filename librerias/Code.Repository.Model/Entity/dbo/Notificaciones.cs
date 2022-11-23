using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Code.Repository.Model.Entity.dbo
{

    public enum TipoNotificaciones
    {
        Proveddores = 1,
        Licitaciones = 2
    }

    [Table("Notificaciones")]
    public class Notificaciones
    {
        [Key]
        public int NotId { get; set; }
        public int IdTercero { get; set; }
        public int NotIdUsuario { get; set; }
        public int? NotIdCategoria { get; set; }
        public string NotZona { get; set; }
        public int? NotIdConstructora { get; set; }
        public int NotTipo { get; set; }
        
    }
}
