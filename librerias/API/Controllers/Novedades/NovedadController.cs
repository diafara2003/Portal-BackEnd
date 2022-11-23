using Code.Repository.Model.DTO.Novedades;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.Novedades
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovedadController : ControllerBase
    {
        private readonly IUserRepository _IUser;
        public NovedadController(IUserRepository IUser)
        {
            _IUser = IUser;
        }

        [HttpGet]
        [Route("constructora")]        
        public IActionResult GetNovedadAll(int constructora)
        {
            var currentUser = _IUser.GetUser(HttpContext);

            return Ok(new NovedadBL().GetNovedadTercero(currentUser.idEmpresa, constructora,currentUser.id));
        }


        [HttpGet]
        public IActionResult GetNovedad(int novedad)
        {

            return Ok(new NovedadBL().GetNovedad(novedad));
        }


        [HttpPost("cambiarestado")]
        public async Task<IActionResult> PostCambiarEstado(CambiarEstadoNovedadDTO request) {
            return Ok(await new NovedadBL().CambiarEstado(request));
        }

        /// <summary>
        /// Consulta las novedades segun tercero
        /// </summary>     
        /// <returns></returns>       

        [HttpGet]
        [Route("ConsultarNovedad")]
        public IActionResult ConsultaNovedad()
        {
            var currentUser = _IUser.GetUser(HttpContext);

            return Ok(new NovedadBL().ConsultaNovedadesTerceros(currentUser.idEmpresa));

        }

        [HttpGet]
        [Route("ConstructoraNovedad")]
        public IActionResult ConsultaConstructoraNovedad()
        {
            var currentUser = _IUser.GetUser(HttpContext);

            return Ok(new NovedadBL().ConsultaConstructorasNovedadTer(currentUser.idEmpresa));

        }

        [HttpPost]
        [Route("CambiarVistoNovedad")]
        public IActionResult NovedadVista(CambiarVistoNovedadDTO data)
        {
            return Ok(new NovedadBL().CambiarVistoNovedad(data.idConstrcutora));

        }
    }
}
