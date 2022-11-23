using Code.Repository.Model.DTO.Novedades;
using Code.Repository.Model.Entity.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Mapper
{
    public static class MapNovedadUsuario
    {
        public static NovedadUsuarioDTO MapToDTO(this NovedadesUsuarios data)
        {
            return new NovedadUsuarioDTO()
            {
                IdNovedadUsuario = data.NUId,
                IdNovedad = data.NUNovedadId,
                IdUsuario = data.NUUsuarioId,
                Visto = data.NUVisto
            };
        }

        public static NovedadesUsuarios MapToEntity(this NovedadUsuarioDTO data)
        {
            return new NovedadesUsuarios()
            {
                NUId = data.IdNovedadUsuario,
                NUNovedadId = data.IdNovedad,
                NUUsuarioId = data.IdUsuario,
                NUVisto = data.Visto
            };
        }
    }
}
