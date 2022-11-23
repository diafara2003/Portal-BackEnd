

using Code.Repository.Session.Implementation;
using Code.Repository.Session.Model;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Code.Repository.Session
{

    public interface IUserRepository
    {

        UserSessionDTO GetUser(HttpContext _);
    }

    public class UserRepository : IUserRepository
    {
        
        UserSessionDTO IUserRepository.GetUser(HttpContext _)
        {
            string id = _.User.Claims.FirstOrDefault(c => c.Type == "Id").Value;

            return new UserSession().GetUserSession(int.Parse(id));
        }
    }
}
