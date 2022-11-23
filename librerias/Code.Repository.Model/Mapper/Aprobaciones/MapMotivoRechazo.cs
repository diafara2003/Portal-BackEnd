

using Code.Repository.Model.DTO.Aprobaciones.Rangos;
using Code.Repository.Model.Entity.apr;
using System.Collections.Generic;

namespace Code.Repository.Model.Mapper.Aprobaciones
{
    public static class MapMotivoRechazo
    {

        public static IEnumerable<MotivosRechazoDTO> MapToDTO(this List<MotivoRechazo> data)
        {

            List<MotivosRechazoDTO> obj = new List<MotivosRechazoDTO>();

            data.ForEach(c => obj.Add(c.MapToDTO()));

            return obj;


        }


        public static MotivosRechazoDTO MapToDTO(this MotivoRechazo data)
        {

            return new MotivosRechazoDTO()
            {
                id = data.id,
                texto = data.MotTexto

            };


        }

        public static MotivoRechazo MapToEntity(this MotivosRechazoDTO data)
        {

            return new MotivoRechazo()
            {
                id = data.id,
                MotTexto = data.texto,
                MotCodigo = ""
            };


        }
    }
}
