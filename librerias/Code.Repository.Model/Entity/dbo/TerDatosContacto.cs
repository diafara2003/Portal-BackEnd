using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity
{

    public enum TipoContacto
    {
        RepresentanteLegal = 1,
        ResponsablePedidos = 2,
        ResponsableContratos = 3,
        ResponsableActasFacturacion = 4,
        Comercial = 5,
        Cartera = 6,

    }


    [Table("TerDatosContacto", Schema = "dbo")]
    public class TerDatosContacto
    {
        [Key]
        public int TdcId { get; set; }
        public int TdcTerceroId { get; set; }
        public int TdcTipoContactoId { get; set; }
        public string TdcNombre { get; set; }
        public string TdcNumDocumento { get; set; }
        public string TdcCargo { get; set; }
        public string TdcCorreo { get; set; }
        public string TdcDireccion { get; set; }
        public int TdcCiudad { get; set; }
        public string TdcTelefono { get; set; }
        public string TdcCelular { get; set; }

    }
}
