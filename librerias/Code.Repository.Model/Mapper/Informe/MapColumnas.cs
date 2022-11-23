
using Code.Repository.Model.DTO.Informe;
using Code.Repository.Model.Entity.inf;
using System.Collections.Generic;

namespace Code.Repository.Model.Mapper.Informe
{
    public static class MapColumnas
    {
        public static ColumnasDTO MapToDTO(this ColumnasInforme data)
        {

            return new ColumnasDTO()
            {

                align = data.ColAlign,
                key = data.ColNombreAPI,
                nombre = data.ColNombre,
                openLink = data.ColOpenLink,
                formatoNumerico = data.ColFormatoNumerico
            };
        }
    }
}
