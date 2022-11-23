using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Monedas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code.Repository.Model.Mapper;

namespace Code.Repository.RepositoryBL.Operations
{
    public class MonedaBL
    {
        public IEnumerable<MonedaDTO> ConsultarMonedas()
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            return (from moneda in objcnn.monedas
                    select moneda.MapToDTO());
        }
    }
}
