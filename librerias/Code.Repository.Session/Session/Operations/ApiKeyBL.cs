
using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.Entity;
using System.Linq;

namespace Code.Repository.Session.Operations
{
    public class ApiKeyBL
    {


        public Constructora GetContructoraKey(string key)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            return (from c in objcnn.constructoras
                    where c.ConsApiKey.ToString().Equals(key)
                    select c
                    ).FirstOrDefault();

        }
    }
}
