using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity
{
    [Table("TerInformacionGeneral", Schema = "dbo")]
    public class TerInformacionGeneral
    {
        [Key]
        public int TigId { get; set; }
        public int TigTerceroId { get; set; }
        public string TigNombre { get; set; }
        public string TerApellido { get; set; }
        public string TigTipoPersona { get; set; }
        public string TigTipoDocumento { get; set; }
        public string TigNumeroIdentificacion { get; set; }
        public int? TerDigitoVerificacion { get; set; }
        public string TigCorreo { get; set; }
        public int TigCiudad { get; set; }
        public string TigDireccion { get; set; }
        public int TigActEconomicaPri { get; set; }
        public string TigTelefono { get; set; }
        public string TigPaginaWeb { get; set; }
        public bool TigCertificadoISO { get; set; }
        public byte[] TerLogo { get; set; }
        public string TigTipoEmpresa { get; set; }
    }
}
