using Code.Repository.Model.DTO.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code.Repository.Model.Mapper.NotificacionPortal;
using Code.Repository.Model.Mapper.Auditoria;
using Code.Repository.Model.Entity.dbo;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.DTO.Usuarios;
using Code.Repository.Model.Mapper;
using Code.Repository.Model.DTO.Abastecimiento.Cotizaciones;
using Code.Repository.RepositoryBL.Operations.Auditoria;
using Code.Repository.Model.MappAuditoria;
using Code.Repository.Model.DTOAuditoria;
using static Code.Repository.EntityFramework.Context.ApplicationDatabaseContext;

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
           
                //se valida que el usuario no este registrado
                if (objcnn.datoContacto.Count(c => c.NotIdUsuario == data.usuario && c.IdTercero == proveedor && c.NotTipo == (int)tipo) > 0)
                {
                    return new ResponseDTO()
                    {
                        codigo = -1,
                        Success = false,
                        mensaje = "El usuario ya se encuentra registrado para notificaciones."
                    };
                }
           


            return new ResponseDTO();
        }
        public Tuple<ResponseDTO, ConsultarNotificacionDTO> AgregarNotificacion(NotificacionDTO data, TipoNotificaciones tipo, int proveedor, int _idUser)
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
            var tipoAudit = tipo == TipoNotificaciones.Proveddores ? (int)TipoAuditoria.DatosNotificacionesProveedor : (int)TipoAuditoria.DatosNotificacionesLicitaciones;

            _notificacion.IdTercero = proveedor;

            objcnn.datoContacto.Add(_notificacion);

            objcnn.SaveChanges();
  
            if (TipoNotificaciones.Proveddores == tipo)
            {               
                var _userNotificacion = (from us in objcnn.usuario
                                         join no in objcnn.datoContacto on us.UserId equals no.NotIdUsuario
                                         where no.NotId == _notificacion.NotId
                                         select us.MapToAuditoriaNotiProve()).FirstOrDefault();

                Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(_userNotificacion, new TerNotificacionProveedorDTOAuditoria() { });
                objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, _idUser, proveedor, tipoAudit, false, true, Opcion: "Datos notificaciones proveedores");
            }
            else
            {               
                var _userNotificacionLi = (from us in objcnn.usuario
                                           join no in objcnn.datoContacto on us.UserId equals no.NotIdUsuario 
                                           join cons in objcnn.constructoras on no.NotIdConstructora equals cons.ConstId into constLeft from constComplete in constLeft.DefaultIfEmpty()
                                           join ct in objcnn.categoriasTercero on no.NotIdCategoria equals ct.CatId into ctLetf from cateComplete in ctLetf.DefaultIfEmpty()
                                           where no.NotId == _notificacion.NotId 
                                           select us.MapToAuditoriaNotiLici(cateComplete, constComplete, no)).FirstOrDefault();

                Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(_userNotificacionLi, new TerNotificacionLicitacionDTOAuditoria() { });
                objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, _idUser, proveedor, tipoAudit, false, true, Opcion: "Datos notificaciones licitaciones");
            }
           

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

        public ResponseDTO EliminarNotificacion(int id,  int proveedor, int _idUser)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var _contacto = objcnn.datoContacto.Find(id);
            objcnn.Entry(_contacto).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            var tipoAudit = _contacto.NotTipo == (int)TipoNotificaciones.Proveddores ? (int)TipoAuditoria.DatosNotificacionesProveedor : (int)TipoAuditoria.DatosNotificacionesLicitaciones;


            if ((int)TipoNotificaciones.Proveddores == _contacto.NotTipo )
            {
                var _userNotificacion = (from us in objcnn.usuario
                                         join no in objcnn.datoContacto on us.UserId equals no.NotIdUsuario
                                         where no.NotId == id
                                         select us.MapToAuditoriaNotiProve()).FirstOrDefault();

                Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(_userNotificacion, new TerNotificacionProveedorDTOAuditoria() { });
                objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, _idUser, proveedor, tipoAudit, true, false, Opcion: "Datos notificaciones proveedores");
            }
            else
            {
                var _userNotificacionLi = (from us in objcnn.usuario
                                           join no in objcnn.datoContacto on us.UserId equals no.NotIdUsuario
                                           join cons in objcnn.constructoras on no.NotIdConstructora equals cons.ConstId  into constLeft from constComplete in constLeft.DefaultIfEmpty()
                                           join ct in objcnn.categoriasTercero on no.NotIdCategoria equals ct.CatId into ctLetf from cateComplete in ctLetf.DefaultIfEmpty()
                                           where no.NotId == id
                                           select us.MapToAuditoriaNotiLici(cateComplete, constComplete, no)).FirstOrDefault();

                Tuple<string, string> _datos = new AuditoriaBL().diferenciasAudit(_userNotificacionLi, new TerNotificacionLicitacionDTOAuditoria() { });
                objcnn.SaveChangesAuditoria(_datos.Item1, _datos.Item2, _idUser, proveedor, tipoAudit, true, false, Opcion: "Datos notificaciones licitaciones");
            }

            

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
