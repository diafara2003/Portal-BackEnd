using Code.Repository.DAO.Context;
using Code.Repository.Model.DTO.Abastecimiento.Licitaciones;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.RepositoryBL.Operations.Abastecimiento.Cotizaciones;
using Code.Repository.RepositoryBL.Operations.Abastecimiento.Licitaciones;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers.Abastecimiento.Licitaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSAseguradoraController : ControllerBase
    {
        [HttpGet]
        [Route("ConsultaTercerosLicitacion")]
        public async Task<IActionResult> consultaTercerosLicitacion( int op )
        {
            return Ok(new LicitacionTerceroBL().consultaAsegurados(op));
        } 

        [HttpPut]
        [Route("ActualizaEstado")]
        public async Task<IActionResult> actualizaEstado(CSAseguradoraDTO data)
        {
            var token = await new ConexionERP(data.IdConstructora).ObtenerToken();
            var Const_ = new ConexionERP(data.IdConstructora).ObtenerConstructora();
            string url = "/ADPRO/API/Licitacion/Invitado/ActualizaEstado";

            CSInvitadoDTO obj = new CSInvitadoDTO();

            obj.NIT = data.Identificacion;
            obj.IdLicitacion = data.IdLicitacion;
            obj.Asegurado = data.Asegurado;
            var response_ = await new ConexionERP(data.IdConstructora).Peticion(Const_.ConstRuta_API + url, HttpMethod.Put, obj, token.ToString());


            return Ok(new LicitacionTerceroBL().actualizaEstado(data));
        }
    }
}
