using Code.Repository.Document.Operations;
using Code.Repository.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.Documentos
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosERPController : ControllerBase
    {

        private readonly IUserRepository _IUser;
        public DocumentosERPController(IUserRepository IUser)
        {
            _IUser = IUser;
        }


        [HttpGet("requeridos")]
        public async Task<IActionResult> GetREqueridos(int constructora)
        {
            var currentUser = _IUser.GetUser(HttpContext);

            return Ok(await  new DocumentosERP().GetDocumentosRequeridosconstructora(constructora, currentUser.idEmpresa));
           

        }


    }
}
