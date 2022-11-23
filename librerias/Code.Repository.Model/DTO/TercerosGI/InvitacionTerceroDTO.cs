

using Code.Repository.Model.DTO.Ciudades;
using Code.Repository.Model.DTO.TercerosIG;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Code.Repository.Model.DTO.TercerosGI
{
    public class InvitacionTerceroDTO
    {
        [Required]
        public string tipoPersona { get; set; }
        [Required]
        public string tipoDocumento { get; set; }
        [Required]
        public string documento { get; set; }
        [Required]
        public string correo { get; set; }
        [Required]
        public string nombre { get; set; }
        public string apellido { get; set; }
               
        public string ciudad { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public int? codigoVerificacion { get; set; }
       

    }

    public class DesasociarTercero
    {
        public string documento { get; set; }
    }


    public class RecordarClaveDTO
    {
        public string usuario { get; set; }

    }


    public class InvitarTerceroFromERP {

        public InvitacionTerceroDTO informacion { get; set; }
        public List<DatosContactoERPDTO> datosContacto { get; set; }
        public int estado { get; set; }
    }

    public class DatosContactoERPDTO
    {
        public string Nombre { get; set; }
        public string NumeroDocumento { get; set; }
        public string Cargo { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public int tipo { get; set; }
    }

    public class CambioClaveDTO
    {
        public string PassOld { get; set; }
        public string PassNew { get; set; }
        public string PassNewR { get; set; }
    }

    public class InvitacionTerceroRes
    {
        public Tuple<TerceroDTO, bool> TerceroInvitacion { get; set; }
        public AlternateView Template{ get; set; }
        public List<string> Mail { get; set; }
        public string Asunto { get; set; }
    }
}
