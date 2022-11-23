using Code.Repository.DAO.Context;
using Code.Repository.Model.DTO.Abastecimiento;
using Code.Repository.Model.DTO.Abastecimiento.Cotizaciones;
using Code.Repository.Model.DTO.Abastecimiento.Licitaciones;
using Code.Repository.RepositoryBL.Operations.Abastecimiento.Cotizaciones;
using Code.Repository.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers.Abastecimiento.Cotizaciones
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CSCotizacionController : ControllerBase
    {
        private readonly IUserRepository _Iprov;

        public CSCotizacionController(IUserRepository IProv)
        {
            _Iprov = IProv;
        }

        [HttpGet]
        [Route("Detalle")]
        public CSCotizacionCotDTO ConsultarDetalleCotizacion(int idConstructora, int idLicitacion = -1, int idCotizacion = -1)
        {
            return new CotizacionBL().ConsultarDetalleCotizacion(_Iprov.GetUser(HttpContext).idEmpresa, idConstructora, idLicitacion, idCotizacion);
        }

        [HttpGet]
        [Route("Listado")]
        public IEnumerable<CSResumenCotizacionDTO> ConsultarResumenCotizaciones(int idEmpresa)
        {
            return new CotizacionBL().ConsultarCotizacionesxProveedor(idEmpresa, _Iprov.GetUser(HttpContext).id);
        }

        [HttpGet]
        [Route("Dashboard")]
        public CSDashboardCotDTO ConsultarInfoDashboard(int idEmpresa)
        {
            return new CotizacionBL().ConsultarResumenCotizaciones(_Iprov.GetUser(HttpContext).id, idEmpresa);
        }

        [HttpGet]
        [Route("Categoria/Agrupada")]
        public object ConsultarInvitacionesCategoria(int idEmpresa)
        {
            return new CotizacionBL().ConsultarInvitacionesAgrupadasxCategoria(_Iprov.GetUser(HttpContext).id, idEmpresa);
        }

        [HttpGet]
        [Route("Estados/Agrupado")]
        public object ConsultarEstadosAgrupados(int idEmpresa)
        {
            return new CotizacionBL().ConsultarEstadosAgrupados(_Iprov.GetUser(HttpContext).id, idEmpresa);
        }

        [HttpPut]
        [Route("Confirmar")]

        public async Task<IActionResult> GuardarConfirmacion(CSDocumentoDTO data)
        {

            var token = await new ConexionERP(data.IdConstructora).ObtenerToken();
            var Const_ = new ConexionERP(data.IdConstructora).ObtenerConstructora();
            string url = "/ADPRO/API/Cotizaciones/Confirmar";

            var response_ = await new ConexionERP(data.IdConstructora).Peticion(Const_.ConstRuta_API + url, HttpMethod.Put, data, token.ToString());
            var c = new CotizacionBL().ConfirmarCotizacion(data.IdConstructora, data.IdLicitacion, data.IdDocumento);

            return Ok(response_);
        }

        [HttpGet]
        [Route("Datos")]
        public async Task<IActionResult> ConsultarDatos(int idEmpresa = -1, int idLicitacion = -1, int idCotizacion = -1)
        {
            var token = await new ConexionERP(idEmpresa).ObtenerToken();
            var Const_ = new ConexionERP(idEmpresa).ObtenerConstructora();
            string url = "/ADPRO/API/Cotizaciones/Datos?idLicitacion=" + idLicitacion.ToString() + "&idCotizacion=" + idCotizacion.ToString();

            var response_ = await new ConexionERP(idEmpresa).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());

            return Ok(response_);
        }

        
        [HttpPut]
        [Route("Datos")]
        public async Task<IActionResult> GuardarDatos(CSDatosCotDTO data)
        {
            data.Proveedor = Convert.ToInt32(_Iprov.GetUser(HttpContext).nit);
            var token = await new ConexionERP(data.IdConstructora).ObtenerToken();
            var Const_ = new ConexionERP(data.IdConstructora).ObtenerConstructora();
            string url = "/ADPRO/API/Cotizaciones/Datos";

            var response_ = await new ConexionERP(data.IdConstructora).Peticion(Const_.ConstRuta_API + url, HttpMethod.Put, data, token.ToString());

            var resp = JObject.Parse(response_);

            int idcotiza = (int)resp["Id"];

            url = "/ADPRO/API/Cotizaciones/ResumenPortal?idLicitacion=" + data.IdLicitacion + "&idCotizacion=" + idcotiza.ToString();
            var resumencot_ = await new ConexionERP(data.IdConstructora).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());

            var respcot = JObject.Parse(resumencot_);

            CSCotizacionCotDTO cot = new CSCotizacionCotDTO();
            cot.IdLicitacion = (int)respcot["IdLicitacion"];
            cot.IdTercero = _Iprov.GetUser(HttpContext).idEmpresa;
            cot.Cotizacion = (int)respcot["Cotizacion"];
            cot.Valor = (decimal)respcot["Valor"];
            cot.FormaPago = (string)respcot["FormaPago"];
            cot.TipoTributo = (string)respcot["TipoTributo"];
            cot.Estado = (int)respcot["Estado"];

            var cotiza = new CotizacionBL().GuardarCotizacion(cot);

            return Ok(response_);
        }

        [HttpGet]
        [Route("Versiones")]
        public async Task<IActionResult> ConsultarVersiones(int idEmpresa = -1, int idLicitacion = -1)
        {
            var token = await new ConexionERP(idEmpresa).ObtenerToken();
            var Const_ = new ConexionERP(idEmpresa).ObtenerConstructora();
            string url = "/ADPRO/API/Cotizaciones/Versiones?idLicitacion=" + idLicitacion.ToString() + "&tercero=" + _Iprov.GetUser(HttpContext).nit;

            var response_ = await new ConexionERP(idEmpresa).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());

            return Ok(response_);
        }


    }
}
