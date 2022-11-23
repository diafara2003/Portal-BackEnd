using API.Attributes;
using Code.Repository.DAO.Context;
using Code.Repository.Model.DTO.Abastecimiento.Licitaciones;
using Code.Repository.RepositoryBL.Operations.Abastecimiento.Licitaciones;
using Code.Repository.Session;
using Code.Repository.Session.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers.Abastecimiento.Licitaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSLicitacionInvitadoController : ControllerBase
    {
        private readonly IUserRepository _Iprov;
        private readonly IconstructoraRepository _Iconstructora;
        public CSLicitacionInvitadoController(IUserRepository IProv, IconstructoraRepository IConst)
        {
            _Iconstructora = IConst;
            _Iprov = IProv;
        }
        private int idConstructora_ = 2;

        [HttpPut]
        public async Task<IActionResult> GuardarConfirmacion(CSInvitadoDTO data)
        {
            data.NIT = _Iprov.GetUser(HttpContext).nit;

            var token = await new ConexionERP(idConstructora_).ObtenerToken();
            var Const_ = new ConexionERP(idConstructora_).ObtenerConstructora();
            string url = "/ADPRO/API/Licitacion/Invitado/Confirmar";

            var response_ = await new ConexionERP(idConstructora_).Peticion(Const_.ConstRuta_API + url, HttpMethod.Put, data, token.ToString());
            return Ok(response_);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarEstado(int idLicitacion)
        {
            string Nit_ = _Iprov.GetUser(HttpContext).nit;

            var token = await new ConexionERP(idConstructora_).ObtenerToken();
            var Const_ = new ConexionERP(idConstructora_).ObtenerConstructora();
            string url = "/ADPRO/API/Licitacion/Invitado/Aceptado?idLicitacion=" + idLicitacion.ToString() + "&nit=" + Nit_;

            var response_ = await new ConexionERP(idConstructora_).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());
            return Ok(response_);
        }

        [HttpPost]
        [EnableCors("CorsPolicy")]
        [ApiKey]
        [Route("HabilitarCotizacion")]
        [AllowAnonymous]
        public async Task<IActionResult> HabilitarCotizarTercero(List<CSInvitadoDTO> data)
        {
            return Ok(new LicitacionTerceroBL().HabilitarCotizar(data, _Iconstructora.Get(HttpContext).ConstId));
        }

    }
}
