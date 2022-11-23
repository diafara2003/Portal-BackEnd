using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity
{
    [Table("NotificacionesLicitacion", Schema = "ADP_CS")]
    public class Notificacion
    {
        [Key]
        public int NLId { get; set; }
        public int NLConstructora { get; set; }
        public int NLLicitacion { get; set; }
        public int NLTipo { get; set; }
        public string NLAsunto { get; set; }
        public string NLMensaje { get; set; }
        public DateTime NLFecha { get; set; }

    }
}
