using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Constructora;
using Code.Repository.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using Code.Repository.Model.Mapper;

namespace Code.Repository.RepositoryBL.Operations
{
    public class ConstucturasBL
    {
        public Constructora ObtenerConstructora(int id)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            return objcnn.constructoras.Where(c => c.ConstId == id).FirstOrDefault();
        }


        public Constructora GetContructoraKey(string key)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return (from c in objcnn.constructoras
                    where c.ConsApiKey.ToString().Equals(key)
                    select c
                    ).FirstOrDefault();

        }

        public IEnumerable<ConstructoraDTO> ObtenerConstructoraxTercero(int idTercero, string filter = "")

        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            if (filter != null) filter = filter.Replace("_", "");

            if (string.IsNullOrEmpty(filter))

                return (from _tercero in objcnn.terceroconstructora
                        join _contructora in objcnn.constructoras on _tercero.IdConstructora equals _contructora.ConstId
                        where _tercero.IdTercero == idTercero
                        //&& _tercero.Estado == 1
                        select _contructora.MapToDTO()
                        );

            else
                return (from _tercero in objcnn.terceroconstructora
                        join _contructora in objcnn.constructoras on _tercero.IdConstructora equals _contructora.ConstId
                        where _tercero.IdTercero == idTercero
                        && _contructora.ConstNombre.Contains(filter)
                        select _contructora.MapToDTO()
                        );
        }


        public string AsociarTercero(int idContrustora, int idTercero, EstadoTercero estado = EstadoTercero.Completada)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            objcnn.terceroconstructora.Add(new TercerosConstructora()
            {
                Estado = (int)estado,
                IdConstructora = idContrustora,
                IdTercero = idTercero,
                IdTerCons = 0
            });

            objcnn.SaveChanges();

            return objcnn.constructoras.Single(c => c.ConstId == idContrustora).ConsApiKey.ToString();
        }


        public void DesasociarTercero(int idContrustora, int idTercero)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var terceroConst = objcnn.terceroconstructora.FirstOrDefault(c => c.IdConstructora == idContrustora && c.IdTercero == idTercero);

            objcnn.Entry(terceroConst).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            objcnn.SaveChanges();


        }
    }
}
