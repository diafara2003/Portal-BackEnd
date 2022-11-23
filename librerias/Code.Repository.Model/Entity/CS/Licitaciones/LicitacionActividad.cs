using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity.CS.Licitaciones
{
    [Table("LicitacionesActividades", Schema = "CS")]
    public class LicitacionActividad
    {
        [Key]
        public int LAId { get; set; }
        public int LALicitacionId { get; set; }
        public string LACodigo { get; set; }
        public string LADescripcion { get; set; }
        public string LAAlcance { get; set; }
        public string LAUM { get; set; }
        public string LAGrupo { get; set; }
        public decimal LaCant { get; set; }
        public int LATipo { get; set; }
        public int LAObjetivo { get; set; }



    }
}
