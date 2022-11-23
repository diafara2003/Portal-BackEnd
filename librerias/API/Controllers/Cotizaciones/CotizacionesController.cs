using Code.Repository.DAO.Context;
using Code.Repository.Model.DTO.Abastecimiento.Cotizaciones;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.RepositoryBL.Operations.Abastecimiento.Cotizaciones;
using Code.Repository.Session;
using Code.Repository.Session.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers.Cotizaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionesController : ControllerBase
    {
        private IUserRepository idProv_;
        private readonly IconstructoraRepository Iconstructora_;

        public CotizacionesController(IconstructoraRepository IContructora, IUserRepository idProv)
        {
            idProv_ = idProv;
            Iconstructora_ = IContructora;
        }

        public int idConst = 2;

        [HttpGet]
        [Route("ActividadesExcel")]
        public async Task<IActionResult> ConsultarActividadesCotExcel(int idConst, int idLicitacion, int idCotizacion, string noLicitacion)
        {

            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            string url = "/ADPRO/API/CotizacionesLic/ActividadesExcel?idLicitacion=" + idLicitacion + "&idCotizacion=" + idCotizacion + "&idProveedor=" + idProv_.GetUser(HttpContext).idEmpresa + "&noLicitacion=" + noLicitacion;
            var response_ = await new ConexionERP(idConst).PeticionArchivo(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());

            return Ok(response_);
        }

        [HttpPost]
        [Route("ValidarImportacion")]
        public async Task<IActionResult> ValidarImportacion(LicCotizacionMMDTO data)
        {
            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            string url = "/ADPRO/API/CotizacionesLic/ValidarImportacion";
            var response_ = await new ConexionERP(idConst).Peticion(Const_.ConstRuta_API + url, HttpMethod.Put, data, token.ToString());

            return Ok(response_);
        }

        [HttpPost]
        [Route("GuardarImportacion")]
        public async Task<IActionResult> GuardarImportacion(LicCotizacionDTO data)
        {

            data.NitProveedor = idProv_.GetUser(HttpContext).nit;
            data.IdProveedor = -1;
            var token = await new ConexionERP(data.IdConstructora).ObtenerToken();
            var Const_ = new ConexionERP(data.IdConstructora).ObtenerConstructora();
            string url = "/ADPRO/API/CotizacionesLic/Guardar";
            var response_ = await new ConexionERP(data.IdConstructora).Peticion(Const_.ConstRuta_API + url, HttpMethod.Put, data, token.ToString());

            var resp = JObject.Parse(response_);
            
            int idcotiza = (int)resp["OtroValor"];

            url = "/ADPRO/API/Cotizaciones/ResumenPortal?idLicitacion="+data.IdLicitacion+"&idCotizacion="+ idcotiza.ToString();
            var resumencot_ = await new ConexionERP(data.IdConstructora).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());

            var respcot = JObject.Parse(resumencot_);

            CSCotizacionCotDTO cot = new CSCotizacionCotDTO();
            cot.IdLicitacion = (int)respcot["IdLicitacion"];
            cot.IdTercero = idProv_.GetUser(HttpContext).idEmpresa;
            cot.Cotizacion = (int)respcot["Cotizacion"];
            cot.Valor = (decimal)respcot["Valor"];
            cot.FormaPago = (string)respcot["FormaPago"];
            cot.TipoTributo = (string)respcot["TipoTributo"];
            cot.Estado = (int)respcot["Estado"];

            var x = new LicitacionesBL().CambiarEstadoCotizacion(data.IdConstructora, data.IdLicitacion, idProv_.GetUser(HttpContext).idEmpresa);
            var cotiza = new CotizacionBL().GuardarCotizacion(cot);
            return Ok(response_);
        }


        

    }

    public class LicCotizacionDTO
    {
        public int IdConstructora { get; set; }
        public int IdCotizacion { get; set; }
        public int IdLicitacion { get; set; }
        public int IdProveedor { get; set; }
        public string NitProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string CorreoProveedor { get; set; }
        public string Fecha { get; set; }
        public decimal Valor { get; set; }
        public bool EsMenor { get; set; }
        public bool ManejaAIU { get; set; }
        public decimal Administracion { get; set; }
        public decimal Utilidad { get; set; }
        public decimal Imprevistos { get; set; }
        public List<LicCotizacionActividadDTO> Actividades { get; set; }
    }


    public class LicCotizacionMMDTO
    {
        public int IdCotizacion { get; set; }
        public int IdLicitacion { get; set; }
        public int IdProveedor { get; set; }
        public List<LicCotizacionActividadMMDTO> Actividades { get; set; }
    }

    public class LicCotizacionActividadMMDTO
    {
        public int Id { get; set; }
        public int IdCotizacion { get; set; }
        public string CodigoActividad { get; set; }
        public decimal Valor { get; set; }
        public string CampoAdd { get; set; }
    }
    public class LicCotizacionActividadDTO
    {
        public int Id { get; set; }
        public int IdCotizacion { get; set; }
        public int IdActividad { get; set; }
        public string CodigoActividad { get; set; }
        public decimal Total { get; set; }
        public decimal Valor { get; set; }
        public LicActividadDTO Actividad { get; set; }
        public bool Minimo { get; set; }
        public bool Maximo { get; set; }
        public decimal VrMinimo { get; set; }
        public decimal VrMaximo { get; set; }
        public decimal VrPromedio { get; set; }
        public string CampoAdd { get; set; }
        public string Observacion { get; set; }
    }

    public class LicActividadDTO
    {
        public int IdActividad { get; set; }
        public int IdLicitacion { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Alcance { get; set; }
        public string UM { get; set; }
        public string Grupo { get; set; }
        public decimal CantPpto { get; set; }
        public decimal VrPpto { get; set; }
        public decimal Cant { get; set; }
        public decimal Total { get; set; }
    }
}
