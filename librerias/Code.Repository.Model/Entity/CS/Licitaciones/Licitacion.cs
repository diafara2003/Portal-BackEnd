using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity.CS.Licitaciones
{
    [Table("Licitaciones", Schema = "CS")]
    public class Licitacion
    {
        [Key]
        public int LicId { get; set; }
        public int LicConstructoraId { get; set; }
        public int LicNumero { get; set; }
        public int LicFechaCreacion { get; set; }
        public int LicFechaVigencia { get; set; }
        public int LicAsunto { get; set; }
        public int LicDescripcion { get; set; }
        public int LicEstado { get; set; }
        public int LicTipo { get; set; }
        public int LicClase { get; set; }
    }
}
