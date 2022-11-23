using Code.Repository.Model.Entity;
using Code.Repository.RepositoryBL.Operations;
using Code.Repository.Session.Implementation;
using Code.Repository.Session.Model;
using Microsoft.AspNetCore.Http;
using System.Linq;


namespace Code.Repository.Session.Implementation
{


    public interface IconstructoraRepository
    {
        public static string APIKEYNAME = "ApiKey";

        Constructora Get(HttpContext _);
    }

    public class ConstructoraRepository : IconstructoraRepository
    {

        public Constructora Get(HttpContext _)
        {
            string id = _.Request.Headers.Where(c => c.Key.Equals(IconstructoraRepository.APIKEYNAME)).Select(c => c.Value).FirstOrDefault();

            return new ConstucturasBL().GetContructoraKey(id);
        }
    }
}
