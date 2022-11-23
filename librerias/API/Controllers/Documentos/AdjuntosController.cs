using Code.Repository.Document.Operations;
using Code.Repository.Model.DTO.Adjuntos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers.Documentos
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdjuntosController : ControllerBase
    {
        [HttpGet("tipos")]
        [AllowAnonymous]
        [EnableCors("CorsPolicy")]
        public IActionResult GetTipoAdjuntos() => Ok(new AdjuntoBL().GetTipoadjuntotercero());




        [HttpGet("requeridos/categorias")]
        [AllowAnonymous]
        [EnableCors("CorsPolicy")]
        public IActionResult GetRequeridosGrupo(string categorias) => Ok(new AdjuntoBL().GetRequeridosGrupo(categorias));

    }
}
