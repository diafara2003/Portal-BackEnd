using Code.Repository.Model.DTO.Login;
using Code.Repository.Session;
using Code.Repository.Session.Model;
using Code.Repository.Session.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PerfilController : ControllerBase
    {

        private readonly IUserRepository _IUser;
        public PerfilController(IUserRepository IUser)
        {
            _IUser = IUser;
        }

        [HttpGet("administracion")]
        public IActionResult GetPerfilAdministracion(int usuario = 0, int perfil = 0, string filter = "")
        {
            if (filter == "_") filter = string.Empty;
            UserSessionDTO user = _IUser.GetUser(HttpContext);
            return Ok(new PerfilesBL().GetPerfilconsulta(user.idEmpresa, usuario: usuario, perfil: perfil, filter: filter));
        }

        [HttpGet("administracion/consulta")]
        public IActionResult GetPerfilAdministracion(int perfil)
        {
            UserSessionDTO user = _IUser.GetUser(HttpContext);
            return Ok(new PerfilesBL().GetPerfil(user.idEmpresa, perfil));
        }

        [HttpPost]
        public IActionResult Post(AgregarPerfilDTO request)
        {
            UserSessionDTO user = _IUser.GetUser(HttpContext);
            return Ok(new PerfilesBL().AgregarPerfil(request, user.idEmpresa));

        }

        [HttpPost("cambiarEstado")]
        public IActionResult PostCambiarEstgado(PerfilDTO request)
        {
            
            new PerfilesBL().CambiarEstado(request);
            return Ok("") ;

        }
    }
}
