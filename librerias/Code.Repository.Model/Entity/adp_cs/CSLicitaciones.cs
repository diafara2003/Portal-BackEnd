using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity
{
    [Table("Licitaciones", Schema = "ADP_CS")]
    public class CSLicitacion
    {
        [Key]
        public int LicId { get; set; }
        public int LicConstructora { get; set; }
        public int LicLicitacion { get; set; }
        public int LicNumero { get; set; }
        public DateTime LicFecha { get; set; }
        public DateTime LicFechaCierre { get; set; }
        public string LicAsunto { get; set; }
        public int LicCategoria { get; set; }
        public decimal LicVrEstimado { get; set; }
        public int LicCantActividades { get; set; }
        public int LicEstado { get; set; }
        public string LicCiudad { get; set; }
        public string LicProyecto { get; set; }

    }
}
