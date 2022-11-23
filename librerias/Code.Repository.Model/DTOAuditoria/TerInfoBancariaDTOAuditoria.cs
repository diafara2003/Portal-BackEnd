using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTOAuditoria
{
    public class TerInfoBancariaDTOAuditoria
    {

        public TerInfoBancariaDTOAuditoria()
        {
            this.bancoTexto=string.Empty;
            this.tipoCuentaTexto = string.Empty;
            this.numero = string.Empty;
            this.ciudadTexto = string.Empty;
            this.correoPagos = string.Empty;
        }

        public string bancoTexto { get; set; }
        public string tipoCuentaTexto { get; set; }
        public string numero { get; set; }
        public string ciudadTexto { get; set; }
        public string correoPagos { get; set; }
    }
}
