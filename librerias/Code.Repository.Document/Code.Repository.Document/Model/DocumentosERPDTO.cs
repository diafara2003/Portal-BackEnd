using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Document.Model
{
    public class DocumentosRequeridosERPDTO
    {
        public int especialidad { get; set; }
        public int tipoAdjunto { get; set; }
        public string nombreEspecialidad { get; set; }
    }

    public class ResponseERPDocReqDTO
    {
        public int especialidad { get; set; }
        public int tipoAdjunto { get; set; }
        public int tipo { get; set; }
    }

}
