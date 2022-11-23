
using API.Helper;
using Code.Repository.Email.Implementation;
using Code.Repository.Email.Interface;
using Code.Repository.Email.Model;
using Code.Repository.Email.Template;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.Model.DTO.Usuarios;
using Code.Repository.Model.Entity;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Code.Repository.Session.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace API.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {

        //const int tercero = 21;
        private readonly IEmailSender IEmailSender;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserRepository _IUser;
        private readonly IWebHostEnvironment _env;

        public UsuarioController(IOptionsMonitor<EmailSettingsDTO> mailconfig,
           IWebHostEnvironment webHostEnvironment,
           IUserRepository IUser,
           IWebHostEnvironment env)
        {
            IEmailSender = new EmailSender(mailconfig.CurrentValue);
            _webHostEnvironment = webHostEnvironment;
            _IUser = IUser;
            _env = env;
        }

        [HttpGet]
        public IActionResult GetUsers(string tipo = "", string filter = "")
        {            

            if (filter == "_") filter = string.Empty;

            return Ok(new UsuarioBL().GetUsers(_IUser.GetUser(HttpContext).idEmpresa, filter: filter));
        }



        [HttpGet("all")]
        public IActionResult GetUsersAll(string tipo = "", string filter = "")
        {
          

            if (filter == "_") filter = string.Empty;

            return Ok(new UsuarioBL().GetUsersAll(_IUser.GetUser(HttpContext).idEmpresa, filter: filter));
        }

        [HttpGet("sinperfil")]
        public IActionResult GetUsersSinPerfil()
        {


            return Ok(new UsuarioBL().GetUsersSinPerfil(_IUser.GetUser(HttpContext).idEmpresa));
        }


        [HttpGet("detallado")]
        public IActionResult GetInfoUser()
        {
            var _session = _IUser.GetUser(HttpContext);
            return Ok(new UsuarioBL().GetUserDetail(_session.idEmpresa, _session.id));
        }


        [HttpGet("informacion")]
        public IActionResult GetUsersSession()
        {
            var currentUser = _IUser.GetUser(HttpContext);
            return Ok(new UserSession().GetUserSession(currentUser.id));
        }


        [HttpPost("actualizardatos")]
        public IActionResult PostActualizarDatos(UsuarioDTO request)
        {
            var session = _IUser.GetUser(HttpContext);
            return Ok(new UsuarioBL().ActualizarDatos(request, session.idEmpresa, session.id));
        }

        [HttpPost]
        public IActionResult PostUsuario(UsuarioDTO request)
        {
            var sesion = _IUser.GetUser(HttpContext);
            return Ok(new UsuarioBL().SaveUsuario(request, sesion.idEmpresa,sesion.id));
        }

        [HttpPost("cambiarestado")]
        public IActionResult PostCambiarEstado(CambiarEstadoUsuarioDTO request)
        {
            var sesion = _IUser.GetUser(HttpContext);
            return Ok(new UsuarioBL().CambiarEstadoUsuario(request.usuario, request.activo ? estadoUsuario.Activo: estadoUsuario.Inactivo, sesion.idEmpresa,sesion.id));
        }
        /// <summary>
        /// Metodo encargado de enviar los datos del usuario por correo para ingresar al portal
        /// </summary>
        /// <param name="_usuario"></param>
        /// <returns></returns>
        [HttpPost("envioCorreo")]
        public IActionResult PostEnvioCorreo(UsuarioIdDTO _usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string webRootPath = _webHostEnvironment.ContentRootPath;
                    //se valida si el proveedor ya existia para enviar otro template
                    string asunto = string.Empty;
                    ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
                    List<string> mail = new List<string>();

                    var currentUser = _IUser.GetUser(HttpContext);
                    var _user = objcnn.usuario.Find(_usuario.idUsuario);
                    string clave = new UsuarioBL().cambiarClaveUsuario(_usuario.idUsuario);
                    mail.Add(_user.UserCorreo);
                    asunto = "ADPROVEEDOR - Solicitud de invitación";
                    var template = new TemplateEmailSender(webRootPath).InvitacionTercero(new UserEMailDTO()
                    {
                        correo = _user.UserCorreo,
                        nombreEmpresa = currentUser.nombreEmpresa,
                        clave = _user.UserClave,
                        logoEMpresa = currentUser.URLtipo
                    });

                    string result = IEmailSender.SendEmail(mail, asunto, template);

                    return Ok(new ResponseDTO()
                    {
                        Success = true,
                        codigo = 1,
                        mensaje = result
                    });
                }
                catch (Exception e)
                {
                    return Ok(new ResponseDTO()
                    {
                        Success = false,
                        codigo = -1,
                        mensaje = "No se pudo enviar el correo"
                    });
                }
            }
            else return BadRequest(new ResponseDTO()
            {
                mensaje = ModelState.Values.getMessageError(),
                Success = false,
                codigo = 0
            });
        }
        /// <summary>
        /// Metodo encargado de restablecer la contraseña del usuario y enviar los nuevos datos por correo
        /// </summary>
        /// <param name="_usuario"></param>
        /// <returns></returns>
        [HttpPost("resetPassword")]
        public IActionResult PostResetPassword(UsuarioIdDTO _usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string webRootPath = _webHostEnvironment.ContentRootPath;
                    //se valida si el proveedor ya existia para enviar otro template
                    string asunto = string.Empty;
                    ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
                    List<string> mail = new List<string>();

                    var currentUser = _IUser.GetUser(HttpContext);
                    var _user = objcnn.usuario.Find(_usuario.idUsuario);
                    string NewClave = new UsuarioBL().cambiarClaveUsuario(_usuario.idUsuario);
                    mail.Add(_user.UserCorreo);
                    asunto = "ADPROVEEDOR - Restablecimiento de contraseña";
                    var template = new TemplateEmailSender(webRootPath).RememberPassword(new UserEMailDTO()
                    {
                        correo = _user.UserCorreo,
                        clave = NewClave,
                        logoEMpresa = currentUser.URLtipo,
                        nombreEmpresa = currentUser.nombreEmpresa
                    });



                    IEmailSender.SendEmail(mail, asunto, template);

                    return Ok(new ResponseDTO()
                    {
                        Success = true,
                        codigo = 1,
                        mensaje = "Se envio el correo exitosamente"
                    });
                }
                catch (Exception e)
                {
                    return Ok(new ResponseDTO()
                    {
                        Success = false,
                        codigo = -1,
                        mensaje = "No se pudo enviar el correo" + e.Message
                    });
                }
            }
            else return BadRequest(new ResponseDTO()
            {
                mensaje = ModelState.Values.getMessageError(),
                Success = false,
                codigo = 0
            });
        }
        /// <summary>
        /// Metodo encargado de guardar el logo para el tercero
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadFiles/")]
        public IActionResult SubirArchivo()
        {
            try
            {
                string OrigenID = _IUser.GetUser(HttpContext).idEmpresa.ToString();
                return Ok(new UsuarioBL().GuardarLogo(Request.Form.Files, int.Parse(OrigenID), _env.ContentRootPath));
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }

        }
        /// <summary>
        /// Metodo encargado de descargar el logo segun el tercero
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DescargarLogo")]
        public IActionResult DescargarLogo(int id)
        {
            //var _arhivos = new UsuarioBL().GetFile(id);
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            int IdTer = _IUser.GetUser(HttpContext).idEmpresa;

            Terceros objTercero = objcnn.terceros.Where(c => c.Terid == IdTer).FirstOrDefault();

            if (objTercero.TerRutaLogo == null || string.IsNullOrEmpty(objTercero.TerRutaLogo) || string.IsNullOrWhiteSpace(objTercero.TerRutaLogo))
            {
                var image = System.IO.File.OpenRead($"{_webHostEnvironment.ContentRootPath}\\images\\sin-logopng.png");
                return File(image, "image/jpeg");
            }
            //if (!System.IO.File.Exists(_arhivos.Adjruta))
            //    return BadRequest("El archivo no existe en el servidor.");


            string nameFile = HttpUtility.UrlEncode(objTercero.TerNombreLogo, System.Text.Encoding.UTF8);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(objTercero.TerRutaLogo, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(objTercero.TerRutaLogo);
            return File(bytes, contentType, Path.GetFileName(nameFile));


        }

    }

}
