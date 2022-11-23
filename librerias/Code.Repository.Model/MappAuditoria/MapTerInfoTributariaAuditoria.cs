using Code.Repository.Model.DTOAuditoria;
using Code.Repository.Model.Entity.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.MappAuditoria
{
    public static class MapTerInfoTributariaAuditoria
    {
        public static TerInfTributarioDTOAuditoria MapToAuditoria(this TerInfoTributaria data)
        {
            return new TerInfTributarioDTOAuditoria()
            {
                AutoRetenedorICA = data.AutoRetenedorICA ? "Si" : "No",
                Autorretenedor = data.Autorretenedor ? "Si" : "No",
                Declarante = data.Declarante ? "Si" : "No",
                GranContribuyente = data.GranContribuyente ? "Si" : "No",
                ResponsableIVA = data.ResponsableIVA ? "Si" : "No",
            };
        }
        
    }
}
