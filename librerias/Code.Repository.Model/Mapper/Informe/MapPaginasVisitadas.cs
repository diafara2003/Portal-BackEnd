using Code.Repository.Model.DTO.Informe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Mapper.Informe
{
    public static class MapPaginasVisitadas
    {
        public static IEnumerable<PaginasVisitadasDTO> MapToDTOPaginasVisitas(this DataTable data)
        {
            return (from itemTable in data.AsEnumerable()

                    select new PaginasVisitadasDTO()
                    {
                        pagina = itemTable["pagina"] is not DBNull ? (string)itemTable["pagina"]:"",
                        usuario = itemTable["usuario"] is not DBNull ? (string)itemTable["usuario"]:"",
                        visitas = itemTable["visitas"] is not DBNull ? (int)itemTable["visitas"]: 0,
                        fecha = itemTable["fecha"] is not DBNull ? (string)itemTable["fecha"]: "",
                        hora = itemTable["hora"] is not DBNull ? (string)itemTable["hora"] :"",
                        totalRegistros = itemTable["totalRegistros"] is not DBNull ? (int)itemTable["totalRegistros"] :0,
                        orden = itemTable["orden"] is not DBNull ? (byte)itemTable["orden"]: byte.MinValue
                    });
        }
    }
}
