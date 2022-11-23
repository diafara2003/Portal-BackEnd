using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTOAuditoria
{
    public class UsuarioDTOAuditoria
    {

        public UsuarioDTOAuditoria()
        {
            this.UserNombre = string.Empty;
            this.UserCorreo = string.Empty;
            this.UserCelular = string.Empty;
            this.UserDoc = string.Empty;
            this.UserCargo = string.Empty;
            this.UserEstado = 1;
        }

        public string UserCorreo { get; set; }
        public int UserEstado { get; set; }
        public string UserNombre { get; set; }
        public string UserDoc { get; set; }
        public string UserCargo { get; set; }
        public string UserCelular { get; set; }
    }
}
