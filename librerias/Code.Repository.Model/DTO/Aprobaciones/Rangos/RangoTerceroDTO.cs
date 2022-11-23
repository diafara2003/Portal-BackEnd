using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Aprobaciones.Rangos
{
    public class RangoTerceroDTO
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string tipo { get; set; }
    }

    public class AgregarRangoTerceroDTO {
        public int id { get; set; }
        public bool isPerfil { get; set; }
    }



    public class AProbacionRangoDTO {

        public string nombrePerfil { get; set; }
        public UsuarioAprobacionDTO aprobacion { get; set; }
        public UsuarioAprobacionDTO rechazo { get; set; }

    }

    public class UsuarioAprobacionDTO {

        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime fecha { get; set; }

    }
}
