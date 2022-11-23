using Code.Repository.DAO.Context;
using Code.Repository.Model.DTO.Ciudades;
using Code.Repository.RepositoryBL.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers.Ciudades
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CiudadController : ControllerBase
    {

        public IEnumerable<CiudadesDTO> Get(string filter)
        {

            if (string.IsNullOrEmpty(filter)) filter = string.Empty;
            else if (filter == "_") filter = string.Empty;

            return new CiudadesBL().Get(filter);

        }

        //ERP
        [HttpGet]
        [Route("CiudadProveedor")]
        public async Task<IActionResult> ConsultarCiudadProveedor(int idConst, string filter)
        {
            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            string url = "/ADPRO/API/Ciudad/ListaCiudades?id=" + filter;

            var response_ = await new ConexionERP(idConst).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());

            var x = JArray.Parse(response_);
            List<CiudadesDTO> data = new();
            var ListCiudades = ((JArray)x).Select(x => new CiudadesDTO
            {
                id = (int)x["CiuId"],
                nombre = ((string)x["CiuDesc"]).Split('-')[1]

            }).ToList();
            return Ok(ListCiudades);
        }
    }
}
