using Code.Repository.Model.DTO.Abastecimiento.Cotizaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.RepositoryBL.Operations.Abastecimiento.Mapper
{
    public static class MapDashboard
    {
        public static CSDashboardCotDTO MapToDashboardCotDTO(this DataTable data)
        {

            return (from d in data.AsEnumerable()
                    select new CSDashboardCotDTO()
                    {
                        CantAdjudicadas = (int)d["CantAdjudicadas"],
                        CantCotizadas = (int)d["CantCotizadas"],
                        CantDeclinadas = (int)d["CantDeclinadas"],
                        CantEnviadas = (int)d["CantEnviadas"],
                        CantInvitaciones = (int)d["CantInvitaciones"],
                        CantPendientes = (int)d["CantPendientes"],
                        PorcEfectividad = (decimal)d["PorcEfectividad"],
                        VrAdjudicado = (decimal)d["VrAdjudicado"],
                        VrCotizado = (decimal)d["VrCotizado"],

                    }).FirstOrDefault();
        }
    }
}
