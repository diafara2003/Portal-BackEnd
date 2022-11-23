using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento.Cotizaciones
{
    public class NotificacionesDTO
    {
        public bool Visto { get; set; }
        public string Asunto { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }

        public object ToList()
        {
            throw new NotImplementedException();
        }
    }
}
