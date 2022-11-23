using Code.Repository.Model.DTO.TercerosIG;
using Code.Repository.Model.Entity;
using System.Collections.Generic;

namespace Code.Repository.Model.Mapper
{
    public static class MapActividadEconomica
    {


        public static IEnumerable<ActividadEconomicaDTO> MaptoDTO(this List<ActividadEconomica> data)
        {
            List<ActividadEconomicaDTO> objLst = new List<ActividadEconomicaDTO>();

            data.ForEach(c => objLst.Add(c.MaptoDTO()));

            return objLst;


        }

        public static ActividadEconomica MaptoEntity(this ActividadEconomicaDTO data)
        {
            return new ActividadEconomica()
            {
                ActEcCodigo = data.codigo,
                ActEcId = data.id,
                ActECtexto = data.nombre
            };
        }


        public static ActividadEconomicaDTO MaptoDTO(this ActividadEconomica data)
        {
            if (data == null) data = new ActividadEconomica()
            {
                ActEcId = 0,
                ActECtexto = ""
            };


            return new ActividadEconomicaDTO()
            {
                codigo = data.ActEcCodigo,
                nombre = data.ActECtexto,
                id = data.ActEcId
            };
        }
    }
}
