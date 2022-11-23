using Code.Repository.Model.DTO.TercerosIG;
using Code.Repository.RepositoryBL.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace API.Controllers.ActividadesEconomica
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActividadEconomicaController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<ActividadEconomicaDTO> Get(string filter)
        {
            if (string.IsNullOrEmpty(filter)) filter = string.Empty;
            else if (filter == "_") filter = string.Empty;

            return new ActividadEconomicaBL().Get(filter);
        }

    }
}
