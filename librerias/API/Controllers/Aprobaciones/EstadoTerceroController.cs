using Code.Repository.Email.Implementation;
using Code.Repository.Email.Interface;
using Code.Repository.Email.Model;
using Code.Repository.Email.Template;
using Code.Repository.Model.DTO.Aprobaciones.Rangos;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Code.Repository.Session.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace API.Controllers.Aprobaciones
{
    [Route("api/[controller]")]
    [ApiController]

    public class EstadoTerceroController : ControllerBase
    {


        private readonly IUserRepository _IUser;
        private readonly IEmailSender IEmailSender;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public EstadoTerceroController(IUserRepository IUser,
            IOptionsMonitor<EmailSettingsDTO> mailconfig,
            IWebHostEnvironment webHostEnvironment

            )
        {
            _IUser = IUser;
            IEmailSender = new EmailSender(mailconfig.CurrentValue);
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public IActionResult GetEstadoTerceros()
        {

            UserSessionDTO sesion = _IUser.GetUser(HttpContext);
            return Ok(new TercerosBL().GetEstadoTercero(sesion.idEmpresa));
        }


        [HttpPost("aprobar")]
        public IActionResult AprobarTerceros(AprobarTerceroDTO request)
        {
            UserSessionDTO sesion = _IUser.GetUser(HttpContext);
            string webRootPath = _webHostEnvironment.ContentRootPath;
            string asunto = "";
            AlternateView template = null;
            List<string> mail = new List<string>();
            string base64String = string.Empty;

            if (sesion.logo != null)
                base64String = $"data:image/png;base64,{Convert.ToBase64String(sesion.logo, 0, sesion.logo.Length)}";

            var x = new UserRechazoDTO();
            if (!request.isAprobado)
            {
                asunto = "Rechazo solicitud en Portal ADPROVEEDOR";

                template = new TemplateEmailSender(webRootPath).RechazoAprobacion(new UserRechazoDTO()
                {
                    comentarios = request.comentarios,
                    nombreEmpresa = sesion.nombreEmpresa,
                    motivosRechazo = request.motivoRechazo.Select(c => c.texto).ToList(),
                    logoEMpresa = sesion.URLtipo
                });



                var lstUser = new NotificacionesBL().GetUserNotificacion(Code.Repository.Model.Entity.dbo.TipoNotificaciones.Proveddores, sesion.idEmpresa);

                lstUser.ToList().ForEach(u => mail.Add(u.correo));

                IEmailSender.SendEmail(mail, asunto, template);
            }
            return Ok(
                new RangoAprobacionesBL().AprobarTercero(request, sesion.idEmpresa, sesion.id)
                );
        }

        [HttpGet("rechazo/motivos")]
        public IActionResult GetMotivosRechazo()
        {
            return Ok(
                  new RangoAprobacionesBL().GetMotivosRechazo()
                  );
        }
    }
}
