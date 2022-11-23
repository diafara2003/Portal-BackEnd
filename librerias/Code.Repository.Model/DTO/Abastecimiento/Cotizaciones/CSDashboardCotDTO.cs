using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Abastecimiento.Cotizaciones
{
    public class CSDashboardCotDTO
    {
        public int CantInvitaciones { get; set; }
        public int CantDeclinadas { get; set; }
        public int CantEnviadas { get; set; }
        public int CantAdjudicadas { get; set; }
        public int CantPendientes { get; set; }
        public int CantCotizadas { get; set; }
        public decimal VrCotizado { get; set; }
        public decimal VrAdjudicado { get; set; }
        public decimal PorcEfectividad { get; set; }

    }
}
