using Code.Repository.RepositoryBL.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Licitaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new CategoriaBL().ConsultarCategorias(""));
        }

        [HttpGet]
        [Route("Listado")]
        public IActionResult ConsultarListado(string filter)
        {
            return Ok(new CategoriaBL().ConsultarCategorias(filter));
        }

    }
}
