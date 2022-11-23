using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity
{

    public enum EstadoTercero
    {
        DocumentacionCompleta = -4,
        DocumentacionPendiente = -3,
        Anulado = -2,
        Rechazado = -1,
        Aprobado = 1,
        Validado = 2,
        Completada = 3
    }


    [Table("Terceros", Schema = "dbo")]
    public class Terceros
    {
        [Key]
        public int Terid { get; set; }        
        public int TerEstado { get; set; }
        public bool TerEmailConfirmado { get; set; }
        public DateTime TerFechaRegistro { get; set; }
        public string TerRutaLogo { get; set; }
        public string TerNombreLogo { get; set; }
    }
}
