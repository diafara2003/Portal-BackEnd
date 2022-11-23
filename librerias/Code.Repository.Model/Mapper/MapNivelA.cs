using Code.Repository.Model.DTO.Ciudades;
using Code.Repository.Model.DTO.Login;
using Code.Repository.Model.Entity;
using System.Collections.Generic;

namespace Code.Repository.Model.Mapper
{
    public static class MapNivelA
    {
        public static NivelesA MapToEntity(this PerfilDTO data)
        {
            return new NivelesA()
            {
                NivNombre = data.nombre,
                NivId = data.id,
                NivEstado = data.estado
            };
        }

        public static List<PerfilConsultaDTO> MapToDTOConsulta(this List<NivelesA> data)
        {
            List<PerfilConsultaDTO> obj = new List<PerfilConsultaDTO>();

            data.ForEach(c => obj.Add(c.MapToDTOConsulta()));

            return obj;
        }

        public static PerfilConsultaDTO MapToDTOConsulta(this NivelesA data)
        {

            if (data == null) data = new NivelesA()
            {
                NivId = 0,
                NivEstado = true,
                NivNombre = string.Empty

            };

            return new PerfilConsultaDTO()
            {
                id = data.NivId,
                estado = data.NivEstado,
                nombre = data.NivNombre,
                countUsuarios = 0
            };
        }


        public static PerfilDTO MapToDTO(this NivelesA data)
        {

            if (data == null) data = new NivelesA()
            {
                NivId = 0,
                NivEstado = true,
                NivNombre = string.Empty

            };

            return new PerfilDTO()
            {
                id = data.NivId,
                estado = data.NivEstado,
                nombre = data.NivNombre
            };
        }
    }
}
