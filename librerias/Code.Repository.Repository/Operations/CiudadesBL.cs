using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Ciudades;
using Code.Repository.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using Code.Repository.Model.Mapper;

namespace Code.Repository.RepositoryBL.Operations
{
    public class CiudadesBL
    {

        public IEnumerable<CiudadesDTO> Get(string filter)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            IEnumerable<Ciudades> objLst = new List<Ciudades>();

            if (string.IsNullOrEmpty(filter) || filter == "_")
                objLst = objcnn.ciudad
                    .Take(ActividadEconomicaBL.maxRegisterAutoComplete);


            else
                objLst = objcnn.ciudad
                    .Where(c => c.CiuNombre.Contains(filter))
                    .Take(ActividadEconomicaBL.maxRegisterAutoComplete);



            return objLst
                     .ToList()
                    .MapToDTO();
        }
    }
}
