using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Constructora
{
  public  class ConstructoraDTO
    {
        public int id { get; set; }
        public string NIT { get; set; }
        public string urlLogo { get; set; }
        public string nombre { get; set; }
        public string baseURL { get; set; }
    }

    public class ConstructoraNotificacionDTO {
        public int id { get; set; }
        public string nombre { get; set; }
    }
}
