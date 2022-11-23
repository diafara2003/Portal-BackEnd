using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity.dbo
{
    [Table("TipoContactoTercero", Schema = "dbo")]
    public class TipoContactoTercero
    {
        [Key]
        public int Id { get; set; }
        public string Texto { get; set; }
        public bool Estado { get; set; }
        public int Orden { get; set; }        

    }
   
}
