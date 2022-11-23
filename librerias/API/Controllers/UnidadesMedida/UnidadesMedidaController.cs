using Code.Repository.DAO.Context;
using Code.Repository.Model.DTO.UnidadesMedida;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers.UnidadesMedida
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadesMedidaController: ControllerBase
    {
        //ERP
        [HttpGet]
        [Route("ConsultarUMConstructora")]
        public async Task<IActionResult> ConsultarUMConstructora(int idConst, string filter)
        {
            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            string url = "/ADPRO/API/UnidadesMedidas/Consultar?UCid=" + filter + "&UCDesc=-1";

            var response_ = await new ConexionERP(idConst).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());

            var x = JArray.Parse(response_);
            List<UnidadesMedidaDTO> data = new();
            var ListUnidadesMedida = ((JArray)x).Select(x => new UnidadesMedidaDTO
            {
                UMId=(string)x["UCid"],
                UMDecripcion=(string)x["UCDesc"]

            }).ToList();
            return Ok(ListUnidadesMedida);
        }
    }
}
