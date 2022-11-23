
using Code.Repository.Model.DTO.Especialidades;
using Code.Repository.Model.DTO.ResponseCommon;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers.Especialidades
{

    /// <summary>
    /// api de especialidades
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class EspecialidadesController : ControllerBase
    {

        private readonly IUserRepository _IUser;

        public EspecialidadesController(IUserRepository IUser)
        {
            _IUser = IUser;
        }

        /// <summary>
        /// consulta todas las especialidades registradas en el sistema
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetEspecialidades(string filter = "")
        {

            if (string.IsNullOrEmpty(filter)) filter = string.Empty;
            else if (filter == "_") filter = string.Empty;

            var _tercero = _IUser.GetUser(HttpContext);

            return Ok(new EspecialidadBL().GetEspecialidadesFaltantes(_tercero.id, filter.TrimStart().TrimEnd()));


        }




        /// <summary>
        /// consulta todas las especialidades por tercero de sesion
        /// </summary>
        /// <returns></returns>
        [HttpGet("tercero")]
        public IActionResult GetEspecialidadesXTercero(int id)
        {
            if (id == 0)
            {
                var _tercero = _IUser.GetUser(HttpContext);
                id = _tercero.idEmpresa;
            }

            return Ok(new EspecialidadBL().GetEspecialidades(id));


        }

        /// <summary>
        /// elimina una especialidadTercero al tercero de sesion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("tercero/{id:int}")]
        public IActionResult EliminarEspecialidadTercero(int id)
        {

            var _tercero = _IUser.GetUser(HttpContext);

            new EspecialidadBL().EliminarEspecialidad(id, _tercero.idEmpresa, _tercero.id);

            return Ok(new ResponseDTO()
            {
                codigo = 1,
                mensaje = "",
                Success = true
            });
        }


        /// <summary>
        /// agrega una especialidadTercero al tercero de sesion
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("tercero")]
        public IActionResult AsociarEspecialidad(List<AsociarEspecialidadDTO> request)
        {
            var _tercero = _IUser.GetUser(HttpContext);


            new EspecialidadBL().AsociarEspecialidad(request, _tercero.idEmpresa);
            return Ok(new ResponseDTO()
            {
                codigo = 1,
                mensaje = "",
                Success = true
            });

        }

        [HttpPost("tercero/guardar")]
        public IActionResult AsociarEspecialidad(AsociarEspecialidadDTO request)
        {
            var _tercero = _IUser.GetUser(HttpContext);


            new EspecialidadBL().AsociarEspecialidad(request, _tercero.idEmpresa, _tercero.id);
            return Ok(new ResponseDTO()
            {
                codigo = 1,
                mensaje = "",
                Success = true
            });

        }
    }
}
