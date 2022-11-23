using API.Configuration;
using API.Configuration.Auth;
using API.Helper;
using Code.Repository.Email.Implementation;
using Code.Repository.Email.Interface;
using Code.Repository.Email.Model;
using Code.Repository.Email.Template;
using Code.Repository.Model.DTO.Login;
using Code.Repository.Model.DTO.TercerosIG;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Mapper;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Code.Repository.Session.Implementation;
using Code.Repository.Session.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using System.Collections.Generic;

using System.IO;
using System.Linq;

namespace API.Controllers.login
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        //  private readonly ApplicationDatabaseContext _userManager;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IJwtAuth _IJwtAuth;
        private readonly IEmailSender IEmailSender;

        public LoginController(
            IOptionsMonitor<JwtConfig> optionsMonitor,
            IOptionsMonitor<EmailSettingsDTO> mailconfig,
            IWebHostEnvironment webHostEnvironment)
        {
            _IJwtAuth = new Auth(optionsMonitor.CurrentValue.SecretKey);
            IEmailSender = new EmailSender(mailconfig.CurrentValue);
            _webHostEnvironment = webHostEnvironment;


        }


        /// <summary>
        /// Registra un nuevo usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// 200:Token del usuario, 
        /// 404:EL usuario no existe,
        /// 500:El correo ya se encuentra en uso o los parametros son obligatarios</returns></returns>
        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {

                var existingUser = new TercerosBL().checkDocumento(user.documento);

                if (existingUser)
                {
                    return Ok(new RegistrationResponseDTO()
                    {
                        Message = "El NIT ingresado ya se encuentra en uso",
                        Success = false
                    });
                }


                existingUser = new TercerosBL().checkEmail(user.correo);

                if (existingUser)
                {
                    return Ok(new RegistrationResponseDTO()
                    {
                        Message = "El Correo ingresado ya se encuentra en uso",
                        Success = false
                    });
                }


                TerceroDTO _newUser = new TercerosBL().Add(user);

                List<string> mail = new List<string>();


                mail.Add(user.correo);
                string asunto = string.Empty;

                if (user.IsVinculoConstructora) asunto = "Invitación al portal";
                else asunto = "ADPROVEEDOR - Confirmación registro";


                var _userTercero = new UsuarioBL().GetUsers(_newUser.id).FirstOrDefault();

                string webRootPath = _webHostEnvironment.ContentRootPath;
                IEmailSender.SendEmail(mail, asunto,
                    new TemplateEmailSender(webRootPath).InvitacionTercero(new UserEMailDTO()
                    {
                        nombreEmpresa = $"{user.Nombres} {user.Apellidos}",
                        correo = user.correo,
                        clave = _userTercero.clave,
                        NIT = user.documento
                    }));



                if (user.IsVinculoConstructora)
                {
                    int id = _newUser.id;
                    string key = new ConstucturasBL().AsociarTercero(id, _newUser.id, estado: EstadoTercero.DocumentacionPendiente);
                }

                return Ok(new RegistrationResponseDTO()
                {
                    Success = true,
                    Message = "Se creo la empresa correctamente, se enviará un correo para la activación de la cuenta"
                });
            }
            else
            {
                return BadRequest(new RegistrationResponseDTO()
                {
                    Message = ModelState.Values.getMessageError(),
                    Success = false
                });
            }

        }





        /// <summary>
        /// varifica que el usuario este registrado
        /// </summary>
        /// <param name="user">DTO</param>
        /// <returns>
        /// 200:Token del usuario, 
        /// 404:EL usuario no existe,
        /// 500:mal request</returns>
        [HttpPost]
        [AllowAnonymous]
        [EnableCors("CorsPolicy")]
        public IActionResult Login([FromBody] UserLoginRequestDTO user)
        {


            if (ModelState.IsValid)
            {
                //consulta de usuario
                var existingUser = new UsuarioBL().checkUser(user.usuario);

                if (existingUser == null)

                    return NotFound(new RegistrationResponseDTO()
                    {
                        Message = "No existe el usuario o contraseña incorrecta",
                        Success = false
                    });


                if (!existingUser.UserClave.Equals(user.clave))
                    return NotFound(new RegistrationResponseDTO()
                    {
                        Message = "No existe el usuario o contraseña incorrecta",
                        Success = false
                    });

                //generacion del token
                var jwtToken = GenerateJwtToken(existingUser);

                //auditoria inicio de sesion
                new UsuarioBL().AddEntity<AuditoriaLogin>(new AuditoriaLogin()
                {
                    fecha = System.DateTime.Now,
                    id = 0,
                    usuario = existingUser.UserId,
                    tercero = existingUser.UserIdPpal
                });

                //respuesta
                return Ok(new RegistrationResponseDTO()
                {
                    Success = true,
                    Token = jwtToken,
                    usuario = new UserSession().GetUserSession(existingUser.UserId)
                });
            }
            else
                return BadRequest(new RegistrationResponseDTO()
                {
                    Message = ModelState.Values.getMessageError(),
                    Success = false
                });
        }


        private string GenerateJwtToken(Usuario user)
        {
            return _IJwtAuth.Authentication(user);
        }


    }
}
