using API.Attributes;
using Code.Repository.Model.DTO.Especialidades;
using Code.Repository.RepositoryBL.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers.Especialidades
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadERPController : ControllerBase
    {
        /// <summary>
        /// consulta todas las especialidades registradas en el sistema
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<EspecialidadDTO> GetEspecialidades() => new EspecialidadBL().GetEspecialidades();


        [HttpGet("Grupos")]
        public IEnumerable<EspecialidadDTO> GetGrupos(string filter = "")
        {

            if (string.IsNullOrEmpty(filter)) filter = string.Empty;
            else if (filter == "_") filter = string.Empty;

            return new EspecialidadBL().GetGrupos(filter.TrimStart().TrimEnd());
        }


        [HttpGet("Categorias")]
        public IEnumerable<EspecialidadDTO> GetCategorias( int grupo=0,string filter = "")
        {
            if (string.IsNullOrEmpty(filter)) filter = string.Empty;
            else if (filter == "_") filter = string.Empty;

            return new EspecialidadBL().GetCategorias(grupo, filter.TrimStart().TrimEnd());
        }

        [HttpGet("Categorias/especialidad")]
        public IEnumerable<EspecialidadDTO> GetEspecialidadxCategorias(int categoria = 0, string filter = "")
        {
            if (string.IsNullOrEmpty(filter)) filter = string.Empty;
            else if (filter == "_") filter = string.Empty;

            return new EspecialidadBL().GetEspecialidadxCategorias(categoria, filter.TrimStart().TrimEnd());
        }



        [HttpGet]        
        [EnableCors("CorsPolicy")]
        [ApiKey]
        [AllowAnonymous]
        [Route("Listado")]
        public IEnumerable<EspecialidadDTO> GetEspecialidades(string filter)
        {
            if (string.IsNullOrEmpty(filter)) filter = string.Empty;
            else if (filter == "_") filter = string.Empty;
            return new EspecialidadBL().GetEspecialidades(filter);
        }



    }
}
