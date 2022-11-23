using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTOAuditoria
{
    public class TerNotificacionProveedorDTOAuditoria
    {
        public TerNotificacionProveedorDTOAuditoria()
        {
            this.Nombres = string.Empty;
            this.Cargo = string.Empty;
            this.Correo = string.Empty;
            this.Celular = string.Empty;            
        }

        public string Nombres { get; set; }
        public string Cargo { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public int idnot { get; set; }
    }
}
