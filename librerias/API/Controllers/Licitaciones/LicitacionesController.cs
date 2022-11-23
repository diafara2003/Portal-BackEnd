using API.Attributes;
using API.Configuration;
using API.Configuration.Auth;
using API.Helper;
using Code.Repository.DAO.Context;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Licitaciones;
using Code.Repository.Model.DTO.Login;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.Entity;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Code.Repository.Session.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers.Licitaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicitacionesController : ControllerBase
    {

        private readonly IconstructoraRepository _Iconstructora;
        private readonly IUserRepository _Iprov;



        public LicitacionesController(IconstructoraRepository IContructora, IUserRepository IUser)
        {
            _Iconstructora = IContructora;
            _Iprov = IUser;


        }


        [HttpPost]
        [EnableCors("CorsPolicy")]
        [ApiKey]
        [AllowAnonymous]
        [Route("Resumen")]
        public async Task<IActionResult> GuardarResumen(LicitacionDTO data)
        {
            data.IdConstructora = _Iconstructora.Get(HttpContext).ConstId;
            return Ok(new LicitacionesBL().GuardarResumen(data));
        }


        [HttpPost]
        [EnableCors("CorsPolicy")]
        [ApiKey]
        [AllowAnonymous]
        [Route("InvitarTerceros")]
        public async Task<IActionResult> GuardarInvitaciones(List<LicitacionTerceroDTO> data)
        {
            return Ok(new LicitacionesBL().GuardarInvitacionTercero(data, _Iconstructora.Get(HttpContext).ConstId));
        }

        [HttpGet]
        [Route("Listado")]
        public IActionResult ConsultarListado(int idEmpresa)
        {
            return Ok(new LicitacionesBL().ConsultarListadoxProveedor(idEmpresa, _Iprov.GetUser(HttpContext).idEmpresa, false));
        }

        [HttpGet]
        [Route("CantidadxMes")]
        public IActionResult ConsultarCantidadxMes(int idEmpresa)
        {
            return Ok(new LicitacionesBL().ConsultarLicitacionesxMes(idEmpresa, _Iprov.GetUser(HttpContext).idEmpresa));
        }

        [HttpGet]
        [Route("Constructora")]
        public IActionResult ConsultarxContructora(int idProveedor, int idEmpresa)
        {
            return Ok(new LicitacionesBL().ConsultarListadoContructoraxProveedor(idProveedor, idEmpresa));
        }

        [HttpGet]
        [Route("ProximoVencer")]
        public IActionResult ProximasVencer(int idEmpresa)
        {
            return Ok(new LicitacionesBL().ConsultarListadoxProveedor(idEmpresa, _Iprov.GetUser(HttpContext).idEmpresa, true));
        }

        // ERP
        [HttpGet]
        [Route("InfoContructora")]
        public async Task<IActionResult> ConsultarContstructoraLicitacion(int idConst)
        {
            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            string url = "/ADPRO/API/Empresa/InfoBasicaPortal";

            var response_ = await new ConexionERP(idConst).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());
            return Ok(JObject.Parse(response_).ToObject<LicConstructoraDTO>());
        }

        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> ConsultarResumenLicitacion(int idConst, int idLic)
        {
            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            string url = "/ADPRO/API/Licitacion/ResumenPortal?idLicitacion=" + idLic.ToString();

            var response_ = await new ConexionERP(idConst).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());
            var x = JObject.Parse(response_).ToObject<LicResumenDTO>();
            return Ok(x);

        }


        [HttpGet]
        [Route("Contactos")]
        public async Task<IActionResult> ConsultarContactosLicitacion(int idConst, int idLic)
        {
            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            string url = "/ADPRO/API/Licitacion/ContactosPortal?idLicitacion=" + idLic.ToString();

            var response_ = await new ConexionERP(idConst).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());
            List<LicContactoDTO> data = new List<LicContactoDTO>();
            if (response_ == "[]")
            {
                return Ok(data);
            }
            else
            {
                var x = JArray.Parse(response_);
                var c = ((JArray)x).Select(x => new LicContactoDTO
                {
                    cargo = (string)x["Cargo"],
                    nombre = (string)x["Nombre"],
                    telefono = (string)x["Telefono"],
                    correo = (string)x["Correo"],
                }).ToList();

                return Ok(c);
            }
        }

        [HttpGet]
        [Route("Visitas")]
        public async Task<IActionResult> ConsultarVisitasLicitacion(int idConst, int idLic)
        {
            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            string url = "/ADPRO/API/Licitacion/ResumenPortal?idLicitacion=" + idLic.ToString();
            var response_ = await new ConexionERP(idConst).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());

            List<LicContactoDTO> data = new List<LicContactoDTO>();
            if (response_ == "[]")
            {
                return Ok(data);
            }
            else
            {
                var x = JArray.Parse(response_);
                var c = ((JArray)x).Select(x => new LicContactoDTO
                {
                    cargo = (string)x["cargo"],
                    nombre = (string)x["nombre"],
                    telefono = (string)x["telefono"],
                    correo = (string)x["correo"],
                }).ToList();

                return Ok(c);
            }
        }


        [HttpGet]
        [Route("Actividades")]
        public async Task<IActionResult> ConsultarActividadesLicitacion(int idConst, int idLic)
        {
            var token = await new ConexionERP(idConst).ObtenerToken();
            var Const_ = new ConexionERP(idConst).ObtenerConstructora();
            string url = "/ADPRO/API/Licitacion/ActividadPortal?id=" + idLic.ToString();

            var response_ = await new ConexionERP(idConst).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());
            List<LicActividadDTO> data = new List<LicActividadDTO>();
            if (response_ == "[]")
            {
                return Ok(data);
            }
            else
            {

                var x = JArray.Parse(response_);

                var c = ((JArray)x).Select(x => new LicActividadDTO
                {
                    idactividad = (int)x["idactividad"],
                    codigo = (string)x["codigo"],
                    descripcion = (string)x["descripcion"],
                    alcance = (string)x["alcance"],
                    um = (string)x["um"],
                    cantidad = (decimal)x["cantidad"],
                    valor = (decimal)x["valor"],
                    total = (decimal)x["valor"],
                    tipo = (int)x["tipo"],
                    observacion = (string)x["observacion"]

                }).ToList();

                return Ok(c);
            }

        }

        [HttpGet]
        [Route("TipoAdjunto")]
        public async Task<IActionResult> ConsultaTipoAdjunto(int idConstructora,string Descripcion, int Activo)
        {
            var currentUser = _Iprov.GetUser(HttpContext);

            var token = await new ConexionERP(idConstructora).ObtenerToken();
            var Const_ = new ConexionERP(idConstructora).ObtenerConstructora();
            string url = "/ADPRO/API/TipoAdjunto/Listado?Descripcion=" + Descripcion + "&Activo=" + Activo;

            var response_ = await new ConexionERP(idConstructora).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());
            List<LicTipoAdjuntoDTO> data = new List<LicTipoAdjuntoDTO>();
            if (response_ == "[]")
            {
                return Ok(data);
            }
            else
            {
                var JSON_Tipo = JArray.Parse(response_);

                var ListaTiposAdj = ((JArray)JSON_Tipo).Select(x => new LicTipoAdjuntoDTO
                {
                    Activo = (int)x["Activo"],
                    Descripcion = (string)x["Descripcion"],
                    Id = (int)x["IdTipoAdj"]

                }).ToList();

                return Ok(ListaTiposAdj);
            }
        }
        [HttpGet]
        [Route("Adjunto")]
        public async Task<IActionResult> ConsultaAdjunto(int idConstructora,int idLicitacion, string Tipo)
        {
            var currentUser = _Iprov.GetUser(HttpContext);
            //idLicitacion = 21;
            var token = await new ConexionERP(idConstructora).ObtenerToken();
            var Const_ = new ConexionERP(idConstructora).ObtenerConstructora();
            string url = "/ADPRO/API/UploadFile/ConsultarArchivosAdjuntos?OrigenID=" + idLicitacion.ToString() + "&Tipo=" + Tipo + "&OrigenID_2=-1";

            var response_ = await new ConexionERP(idConstructora).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());
            List<AdjuntoDTO> data = new List<AdjuntoDTO>();
            if (response_ == "")
            {
                return Ok(data);
            }
            else
            {
                var JSON_Adj = JArray.Parse(response_);

                var ListaAdj = ((JArray)JSON_Adj).Select(x => new AdjuntoDTO
                {
                    ArchivoID = (int)x["ArchivoID"],
                    Fecha = (DateTime)x["Fecha"],
                    NombreArchivo = (string)x["NombreArchivo"],
                    OrigenID = (int)x["OrigenID"],
                    Ruta = (string)x["Ruta"],
                    TipoArchivo = (string)x["TipoArchivo"],
                    observaciones = (string)x["Observaciones"]

                }).ToList();

                return Ok(ListaAdj);
            }
        }
        [HttpGet]
        [Route("DescargaAdjunto")]
        public async Task<IActionResult> DescargaAdjunto(int idConstructora, int idLicitacion, int idArchivo, string nameArch)
        {
            var currentUser = _Iprov.GetUser(HttpContext);
            //idLicitacion = 21;
            var token = await new ConexionERP(idConstructora).ObtenerToken();
            var Const_ = new ConexionERP(idConstructora).ObtenerConstructora();
            string url = "/ADPRO/API/UploadFile/Descargar?tipo=LIC&OrigenID=" + idLicitacion.ToString() + "&OrigenID2=-1&idarchivo=" + idArchivo;

            var response_ = await new ConexionERP(idConstructora).PeticionArchivo(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(nameArch, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return File(response_, contentType, nameArch);
        }
    }
}

