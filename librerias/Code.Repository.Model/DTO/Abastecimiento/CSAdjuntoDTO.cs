using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento
{
    public class CSAdjuntoDTO
    {
        public int IdAdjunto { get; set; }
        public int IdDocumento { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Observacion { get; set; }
    }
}
