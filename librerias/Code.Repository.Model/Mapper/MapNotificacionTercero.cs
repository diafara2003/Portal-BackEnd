using Code.Repository.Model.DTO.Licitaciones;
using Code.Repository.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Mapper
{
    public static class MapNotificacionTercero
    {
        public static NotificacionTerceroDTO MapToNotificacionTerceroDTO(this NotificacionTercero register)
        {
            return (new NotificacionTerceroDTO()
            {
                IdTercero = register.NTTercero,
                IdConstructora = register.NTConstructora,
                IdNotificacion = register.NTNotificacion,
                Estado = register.NTEstado
            });
        }

        public static NotificacionTercero MapToNotificacionTercero(this NotificacionTerceroDTO register)
        {
            return (new NotificacionTercero()
            {
                NTTercero = register.IdTercero,
                NTConstructora = register.IdConstructora,
                NTNotificacion = register.IdNotificacion,
                NTEstado = register.Estado
            });
        }

    }
}
