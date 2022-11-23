using API.Attributes;
using Code.Repository.Document.Operations;
using Code.Repository.Email.Implementation;
using Code.Repository.Email.Interface;
using Code.Repository.Email.Model;
using Code.Repository.Email.Template;
using Code.Repository.Model.DTO.Novedades;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.Entity;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Code.Repository.Session.Implementation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace API.Controllers.Novedades
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovedadERPController : ControllerBase
    {
       
        private readonly IEmailSender IEmailSender;
        private readonly IconstructoraRepository _Iconstructora;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NovedadERPController(IconstructoraRepository IContructora,
            IOptionsMonitor<EmailSettingsDTO> mailconfig,
            IWebHostEnvironment webHostEnvironment)
        {
            _Iconstructora = IContructora;
            IEmailSender = new EmailSender(mailconfig.CurrentValue);
            _webHostEnvironment = webHostEnvironment;
        }


        /// <summary>
        /// Agrega una nueva novedad en el sistema
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [EnableCors("CorsPolicy")]
        [ApiKey]
        [HttpPost]
        public IActionResult GuardarNovedad(NovedadesDTO request)
        {
            var constructora = _Iconstructora.Get(HttpContext);


            ResponseDTO data = new NovedadBL().RegistrarNovedad(request, constructora.ConstId);

            if (request.estadoTercero == (int)EstadoTercero.Rechazado || request.estadoTercero == (int)EstadoTercero.Completada)
            {

                string webRootPath = _webHostEnvironment.ContentRootPath;
                string asunto = "";
                AlternateView template = null;
                List<string> mail = new List<string>();

                var datosConstructora = _Iconstructora.Get(HttpContext);

                if (request.estadoTercero == (int)EstadoTercero.Rechazado)
                    asunto = "ADPROVEEDOR - Rechazo solicitud ";
                else asunto = "ADPROVEEDOR - Aprobación solicitud";

                var x = new UserRechazoDTO(); 

                if (request.estadoTercero == (int)EstadoTercero.Rechazado)
                {
                    List<string> lstrechazos = new List<string>();
                    var lstrechazoTexto = new RangoAprobacionesBL().GetMotivosRechazo();
                    var lstdocumentos = new AdjuntoBL().GetTipoadjuntotercero();
                    if (request.detalle != null && request.detalle.Count > 0)
                    {
                        request.detalle.ForEach(c =>
                        {
                            lstrechazos.Add(c.nombre);
                        });
                    }

                    template = new TemplateEmailSender(webRootPath).RechazoAprobacion(new UserRechazoDTO()
                    {
                        comentarios = request.comentario,
                        logoEMpresa = datosConstructora.ConstUrlLogo,
                        nombreEmpresa = datosConstructora.ConstNombre,
                        motivosRechazo = lstrechazos
                    });
                }


                else if (request.estadoTercero == (int)EstadoTercero.Validado)
                    template = new TemplateEmailSender(webRootPath).CreacionERP(new UserRechazoDTO()
                    {
                        nombreEmpresa = datosConstructora.ConstNombre,
                        logoEMpresa = datosConstructora.ConstUrlLogo
                    });


                var lstUser = new NotificacionesBL().GetUserNotificacion(Code.Repository.Model.Entity.dbo.TipoNotificaciones.Proveddores, request.tercero);

                lstUser.ToList().ForEach(u => mail.Add(u.correo));

                IEmailSender.SendEmail(mail, asunto, template);


                new TercerosBL().CambiarEstadoTercero(constructora.ConstId, request.tercero, (EstadoTercero)request.estadoTercero);
            }


            return Ok(data);

        }


        [EnableCors("CorsPolicy")]
        [ApiKey]
        [Route("Registrar")]
        [HttpPost]
        public IActionResult GuardarRegistroNovedad(NovedadRegistroDTO request)
        {
            var constructora = _Iconstructora.Get(HttpContext);

            ResponseDTO data = new NovedadBL().GuardarRegistroNovedad(request, constructora.ConstId);
            var usuarios = new NovedadBL().ObtenerUsuariosxCorreo(request.Correos);
            var response = new NovedadBL().GuardarNovedadUsuarios(data.codigo, usuarios);
            return Ok(response);
        }
    }

}
