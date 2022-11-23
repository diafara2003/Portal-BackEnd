

using Code.Repository.Model.DTO.Aprobaciones.Rangos;
using Code.Repository.Model.Entity.apr;
using Code.Repository.Model.Entity;

namespace Code.Repository.Model.Mapper.Aprobaciones
{
    public static class MapAprobaciones
    {
        public static RangoTerceroDTO MapToDTO(this RangoAprobacion data, string userName)
        {

            return new RangoTerceroDTO()
            {
                id = data.RAId,
                idUsuario = data.RAIdTipo,
                tipo = data.RAprTipo,
                nombreUsuario = userName
            };


        }

        public static RangoAprobacion MapToEntity(this RangoTerceroDTO data)
        {

            return new RangoAprobacion()
            {
                RAId = data.id,
                RAIdTipo = data.idUsuario,
                RAprTipo = data.tipo
            };


        }




    }
}
