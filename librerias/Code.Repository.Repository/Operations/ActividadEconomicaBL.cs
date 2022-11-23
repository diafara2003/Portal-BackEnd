using Code.Repository.Model.Mapper;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.TercerosIG;
using System.Collections.Generic;
using System.Linq;
using Code.Repository.Model.Entity;

namespace Code.Repository.RepositoryBL.Operations
{
    public class ActividadEconomicaBL
    {


        public static readonly int maxRegisterAutoComplete = 20;
        public IEnumerable<ActividadEconomicaDTO> Get(string filter)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();



            IEnumerable<ActividadEconomica> objLst = new List<ActividadEconomica>();

            if (string.IsNullOrEmpty(filter) || filter == "_")
                objLst = objcnn.actividadEconomica
                    .Take(maxRegisterAutoComplete);


            else
                objLst = objcnn.actividadEconomica
                    .Where(c => c.ActECtexto.Contains(filter))
                    .Take(maxRegisterAutoComplete);



            return objLst
                     .ToList()
                    .MaptoDTO();
        }
    }
}
