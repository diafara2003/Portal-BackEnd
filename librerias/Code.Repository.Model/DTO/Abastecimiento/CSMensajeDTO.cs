using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento
{
    public class CSMensajeDTO
    {
        public int IdConstructora { get; set; }
        public int IdLicitacion { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Fecha { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public int Tipo { get; set; }
        public string Origen { get; set; }

    }
}
