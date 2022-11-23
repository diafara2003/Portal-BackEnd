using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Aprobaciones.Rangos
{
    public class AprobarTerceroDTO
    {

        public int id { get; set; }
        public string comentarios { get; set; }
        public bool isAprobado { get; set; }
        public IList<MotivosRechazoDTO> motivoRechazo { get; set; }

    }

   
}
