using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Informe
{
    public class AuditoriaLoginDTO
    {
        public string nombre { get; set; }
        public string correo{ get; set; }
        public string fecha { get; set; }
        public string documento { get; set; }
        public int inicioCount { get; set; }
        public int totalPaginas { get; set; }
    }
}
