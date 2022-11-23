
using Code.Repository.Model.DTO.TercerosGI;
using Code.Repository.Model.Entity;
using System.Collections.Generic;

namespace Code.Repository.Model.Mapper
{
    public static class MapCamaraComercio
    {
        public static IEnumerable<TerCamaraComercioDTO> MaptoDTO(this List<TerCamaraComercio> data)
        {
            List<TerCamaraComercioDTO> objLst = new List<TerCamaraComercioDTO>();

            data.ForEach(c => objLst.Add(c.MapToDTO()));

            return objLst;


        }

        public static TerCamaraComercio MapToEntity(this TerCamaraComercioDTO data)
        {


            return new TerCamaraComercio()
            {
                TerCamId = data.id,
                TerCamCargo = data.cargo,
                TerCamDocumento = data.documento,
                TerCamTipoDoc = data.tipoDocumento,
                TerCapNombre = data.nombre
            };

        }

        public static TerCamaraComercioDTO MapToDTO(this TerCamaraComercio data)
        {

            return new TerCamaraComercioDTO()
            {
                cargo = data.TerCamCargo,
                nombre = data.TerCapNombre,
                tipoDocumento = data.TerCamTipoDoc,
                documento = data.TerCamDocumento,
                id = data.TerCamId
            };
        }
    }
}
