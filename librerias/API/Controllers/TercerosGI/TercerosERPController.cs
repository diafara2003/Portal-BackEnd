using API.Attributes;
using Code.Repository.Email.Implementation;
using Code.Repository.Email.Interface;
using Code.Repository.Email.Model;
using Code.Repository.Email.Template;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.DTO.TercerosGI;
using Code.Repository.Model.Entity;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace API.Controllers.TercerosGI
{
    [Route("api/[controller]")]
    [ApiController]
    public class TercerosERPController : ControllerBase
    {
        private readonly IEmailSender IEmailSender;
        private readonly IconstructoraRepository _Iconstructora;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public TercerosERPController(IconstructoraRepository IContructora, IOptionsMonitor<EmailSettingsDTO> mailconfig, IWebHostEnvironment webHostEnvironment)
        {
            _Iconstructora = IContructora;
            IEmailSender = new EmailSender(mailconfig.CurrentValue);
            _webHostEnvironment = webHostEnvironment;
        }




        [EnableCors("CorsPolicy")]
        [ApiKey]
        [HttpGet]
        [Route("informacion")]
        [AllowAnonymous]
        public IActionResult ConsultarTerceroXId(int proveedor)
        {
            var constructora = _Iconstructora.Get(HttpContext);
            var response = new TercerosGestionInformacionBL().GetDatosTerceroXId(constructora.ConstId, proveedor);
            return Ok(response);
        }


        [EnableCors("CorsPolicy")]
        [ApiKey]
        [HttpGet]
        [Route("listado")]
        [AllowAnonymous]
        public IActionResult ConsultarListadoTercero(int estado = 0)
        {
            var constructora = _Iconstructora.Get(HttpContext);
            var response = new TercerosGestionInformacionBL().GetDatosTercero(constructora.ConstId, estado);
            return Ok(response);
        }


        [EnableCors("CorsPolicy")]
        [ApiKey]
        [HttpGet]
        [Route("validacionPortal")]
        [AllowAnonymous]
        public IActionResult ConsultarAllTercero()
        {
            var constructora = _Iconstructora.Get(HttpContext);
            var response = new TercerosGestionInformacionBL().GetAllDatosTercero(constructora.ConstId);
            return Ok(response);
        }


        [EnableCors("CorsPolicy")]
        [ApiKey]
        [HttpGet]
        [Route("detallado")]
        [AllowAnonymous]
        public IActionResult ConsultarDetallesTercero(int proveedor)
        {
            var constructora = _Iconstructora.Get(HttpContext);
            var response = new TercerosGestionInformacionBL().GetDatosTercero(proveedor);
            return Ok(response);
        }



        [EnableCors("CorsPolicy")]
        [ApiKey]
        [HttpGet]
        [Route("proveedores")]
        [AllowAnonymous]
        public IActionResult GetProveedores(string filterId)
        {
            var constructora = _Iconstructora.Get(HttpContext);
            var response = new TercerosGestionInformacionBL().GetDatosTercero(filterId, constructora.ConstId);
            return Ok(response);
        }


        [EnableCors("CorsPolicy")]
        [ApiKey]
        [HttpPost]
        [Route("estado")]
        [AllowAnonymous]
        public IActionResult CambiarEstadoProveedor(List<CambiarEstadoTerCons> request)
        {
            var constructora = _Iconstructora.Get(HttpContext);


            new TercerosBL().CambiarEstadoTercero(constructora.ConstId, request);
          

            return Ok(new ResponseDTO()
            {
                Success = true,
                codigo = 0,
                mensaje = ""
            });
        }


        [EnableCors("CorsPolicy")]
        [ApiKey]
        [HttpPost]
        [Route("nuevo/registro")]
        public IActionResult NotificacionNuevoProveedor(ResponseCrearProveedorDTO request)
        {
            var constructora = _Iconstructora.Get(HttpContext);
            string webRootPath = _webHostEnvironment.ContentRootPath;
            
            List<string> mail = new List<string>();
            string asunto = "ADPROVEEDOR - Aprobación solicitud";
            AlternateView template = null;
            var datosConstructora = _Iconstructora.Get(HttpContext);

            new TercerosBL().NuevoTerceroERP(constructora.ConstId, request);

            template = new TemplateEmailSender(webRootPath).CreacionERP(new UserRechazoDTO()
            {
                nombreEmpresa = datosConstructora.ConstNombre,
                logoEMpresa = datosConstructora.ConstUrlLogo
            });
            
            var lstUser = new NotificacionesBL().GetUserNotificacion(Code.Repository.Model.Entity.dbo.TipoNotificaciones.Proveddores, request.idTercero);


            lstUser.ToList().ForEach(u => mail.Add(u.correo));


            IEmailSender.SendEmail(mail, asunto, template);

            return Ok(new ResponseDTO()
            {
                Success = true,
                codigo = 0,
                mensaje = ""
            });
        }

        [EnableCors("CorsPolicy")]
        [ApiKey]
        [HttpGet]
        [Route("Especialidad")]
        public IActionResult ConsultarTercerosxEspecialidad(int idEspecialidad)
        {
            var constructora = _Iconstructora.Get(HttpContext);
            var data_ = new TercerosBL().ConsultarTercerosxEspecialidad(idEspecialidad, constructora.ConstId);
            return Ok(data_);
        }

    }
}
