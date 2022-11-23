using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Abastecimiento.Cotizaciones;
using Code.Repository.Model.DTO.Licitaciones;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Mapper;
using Code.Repository.Model.Mapper.NotificacionPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.RepositoryBL.Operations.Licitaciones
{
    public class NotificacionesBL
    {
        public IEnumerable<NotificacionDTO> ConsultarNotificaciones(int proveedor, int empresa)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            var notificaTer_ = objcnn.notificacionesTercero.Where(z => z.NTConstructora.Equals(empresa)).Where(x => x.NTTercero.Equals(proveedor));
            var notifica_ = objcnn.notificaciones.Where(z => z.NLConstructora.Equals(empresa));
            var Licita_ = objcnn.licitaciones.Where(z => z.LicConstructora.Equals(empresa));
            var _data = (from a in notificaTer_
                         join b in notifica_ on a.NTNotificacion equals b.NLId
                         select new NotificacionDTO()
                         {
                             IdLicitacion = b.NLLicitacion,
                             Fecha = b.NLFecha,
                             Asunto = b.NLAsunto,
                             Mensaje = b.NLMensaje,
                             IdNotificacion = b.NLId,
                             Estado = a.NTEstado,
                             LicitacionNumero = Licita_.Where(x => x.LicId == b.NLLicitacion).FirstOrDefault().LicNumero
                         }).ToList();


            return _data;
        }

        public NotificacionDTO ConsultarNotificacion(int idNotificacion)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            return objcnn.notificaciones.Where(c => c.NLId == idNotificacion).FirstOrDefault().MapToNotificacionDTO();
        }
        public ResponseDTO GuardarNotificacion(NotificacionDTO data)
        {
            ResponseDTO respuesta = new ResponseDTO();
            try
            {
                ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
                Notificacion objNew_ = data.MapToNotificacion();
                objcnn.notificaciones.Add(objNew_);
                objcnn.SaveChanges();
                respuesta.codigo = 0;
                respuesta.mensaje = "Se guardó correctamente";
                respuesta.Success = true;
            }
            catch (Exception e)
            {
                respuesta.codigo = 1;
                respuesta.mensaje = e.Message;
                respuesta.Success = false;
            }

            return respuesta;
        }

    }
}
