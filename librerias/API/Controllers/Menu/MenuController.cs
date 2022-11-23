using Code.Repository.Model.DTO.Menu;
using Code.Repository.Model.Entity;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session;
using Code.Repository.Session.Model;
using Code.Repository.Session.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API.Controllers.Menu
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private readonly IUserRepository _IUser;

        public MenuController(IUserRepository IUser)
        {
            _IUser = IUser;
        }


        [HttpGet]
        public IActionResult GetMenu(string cod="")
        {
           
            try
            {
                UserSessionDTO _user = _IUser.GetUser(HttpContext);

                return Ok(new MenuBL().GetMenu(_user.id,codigo:cod));
            }
            catch
            {
                return Ok(new List<Code.Repository.Model.Entity.Menu>());
            }
        }


        [HttpPost("navegacion")]
        public IActionResult InsertNavegacion(NavegacionInsertDTO request)
        {

            try
            {
                UserSessionDTO _user = _IUser.GetUser(HttpContext);
                var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;

                new MenuBL().InsertNavegacion(request.id, _user.id, remoteIpAddress.ToString());

                return Ok(string.Empty);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

    }
}
