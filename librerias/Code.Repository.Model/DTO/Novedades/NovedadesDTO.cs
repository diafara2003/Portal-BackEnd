using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Novedades
{
    public class NovedadesDTO
    {
        public string asunto { get; set; }
        public string comentario { get; set; }
        public int tercero { get; set; }
        public DateTime fecha { get; set; }
        public List<NovedadesDetDTO> detalle { get; set; }

        public int Visto { get; set; }

        public int estadoTercero { get; set; }
        public int numero { get; set; }
        public string constructora { get; set; }
        public bool IsActiva { get; set; }

        public int IdConstructora { get; set; }
        public int Tipo { get; set; }


    }
    public class NovedadesDetDTO
    {

        public int tipoDocumento { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
    }

    public class CambiarEstadoNovedadDTO {

        public int codigo { get; set; }
        
    }

    public class NovedadesConstructoraDTO
    {
        public int contNotificaciones { get; set; }
        public string nombreConst { get; set; }
        public string logoConst { get; set; }        
        public int ConstructoraId { get; set; }
    }
    public class CambiarVistoNovedadDTO
    {
        public int idConstrcutora { get; set; }

    }
}
