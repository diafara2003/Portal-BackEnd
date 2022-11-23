using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Licitaciones
{
    public class NotificacionTerceroDTO
    {
        public int IdConstructora { get; set; }
        public int IdNotificacion { get; set; }
        public int IdTercero { get; set; }
        public int NitProveedor { get; set; }
        public bool Estado { get; set; }
    }
}
