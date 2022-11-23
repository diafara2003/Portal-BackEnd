using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Informe
{
    public class PaginasVisitadasDTO
    {
        public string usuario { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string pagina { get; set; }
        public int visitas { get; set; }
        public byte orden { get; set; }
        public int totalRegistros { get; set; }

    }
}
