

using Code.Repository.Model.DTO.Ciudades;
using Code.Repository.Model.Entity;
using System.Collections.Generic;

namespace Code.Repository.Model.Mapper
{
    public static class MappCiudades
    {

        public static IEnumerable<CiudadesDTO> MapToDTO(this List<Ciudades> data)
        {
            List<CiudadesDTO> objLst = new List<CiudadesDTO>();

            data.ForEach(c => objLst.Add(c.MapToDTO()));

            return objLst;


        }

        public static Ciudades MapToEntity(this CiudadesDTO data)
        {
            return new Ciudades()
            {
                CiuCodigo = string.Empty,
                CiudID = data.id,
                CiuNombre = data.nombre
            };
        }


        public static CiudadesDTO MapToDTO(this Ciudades data)
        {

            if (data == null) data = new Ciudades()
            {
                CiudID = 0,
                CiuCodigo = string.Empty,
                CiuNombre = string.Empty
            };

            return new CiudadesDTO()
            {
                id = data.CiudID,
                nombre = data.CiuNombre
            };
        }
    }
}
