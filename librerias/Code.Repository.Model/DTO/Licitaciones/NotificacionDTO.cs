using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Licitaciones
{
    public class NotificacionDTO
    {
        public int IdNotificacion { get; set; }
        public int IdConstructora { get; set; }
        public int IdLicitacion { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public int Tipo { get; set; }
        public int LicitacionNumero { get; set; }
    }
}
