using Code.Repository.DAO.Context;
using Code.Repository.Model.DTO.Abastecimiento;
using Code.Repository.Model.Entity;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.RepositoryBL.Operations.Abastecimiento.Licitaciones;
using Code.Repository.Session;
using Code.Repository.Session.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers.Abastecimiento.Licitaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSLicitacionMensajeController : ControllerBase
    {
        
        
        private readonly IUserRepository _IUser;
        #region ERP 
        public CSLicitacionMensajeController( IUserRepository user)
        {
            _IUser = user;
        }

        [HttpGet]
        [Route("Listado")]
        public async Task<IActionResult> ConsultarMensajes(int idConstructora = -1, int idLicitacion = -1)
        {

            var token = await new ConexionERP(idConstructora).ObtenerToken();
            var Const_ = new ConexionERP(idConstructora).ObtenerConstructora();
            string url = "/ADPRO/API/Licitacion/Mensaje/Listado?idLicitacion=" + idLicitacion.ToString() + "&tipo=2";

            var response_ = await new ConexionERP(idConstructora).Peticion(Const_.ConstRuta_API + url, HttpMethod.Get, null, token.ToString());
            return Ok(response_);
        }


        [HttpPut]
        public async Task<IActionResult> GuardarMensaje(CSMensajeDTO data)
        {

            var token = await new ConexionERP(data.IdConstructora).ObtenerToken();
            var Const_ = new ConexionERP(data.IdConstructora).ObtenerConstructora();
            string url = "/ADPRO/API/Licitacion/Mensaje/Guardar";

            var response_ = await new ConexionERP(data.IdConstructora).Peticion(Const_.ConstRuta_API + url, HttpMethod.Put, data, token.ToString());
            return Ok(response_);
        }


        [HttpGet]
        [Route("GetNotifica")]
        public async Task<IActionResult> getNotificaciones(int idConstructora = -1, int idLicitacion = -1)
        {
            int usuario = _IUser.GetUser(HttpContext).id;
            return Ok(new NotificacionesBL().getNotificaciones(idConstructora,idLicitacion, usuario));
        }
        #endregion
    }
}
