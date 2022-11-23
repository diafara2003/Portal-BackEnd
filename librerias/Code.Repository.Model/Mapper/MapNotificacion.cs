using Code.Repository.Model.DTO.Licitaciones;
using Code.Repository.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Mapper
{
    public static class MapNotificacion
    {
        public static NotificacionDTO MapToNotificacionDTO(this Notificacion register)
        {
            return new NotificacionDTO()
            {
                IdConstructora = register.NLConstructora,
                IdNotificacion = register.NLId,
                IdLicitacion = register.NLLicitacion,
                Fecha = register.NLFecha,
                Asunto = register.NLAsunto,
                Mensaje = register.NLMensaje,
                Tipo = register.NLTipo
            };
        }


        public static Notificacion MapToNotificacion(this NotificacionDTO register)
        {
            return new Notificacion()
            {
                NLConstructora = register.IdConstructora,
                NLLicitacion = register.IdLicitacion,
                NLFecha = register.Fecha,
                NLMensaje = register.Mensaje,
                NLTipo = register.Tipo
            };
        }



    }
}
