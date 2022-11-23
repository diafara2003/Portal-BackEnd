using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity.dbo
{
    [Table("Monedas", Schema = "dbo")]
    public class Monedas
    {
        [Key]
        public int MonId { get; set; }
        public string MonDescripcion { get; set; }
        public string MonAbreviacion { get; set; }
        public bool MonFuncional { get; set; }
        public decimal MonTasaCambio { get; set; }
    }
}
