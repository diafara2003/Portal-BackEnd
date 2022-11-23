using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Novedades
{
    public  class NovedadRegistroDTO
    {
        public int IdDocumento { get; set; }    
        public List<string> Correos { get; set; } 
        public string Asunto { get; set; }  
        public string Mensaje { get; set; }
    }
}
