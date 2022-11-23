using Code.Repository.Model.DTO.Aprobaciones.Rangos;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Code.Repository.Session.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Aprobaciones
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RangoTercerosController : ControllerBase
    {

        private readonly IUserRepository _IUser;
        public RangoTercerosController(IUserRepository IUser)
        {
            _IUser = IUser;
        }

        [HttpDelete("{id}")]
        public IActionResult ElimiarRangoAprobacionTercero(int id)
        {

            return Ok(new RangoAprobacionesBL().EliminarRango(id));
        }


        [HttpGet]
        [HttpGet]
        public IActionResult GetRangosTercero()
        {
            UserSessionDTO user = _IUser.GetUser(HttpContext);
            return Ok(new RangoAprobacionesBL().GetRangos(user.idEmpresa));
        }


        [HttpGet("aprobacion")]
        public IActionResult GetRangosAprobacion(int id)
        {
            UserSessionDTO user = _IUser.GetUser(HttpContext);
            return Ok(new RangoAprobacionesBL().GetPerfilesAProbacion(user.idEmpresa,id));
        }

        [HttpPost()]
        public IActionResult AgregarRango(RangoTerceroDTO request)
        {
            UserSessionDTO user = _IUser.GetUser(HttpContext);

            
            return Ok(new RangoAprobacionesBL().AgregarRango(user.idEmpresa, request));
        }
    }
}
