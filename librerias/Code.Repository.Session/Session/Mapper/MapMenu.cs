using Code.Repository.Session.Model.menu;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Code.Repository.Session.Mapper
{
    public static class MapMenu
    {
        public static IEnumerable<MenuDTO> MapToDTO(this DataTable data)
        {

            return (from q in data.AsEnumerable()
                    select new MenuDTO()
                    {
                        ActMenu = (bool)q["ActMenu"],
                        Descripcion = (string)q["Descripcion"],
                        IdMenu = (int)q["IdMenu"],
                        Mencodigo = (string)q["Mencodigo"],
                        PagAyuda = (string)q["Descripcion"],
                        SVG = (string)q["SVG"],
                        TieneHijos = (int)q["TieneHijos"],
                        Ubicacion = (string)q["Ubicacion"],
                        MenRequiereProyecto = (bool)q["requiereProyecto"],

                    });
        }
    }
}
