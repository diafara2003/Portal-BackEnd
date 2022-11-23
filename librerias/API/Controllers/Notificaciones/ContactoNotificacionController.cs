using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Code.Repository.Model.Entity.dbo;
namespace API.Controllers.Notificaciones
{
    [Route("api/[controller]")]
    [ApiController]

    public class ContactoNotificacionController : ControllerBase
    {
        private readonly IUserRepository _IUser;

        public ContactoNotificacionController(IUserRepository IUser)
        {
            _IUser = IUser;
        }


        /// <summary>
        /// consulta todas las notificaciones de los proveedores
        /// </summary>
        /// <returns></returns>
        [HttpGet("proveedor")]
        public IActionResult GetNotificacionProveedor()
        {
            var currentUser = _IUser.GetUser(HttpContext);
            return Ok(new NotificacionesBL().GetNotificacion(TipoNotificaciones.Proveddores, currentUser.idEmpresa));
        }

        /// <summary>
        /// consulta todas las notificaciones para las licitaciones
        /// </summary>
        /// <returns></returns>
        [HttpGet("licitaciones")]
        public IActionResult GetNotificacionLicitacion()
        {
            var currentUser = _IUser.GetUser(HttpContext);
            return Ok(new NotificacionesBL().GetNotificacion(TipoNotificaciones.Licitaciones, currentUser.idEmpresa));
        }


        /// <summary>
        /// agrega una datoContacto para los proveedores
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("proveedor")]
        public IActionResult PostnotificacionProveedor(Code.Repository.Model.DTO.Notificaciones.NotificacionDTO request)
        {
            var currentUser = _IUser.GetUser(HttpContext);
            return Ok(new NotificacionesBL().AgregarNotificacion(request, TipoNotificaciones.Proveddores, currentUser.idEmpresa, currentUser.id));
        }

        /// <summary>
        /// agrega unba datoContacto para las licitaciones
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("licitaciones")]
        public IActionResult PostnotificacionLicitacion(Code.Repository.Model.DTO.Notificaciones.NotificacionDTO request)
        {
            var currentUser = _IUser.GetUser(HttpContext);
            return Ok(new NotificacionesBL().AgregarNotificacion(request, TipoNotificaciones.Licitaciones, currentUser.idEmpresa, currentUser.id));
        }



        [HttpDelete()]
        public IActionResult DeleteNotificacionLicitacion(int id)
        {
            var currentUser = _IUser.GetUser(HttpContext);
            return Ok(new NotificacionesBL().EliminarNotificacion(id, currentUser.idEmpresa, currentUser.id));
        }



    }
}
