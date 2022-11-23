
using Code.Repository.Model.DTO.Informe;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.Informes
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InformesController : ControllerBase
    {
        private readonly IUserRepository _IUser;

        public InformesController(IUserRepository IUser)
        {
            _IUser = IUser;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int constructora, int id, int rows = 50, int page = 1, int noOc = -1, int estado = -1)
        {

            return Ok((await new InformeBL().GetDataInforme(constructora, id, rows, page, noOc, estado)));
        }

        [HttpGet("auditoria/login")]
        public IActionResult GetInformeAuditoriaLogin(string fechai = "", string fechaf = "", int usuario = 0, int rows = 0, int page = 0)
        {
            int proveedor = _IUser.GetUser(HttpContext).idEmpresa ;
            return Ok(new InformeBL().GetInformeAuditoriaLogin(proveedor, fechai, fechaf, usuario, rows, page));
        }

        [HttpGet]
        [Route("PaginasAC")]
        public IActionResult GetUsersAll(string tipo = "", string filter = "")
        {
            if (filter == "_") filter = string.Empty;

            return Ok(new InformeBL().GetPaginas( filter: filter));
        }

        [HttpGet("auditoria/paginasVisitadas")]
        public IActionResult GetInfPaginasVisitadas(string fechai = "", string fechaf = "", string usuario ="-1", int idMenu =0, int rows = 0, int page = 0
                                                    ,int xUsuario =0 , int xFecha = 0, int xHora = 0, int xPagina = 0)
        {

            int proveedor = _IUser.GetUser(HttpContext).idEmpresa;
            return Ok(new InformeBL().GetInfPaginasVisitadas( fechai, fechaf, usuario, idMenu, rows, page, xUsuario, xFecha, xHora, xPagina, proveedor));
        }

    }
}
