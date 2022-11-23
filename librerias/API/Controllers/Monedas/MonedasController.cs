using Code.Repository.Model.DTO.Monedas;
using Code.Repository.RepositoryBL.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers.Monedas
{
    [Route("api/[controller]")]
    public class MonedasController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<MonedaDTO> ConsultarMonedas()
        {
            return new MonedaBL().ConsultarMonedas();
        }
    }
}
