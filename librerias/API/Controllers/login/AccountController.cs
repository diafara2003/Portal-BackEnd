using API.Attributes;
using API.Helper;
using Code.Repository.Email.Hash;
using Code.Repository.Email.Implementation;
using Code.Repository.Email.Interface;
using Code.Repository.Email.Model;
using Code.Repository.Email.Template;
using Code.Repository.Model.DTO.TercerosGI;
using Code.Repository.Model.DTO.TercerosIG;
using Code.Repository.Model.DTO.Usuarios;
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace API.Controllers.login
{

    /// <summary>
    /// Controller para activar cuentas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IEmailSender IEmailSender;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IconstructoraRepository _Iconstructora;
        private readonly IUserRepository _IUser;
        public AccountController(IOptionsMonitor<EmailSettingsDTO> mailconfig,
            IconstructoraRepository IContructora,
            IWebHostEnvironment webHostEnvironment,
            IUserRepository iUser)
        {
            _Iconstructora = IContructora;
            IEmailSender = new EmailSender(mailconfig.CurrentValue);
            _webHostEnvironment = webHostEnvironment;
            _IUser = iUser;
        }

        /// <summary>
        /// Activa la cuenta de un tercero
        /// </summary>
        /// <param name="userid">id del tercero</param>
        /// <param name="token">token de seguridad</param>
        /// <returns>Estado de la peticion</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ConfirmEmail(string userid, string nit)
        {

            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(nit)) return BadRequest("");

            userid = HashHelper.Decrypt(userid);
            nit = HashHelper.Decrypt(nit);

            TerceroDTO _user = new TercerosBL().GetTerceroDocumento(nit);

            if (_user == null) return BadRequest("");


            string password = HashHelper.generatePassword();
            string body = string.Empty;


            if (!_user.correoConfirmado)
            {

                new TercerosBL().UpdateEntity<Terceros>(new Terceros()
                {
                    Terid = _user.id,
                    TerEmailConfirmado = true,
                    TerEstado = (int)EstadoTercero.DocumentacionPendiente
                });



                Usuario _userLogin = new UsuarioBL().GetUsers(_user.id).Single(c => c.correo.Equals(_user.correo)).MapToEntity();

                _userLogin.UserEstado = (int)estadoUsuario.Activo;
                _userLogin.UserClave = password;
                _userLogin.UserIdPpal = _user.id;

                new UsuarioBL().UpdateEntity<Usuario>(_userLogin);


            }
            else
            {
                password = new UsuarioBL().checkUser(_user.correo).UserClave;
            }

            //string webRootPath = @"C:\fuentes\Proveedores\ADPROVEEDOR\librerias\API";

            string webRootPath = _webHostEnvironment.ContentRootPath;



            using (StreamReader reader = new StreamReader(webRootPath + "/Template/AccountConfirmed.html"))
            {

                body = reader.ReadToEnd();
            }

            body = body.Replace("{{password}}", password);


            return new ContentResult()
            {
                ContentType = "text/html",
                Content = body
            };
        }

        //[EnableCors("CorsPolicy")]
        //[ApiKey]
        //[HttpPost("invitarproveedor")]
        //[AllowAnonymous]
        //public IActionResult TerceroInvitado([FromBody] InvitarTerceroFromERP request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        TercerosBL t = new TercerosBL();
        //        try
        //        {
        //            string webRootPath = _webHostEnvironment.ContentRootPath;
        //            Tuple<TerceroDTO, bool> _tercero = t.InvitacionTercero(request.informacion, EstadoTercero.Completada);

        //            if (request.datosContacto != null && request.datosContacto.Count > 0 && _tercero.Item2)
        //                t.AgregarContactosERP(request.datosContacto, _tercero.Item1);


        //            List<string> mail = new List<string>();
        //            mail.Add(request.informacion.correo);
        //            var _constructora = _Iconstructora.Get(HttpContext);
        //            //se valida si el proveedor ya existia para enviar otro template

        //            string asunto = string.Empty;

        //            var ter = new UsuarioBL().GetUserMain(_tercero.Item1.id);

        //            if (ter != null && _tercero.Item2)
        //            {
        //                Usuario _userLogin = ter.MapToEntity();
        //                _userLogin.UserEstado = (int)estadoUsuario.Activo;
        //                _userLogin.UserFechaRegistro = DateTime.Now;
        //                _userLogin.UserIdPpal = _tercero.Item1.id;
        //                _userLogin.UserNivel = 4;
        //                new UsuarioBL().UpdateEntity<Usuario>(_userLogin);
        //            }
        //            asunto = $"Adproveedor - Invitación {_constructora.ConstNombre}";
        //            var template = new TemplateEmailSender(webRootPath).InvitacionTercero(new UserEMailDTO()
        //            {
        //                correo = request.informacion.correo,
        //                clave = new UsuarioBL().GetClaveUser(ter.id),
        //                nombreEmpresa = _constructora.ConstNombre,
        //                logoEMpresa = _constructora.ConstUrlLogo
        //            });


        //            IEmailSender.SendEmail(mail, asunto, template);

        //            string key = new ConstucturasBL().AsociarTercero(_constructora.ConstId, _tercero.Item1.id);

        //            return Ok(new RegistrationResponseDTO()
        //            {
        //                Token = key,
        //                Success = true,
        //                Id = _tercero.Item1.id,
        //                Message = _tercero.Item2 ? "Se registro el proveedor correctamente, se enviará un correo para la activación de la cuenta" : "Se realizo la vinculación con el proveedor"
        //            });
        //        }
        //        catch (Exception e)
        //        {

        //            return Ok(new RegistrationResponseDTO()
        //            {
        //                Token = string.Empty,
        //                Success = false,
        //                Id = 0,
        //                Message = e.Message
        //            });
        //        }

        //    }
        //    else return BadRequest(new RegistrationResponseDTO()
        //    {
        //        Message = ModelState.Values.getMessageError(),
        //        Success = false,
        //        Id = 0
        //    });

        //}
        [EnableCors("CorsPolicy")]
        [ApiKey]
        [HttpPost("invitarproveedor")]
        [AllowAnonymous]
        public IActionResult TerceroInvitado([FromBody] InvitarTerceroFromERP request)
        {
            if (ModelState.IsValid)
            {
                TercerosBL t = new TercerosBL();
                try
                {
                    var dataTercero = new TercerosGestionInformacionBL().getDataTercero(request.informacion.documento);
                    string webRootPath = _webHostEnvironment.ContentRootPath;
                    var _user = new UsuarioBL().checkUser(request.informacion.correo);
                    var _construnctora = _Iconstructora.Get(HttpContext);

                    // 1 Valida si el nit NO EXISTE
                    if (dataTercero == null)
                    {
                        // 1.1 Valida si el correo existe // error
                        if (_user != null)
                        {
                            return Ok(new RegistrationResponseDTO()
                            {
                                Token = String.Empty,
                                Success = false,
                                Id = 0,
                                Message = "El correo ingresado ya existe en el portal de proveedores."
                            });
                        }
                        // 1.2 Si no existe, crea USUARIO 
                        else
                        {
                            var _tercero = t.InvitaAsociaEnviaMailTercero(request, _construnctora, webRootPath);
                            IEmailSender.SendEmail(_tercero.Mail, _tercero.Asunto, _tercero.Template);
                          
                            return Ok(new RegistrationResponseDTO()
                            {
                                Token = string.Empty,
                                Success = true,
                                Id = _tercero.TerceroInvitacion.Item1.id,
                                Message = _tercero.TerceroInvitacion.Item2 ? "Se registro el proveedor correctamente, se enviará un correo para la activación de la cuenta" : "Se realizo la vinculación con el proveedor"
                            });
                        }
                    }
                    // 2 si el NIT EXISTE
                    else
                    {
                        //2.2 Valida si no existe el correo en el tercero ni en el sistema
                        var existeCorreoEnTercero = new TercerosGestionInformacionBL().existeCorreoTercero(dataTercero.TigTerceroId, request.informacion.correo);
                        //No existe el correo en la empresa  && //No existe el correo en la tabla usuarios
                        if (!existeCorreoEnTercero)
                        {
                            if (_user != null)
                            {
                                return Ok(new RegistrationResponseDTO()
                                {
                                    Token = String.Empty,
                                    Success = true,
                                    Id = dataTercero.TigTerceroId,
                                    Message = "El correo se encuentra registrado como otro proveedor dentro del portal."
                                });
                            }

                            //CREA USUARIO EN ESTADO -1 asociado al NIT existente

                            var _tercero = t.creaUserEstadoIncompleto(request, dataTercero.TigTerceroId, _construnctora, webRootPath);
                            IEmailSender.SendEmail(_tercero.Mail, _tercero.Asunto, _tercero.Template);

                            return Ok(new RegistrationResponseDTO()
                            {
                                Token = String.Empty,
                                Success = true,
                                Id = dataTercero.TigTerceroId,
                                Message = "Se enviará un correo para la activación de la cuenta."
                            });
                        }
                        //2.2 Valida si  EXISTE el correo en el tercero 
                        else
                        {
                            //Envia correo invitacion vacia
                            var Mail = new List<string>();
                            Mail.Add(request.informacion.correo);
                            var asunto = $"Adproveedor - Invitación {_construnctora.ConstNombre}";                            
                            var template = new TemplateEmailSender(webRootPath).InvitacionTercero(new UserEMailDTO()
                            {                                                              
                                logoEMpresa = _construnctora.ConstUrlLogo,
                                nombreEmpresa = _construnctora.ConstNombre
                            },isnew:false);

                            IEmailSender.SendEmail(Mail,asunto, template);

                            return Ok(new RegistrationResponseDTO()
                            {
                                Token = String.Empty,
                                Success = true,
                                Id = dataTercero.TigTerceroId,
                                Message = "Se ha enviado un correo con la invitación."
                            });
                        }

                    }
                }
                catch (Exception e)
                {
                    return Ok(new RegistrationResponseDTO()
                    {
                        Token = string.Empty,
                        Success = false,
                        Id = 0,
                        Message = e.Message
                    });
                }

            }
            else return BadRequest(new RegistrationResponseDTO()
            {
                Message = ModelState.Values.getMessageError(),
                Success = false,
                Id = 0
            });
        }
        [EnableCors("CorsPolicy")]
        [ApiKey]
        [HttpPost("solicitudes/nuevo")]
        [AllowAnonymous]
        public IActionResult SolicitudNuevoTercero([FromBody] InvitacionTerceroDTO request)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    var _user = new UsuarioBL().checkUser(request.correo);
                    //se valida si el correo ya existe
                    if ((_user != null && _user.UserId > 0)

                        &&

                        (_user.UserDoc != request.documento))
                    {
                        return Ok(new RegistrationResponseDTO()
                        {
                            Token = String.Empty,
                            Success = false,
                            Id = 0,
                            Message = "El correo ingresado ya existe en el portal de proveedores."
                        });
                    }


                    string webRootPath = _webHostEnvironment.ContentRootPath;
                    Tuple<TerceroDTO, bool> _tercero = new TercerosBL().InvitacionTercero(request);

                    List<string> mail = new List<string>();
                    mail.Add(request.correo);
                    var _constructora = _Iconstructora.Get(HttpContext);
                    //se valida si el proveedor ya existia para enviar otro template

                    string asunto = string.Empty;

                    var ter = new UsuarioBL().GetUserMain(_tercero.Item1.id);
                    string password = HashHelper.generatePassword();
                    if (ter != null)
                    {
                        Usuario _userLogin = ter.MapToEntity();
                        _userLogin.UserEstado = (int)estadoUsuario.Inactivo;
                        _userLogin.UserNivel = 4;
                        _userLogin.UserClave = password;
                        _userLogin.UserFechaRegistro = DateTime.Now;
                        _userLogin.UserIdPpal = _tercero.Item1.id;
                        new UsuarioBL().UpdateEntity<Usuario>(_userLogin);
                    }
                    asunto = $"Addproveedor - Invitación {ter.nombre}";
                    var template = new TemplateEmailSender(webRootPath).InvitacionTercero(new UserEMailDTO()
                    {
                        correo = request.correo,
                        clave = password,
                        logoEMpresa = _constructora.ConstUrlLogo,
                        nombreEmpresa = _constructora.ConstNombre
                    });


                    IEmailSender.SendEmail(mail, asunto, template);

                    string key = new ConstucturasBL().AsociarTercero(_constructora.ConstId, _tercero.Item1.id, EstadoTercero.DocumentacionPendiente);

                    return Ok(new RegistrationResponseDTO()
                    {
                        Token = key,
                        Success = true,
                        Id = _tercero.Item1.id,
                        Message = _tercero.Item2 ? "Se registro el proveedor correctamente, se enviará un correo para la activación de la cuenta" : "Se realizo la vinculación con el proveedor"
                    });
                }
                catch (Exception e)
                {

                    return Ok(new RegistrationResponseDTO()
                    {
                        Token = string.Empty,
                        Success = false,
                        Id = 0,
                        Message = e.Message
                    });
                }

            }
            else return BadRequest(new RegistrationResponseDTO()
            {
                Message = ModelState.Values.getMessageError(),
                Success = false,
                Id = 0
            });

        }





        [ApiKey]
        [HttpPost("desasociartercero")]
        [AllowAnonymous]
        public IActionResult DesAsociarTercero(DesasociarTercero request)
        {

            try
            {
                var _tercero = new TercerosBL().GetTerceroDocumento(request.documento);
                var _constructora = _Iconstructora.Get(HttpContext);
                new ConstucturasBL().DesasociarTercero(_constructora.ConstId, _tercero.id);

                return Ok(new RegistrationResponseDTO()
                {
                    Message = string.Empty,
                    Success = true
                });
            }
            catch (Exception e)
            {

                return Ok(new RegistrationResponseDTO()
                {
                    Message = e.Message,
                    Success = false
                });
            }

        }



        [HttpPost("recordarclave")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult RecordarClave(RecordarClaveDTO request)
        {

            try
            {
                string webRootPath = _webHostEnvironment.ContentRootPath;
                Usuario _userEmpresa = new UsuarioBL().checkUser(request.usuario);

                if (_userEmpresa != null)
                {

                    var _tercero = new TercerosBL().GetEntity<Terceros>(_userEmpresa.UserIdPpal);
                    var _terceroDetalles = new TercerosGestionInformacionBL().ConsultarInfGeneral(_userEmpresa.UserIdPpal);


                    string asunto = string.Empty;

                    asunto = "ADPROVEEDOR - Recuperación contraseña";
                    var template = new TemplateEmailSender(webRootPath).RememberPassword(new UserEMailDTO()
                    {
                        clave = _userEmpresa.UserClave,
                        correo = _userEmpresa.UserCorreo,
                        nombreEmpresa = _terceroDetalles.Nombre
                    });

                    List<string> mail = new List<string>();
                    mail.Add(_userEmpresa.UserCorreo);

                    IEmailSender.SendEmail(mail, asunto, template);


                    return Ok(new RegistrationResponseDTO()
                    {
                        Token = string.Empty,
                        Success = true,
                        Message = "Se realizo la cuperación de la contraseña correctamente."
                    });
                }
                else
                {
                    return NotFound(new RegistrationResponseDTO()
                    {
                        Message = "Usuario no encontrado"
                    });
                }
            }
            catch (Exception e)
            {

                return Ok(new RegistrationResponseDTO()
                {
                    Message = e.Message,
                    Success = false
                });
            }

        }
        /// <summary>
        /// Metodo encargado de cambiar la contraseña manualmente
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("cambioClave")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CambioClave(CambioClaveDTO request)
        {
            var currentUser = (int)_IUser.GetUser(HttpContext).id;
            return Ok(new UsuarioBL().cambiarClaveManual(currentUser, request));

        }




    }
}
