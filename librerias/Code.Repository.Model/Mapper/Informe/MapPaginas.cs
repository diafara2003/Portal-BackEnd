using Code.Repository.Model.DTO.Informe;
using Code.Repository.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Mapper.Informe
{
    public static class MapPaginas
    {
        public static IEnumerable<PaginaDTO> MapToDTO(this IEnumerable<Menu> data)
        {
            List<PaginaDTO> objlst = new List<PaginaDTO>(); ;

            foreach (var item in data) objlst.Add(item.MapToDTO());

            return objlst;
        }

        public static PaginaDTO MapToDTO(this Menu data)
        {
            return new PaginaDTO()
            {
                id = data.IdMenu,
                nombre = data.Descripcion
            };
        }
    }
}
