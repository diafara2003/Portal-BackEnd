using Code.Repository.Model.DTO.Auditoria;
using Code.Repository.RepositoryBL.Operations.Auditoria;
using Code.Repository.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Auditoria
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
       
        private readonly IUserRepository _IUser;
        public AuditoriaController(IUserRepository IUser)
        {
            _IUser = IUser;
        }

        [HttpGet("auditoriaGeneral")]
        public IActionResult GetAuditoriaUltimaMod(int idTipoAuditoria , int documento, bool isDelete , bool isNew)
        {
            var currentUser = _IUser.GetUser(HttpContext);

            return Ok(new AuditoriaBL().getAuditoria(documento, idTipoAuditoria, isDelete, isNew));
        }

        [HttpGet("AuditCampoDetalle")]
        public IActionResult GetDetalleCampoAuditoria(string nameSQl , string nameHMTL, int idTipoAuditoria, int documento, bool isDelete, bool isNew)
        {
            var currentUser = _IUser.GetUser(HttpContext);
            Glosario glosario = new Glosario { NombreHTML = nameHMTL, NombreSQL = nameSQl }; 
            return Ok(new AuditoriaBL().DetalleCampoAuditoria(documento, glosario, idTipoAuditoria, isDelete, isNew));
        }

        //[HttpGet("AuditXFecha")]
        //public IActionResult GetAuditoriaxFecha(string nameSQl, string nameHMTL, string idTipoAuditoria)
        //{
        //    var currentUser = _IUser.GetUser(HttpContext);
        //    Glosario glosario = new Glosario { NombreHTML = nameHMTL, NombreSQL = nameSQl };
        //    return Ok(new AuditoriaBL().DetalleCampoAuditoria(currentUser.idEmpresa, glosario, idTipoAuditoria));
        //}


    }
}
