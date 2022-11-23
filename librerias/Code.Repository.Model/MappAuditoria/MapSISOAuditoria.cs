using Code.Repository.Model.DTOAuditoria;
using Code.Repository.Model.Entity.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.MappAuditoria
{
    public static class MapSISOAuditoria
    {
        public static TerSISODTOAuditoria MapToAuditoria(this TerSISO data)
        {

            return new TerSISODTOAuditoria()
            {
                ProgramaSaludOcupacional = data.ProgramaSaludOcupacional,
                ProgramaFactoresRiesgo = data.ProgramaFactoresRiesgo,
                TieneComiteSO = data.TieneComiteSO,
                ProgramaSeguridadEhigiene = data.ProgramaSeguridadEhigiene,
                ProgramaAmbiental = data.ProgramaAmbiental,
            };

        }
    }
}
