using Code.Repository.DAO.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ValuesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet("error")]
        public IActionResult GetError()
        {
            throw new System.Exception("Email or password is incorrect");

            
        }


        [HttpGet("token/status")]
        public async Task<IActionResult> GetToken()
        {
            var token = await new ConexionERP(5).ObtenerToken();
            return Ok($"token:{token}");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok("hola");
        }


        [HttpGet("root")]
        public async Task<IActionResult> GetRoot()
        {

            return Ok(_webHostEnvironment.ContentRootPath);
        }

        [HttpGet("sinlogo")]
        public async Task<IActionResult> GetSinLogo()
        {
            try
            {
                var image = System.IO.File.OpenRead($"{_webHostEnvironment.ContentRootPath}\\images\\sin-logopng.png");
                return File(image, "image/jpeg");
            }
            catch (System.Exception e)
            {

                return Ok(e.Message);
            }
         
        }
    }
}
