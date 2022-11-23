using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTOAuditoria
{
    public class TerSISODTOAuditoria
    {
        
        public bool ProgramaSaludOcupacional { get; set; }
        public bool ProgramaFactoresRiesgo { get; set; }
        public bool TieneComiteSO { get; set; }
        public bool ProgramaSeguridadEhigiene { get; set; }
        public bool ProgramaAmbiental { get; set; }
    }
}
