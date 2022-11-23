using Code.Repository.Model.DTO.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code.Repository.Model.Mapper.NotificacionPortal;
using Code.Repository.Model.Entity.dbo;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.DTO.Usuarios;
using Code.Repository.Model.Mapper;
using Code.Repository.Model.DTO.Abastecimiento.Cotizaciones;

namespace Code.Repository.RepositoryBL.Operations
{
    public class NotificacionesBL
    {

        public IEnumerable<ConsultarNotificacionDTO> GetNotificacion(TipoNotificaciones tipo, int tercero)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            try
            {
                var d = (from data in objcnn.datoContacto
                         join u in objcnn.usuario on data.NotIdUsuario equals u.UserId
                         //left join
                         join c in objcnn.constructoras on data.NotIdConstructora equals c.ConstId into co
                         from constr in co.DefaultIfEmpty()
                             //left join
                         join e in objcnn.categoriasTercero on data.NotIdCategoria equals e.CatId into es
                         from espe in es.DefaultIfEmpty()

                         where data.IdTercero == tercero
                         && data.NotTipo == (int)tipo
                         select data.MapToDTO(constr, espe, u)
                    );

                return d;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public IEnumerable<ConsultarNotificacionDTO> GetNotificacion(TipoNotificaciones tipo, string nit)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            try
            {
                var d = (from t in objcnn.terInfGeneral
                         join data in objcnn.datoContacto on t.TigTerceroId equals data.IdTercero
                         join u in objcnn.usuario on data.NotIdUsuario equals u.UserId
                         //left join
                         join c in objcnn.constructoras on data.NotIdConstructora equals c.ConstId into co
                         from constr in co.DefaultIfEmpty()
                             //left join
                         join e in objcnn.categoriasTercero on data.NotIdCategoria equals e.CatId into es
                         from espe in es.DefaultIfEmpty()

                         where t.TigNumeroIdentificacion == nit
                         && data.NotTipo == (int)tipo
                         select data.MapToDTO(constr, espe, u)
                    );

                return d;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        ResponseDTO validacionnotificacion(NotificacionDTO data, TipoNotificaciones tipo, int proveedor)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            if (tipo == TipoNotificaciones.Proveddores)
            {

                //se valida que el usuario no este registrado
                if (objcnn.datoContacto.Count(c => c.IdTercero == data.usuario && c.IdTercero == proveedor && c.NotTipo == (int)tipo) > 0)
                {
                    return new ResponseDTO()
                    {
                        codigo = -1,
                        Success = false,
                        mensaje = "El usuario ya se encuentra registrado para notificaciones."
                    };
                }
            }

            else
            {


            }


            return new ResponseDTO();
        }
        public Tuple<ResponseDTO, ConsultarNotificacionDTO> AgregarNotificacion(NotificacionDTO data, TipoNotificaciones tipo, int proveedor)
        {

            Tuple<ResponseDTO, ConsultarNotificacionDTO> objResponse;

            ResponseDTO objREsultado = validacionnotificacion(data, tipo, proveedor);


            if (!objREsultado.Success)
            {
                objResponse = new Tuple<ResponseDTO, ConsultarNotificacionDTO>(objREsultado, new ConsultarNotificacionDTO());

                return objResponse;

            }

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var _notificacion = data.MapToEntity(tipo);

            _notificacion.IdTercero = proveedor;

            objcnn.datoContacto.Add(_notificacion);

            objcnn.SaveChanges();

            objREsultado.codigo = _notificacion.NotId;

            var d = (from n in objcnn.datoContacto
                     join u in objcnn.usuario on n.NotIdUsuario equals u.UserId
                     //left join
                     join c in objcnn.constructoras on n.NotIdConstructora equals c.ConstId into co
                     from constr in co.DefaultIfEmpty()
                         //left join
                     join e in objcnn.categoriasTercero on n.NotIdCategoria equals e.CatId into es
                     from espe in es.DefaultIfEmpty()

                     where n.NotId == _notificacion.NotId
                     select n.MapToDTO(constr, espe, u)
                  ).FirstOrDefault();

            objResponse = new Tuple<ResponseDTO, ConsultarNotificacionDTO>(objREsultado, d);

            return objResponse;
        }

        public ResponseDTO EliminarNotificacion(int id, int idusuario)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var _contacto = objcnn.datoContacto.Find(id);

            objcnn.Entry(_contacto).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            objcnn.SaveChanges();

            return new ResponseDTO();
        }

        public IEnumerable<UsuarioDTO> GetUserNotificacion(TipoNotificaciones tipo, int proveedor)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            return (from n in objcnn.datoContacto
                    join u in objcnn.usuario on n.NotIdUsuario equals u.UserId
                    where u.UserIdPpal == proveedor && u.UserEstado == 1
                    && n.NotTipo == (int)tipo
                    select u.MapToDTO()
                    );

        }

        public IEnumerable<NotificacionesDTO> getNotificaciones(int idConstructora, int idLicitacion, int usuario)
        {
            try
            {
                ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
                var data = (from nov in objcnn.novedades
                            join novU in objcnn.novedadesUsuarios on nov.NovId equals novU.NUNovedadId
                            where nov.NovIdConstructora == idConstructora && nov.NovLogAprid == idLicitacion && novU.NUUsuarioId == usuario

                            select nov.MapNotificacionesDTO(novU)
                        ).ToList();

                return data;



            }
            catch (Exception e)
            {
                throw;
            }

        }

    }
}
