

using Code.Repository.Model.DTO.Aprobaciones.Rangos;
using Code.Repository.Model.Entity.apr;
using Code.Repository.Model.Entity;
using System;

namespace Code.Repository.Model.Mapper.Aprobaciones
{
    public static class MapLogAprobaciones
    {
        public static UsuarioAprobacionDTO MapToDTO(this LogAprobaciones data, string nombre)
        {

            return new UsuarioAprobacionDTO()
            {
                id = data.LogIdUsuario,
                fecha = data.LogFecha,
                nombre = nombre
            };


        }

        public static LogAprobaciones MapToEntity(this UsuarioAprobacionDTO data)
        {

            return new LogAprobaciones()
            {
                LogFecha = DateTime.Now,
                LogContador = 0,
                LogIdUsuario = data.id

            };


        }




    }
}
