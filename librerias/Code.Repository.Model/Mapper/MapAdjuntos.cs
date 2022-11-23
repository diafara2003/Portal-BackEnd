using Code.Repository.Model.DTO.Adjuntos;
using Code.Repository.Model.Entity;

namespace Code.Repository.Model.Mapper
{
    public static class MapAdjuntos
    {

        public static AdjuntosDTO MapToDTO(this Adjuntos data)
        {

            return new AdjuntosDTO()
            {

                extension = data.AjdExtension,
                id = data.AjdId,
                nombre = data.AjdNombre
            };


        }



        public static TipoAdjuntoTerceroDTO MapToDTO(this TipoAdjuntoTercero data) {
            return new TipoAdjuntoTerceroDTO() {
                id = data.TipAdjId,
                nombre = data.TipAdjTexto
            };
        }
    }
}
