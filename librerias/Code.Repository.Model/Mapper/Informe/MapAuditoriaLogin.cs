using Code.Repository.Model.DTO.Informe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Mapper.Informe
{
    public static class MapAuditoriaLogin
    {
        public static IEnumerable<AuditoriaLoginDTO> MapToDTO(this DataTable data)
        {
            return (from x in data.AsEnumerable()

                    select new AuditoriaLoginDTO()
                    {
                        nombre=(string)x["UserNombre"],
                        correo = (string)x["UserCorreo"],
                        documento = (string)x["UserDoc"],
                        fecha = (string)x["fecha"],
                        inicioCount= (int)x["countLogin"],

                        totalPaginas = (int)x["countTotal"],
                    });
        }



    }
}