public class LicResumenDTO
{
    public int numero { get; set; }
    public int idlicitacion { get; set; }
    public string asunto { get; set; }
    public string proyecto { get; set; }
    public string ciudad { get; set; }
    public string fecha { get; set; }
    public string fechavencimiento { get; set; }
    public string tipo { get; set; }
    public int iva { get; set; }
    public decimal valor { get; set; }
    public string formapago { get; set; }
    public string categoria { get; set; }
    public int cantVisitas { get; set; }
    public string descripcion { get; set; }
    public string acceso { get; set; }
    public string para { get; set; }

}

public class LicConstructoraDTO
{
    public string empresa { get; set; }
    public string nit { get; set; }
    public string contacto { get; set; }
    public string email { get; set; }
    public string telefono { get; set; }
    public string celular { get; set; }
}

public class LicActividadDTO
{
    public int tipo { get; set; }
    public int idactividad { get; set; }
    public string codigo { get; set; }
    public string descripcion { get; set; }
    public string alcance { get; set; }
    public string um { get; set; }
    public decimal cantidad { get; set; }
    public decimal valor { get; set; }
    public decimal total { get; set; }
    public string observacion { get; set; }
}


public class LicContactoDTO
{
    public string cargo { get; set; }
    public string nombre { get; set; }
    public string telefono { get; set; }
    public string correo { get; set; }
    public string fecha { get; set; }
}

