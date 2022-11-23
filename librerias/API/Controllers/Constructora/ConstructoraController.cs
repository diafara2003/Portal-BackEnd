using API.Attributes;
using Code.Repository.Model.DTO.Constructora;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Code.Repository.Session.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Constructora
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConstructoraController : ControllerBase
    {
        private readonly IUserRepository _IUser;

        public ConstructoraController(IUserRepository IUser)
        {
            _IUser = IUser;
        }


        [HttpGet("{id:int}")]
        public IActionResult GetConstructoraXId(int id)
        {

            try
            {
                UserSessionDTO _user = _IUser.GetUser(HttpContext);

                return Ok(new ConstucturasBL()
                    .ObtenerConstructoraxTercero(_user.id)
                    .Where(c => c.id == id)
                    .FirstOrDefault());
            }
            catch
            {
                return Ok(new List<ConstructoraDTO>());
            }
        }


        [HttpGet]
        public IActionResult GetConstructora(string filter = "")
        {

            try
            {
                UserSessionDTO _user = _IUser.GetUser(HttpContext);

                return Ok(new ConstucturasBL().ObtenerConstructoraxTercero(_user.idEmpresa, filter));
            }
            catch
            {
                return Ok(new List<ConstructoraDTO>());
            }
        }
    }
}
