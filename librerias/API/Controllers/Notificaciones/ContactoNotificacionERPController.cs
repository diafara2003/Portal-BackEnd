using API.Attributes;
using Code.Repository.Model.Entity.dbo;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Notificaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoNotificacionERPController : ControllerBase
    {

        private readonly IconstructoraRepository _Iconstructora;


        public ContactoNotificacionERPController(IconstructoraRepository IContructora)
        {
            _Iconstructora = IContructora;

        }

        /// <summary>
        /// consulta todas las notificaciones para las licitaciones
        /// </summary>
        /// <returns></returns>
        [EnableCors("CorsPolicy")]
        [ApiKey]
        [HttpGet]
        [Route("licitacion")]
        [AllowAnonymous]
        public IActionResult GetNotificacionLicitacion(string nit)
        {
            //no se si sea necesario
            //var constructora = _Iconstructora.Get(HttpContext);
            return Ok(new NotificacionesBL().GetNotificacion(TipoNotificaciones.Licitaciones, nit));
        }
    }
}
