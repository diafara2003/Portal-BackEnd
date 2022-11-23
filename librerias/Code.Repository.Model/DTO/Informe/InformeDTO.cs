
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Code.Repository.Model.DTO.Informe
{

    public class InformeDTO {


        public IEnumerable<ColumnasDTO> encabezado { get; set; }
        public dynamic detalles { get; set; }
    }
    public class ColumnasDTO
    {
        public string nombre { get; set; }
        public string key { get; set; }
        public string align { get; set; }
        public bool openLink { get; set; }
        public bool formatoNumerico { get; set; }
    }

    
}
