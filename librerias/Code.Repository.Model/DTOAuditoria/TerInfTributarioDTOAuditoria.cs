using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTOAuditoria
{
    public class TerInfTributarioDTOAuditoria
    {
        public TerInfTributarioDTOAuditoria()
        {
            this.ResponsableIVA = string.Empty;
            this.Autorretenedor = string.Empty;
            this.Declarante = string.Empty;
            this.GranContribuyente = string.Empty;
            this.AutoRetenedorICA = string.Empty;
        }

        public string ResponsableIVA { get; set; }
        public string Autorretenedor { get; set; }
        public string Declarante { get; set; }
        public string GranContribuyente { get; set; }
        public string AutoRetenedorICA { get; set; }
    }
}
