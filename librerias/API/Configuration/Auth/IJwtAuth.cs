using Code.Repository.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Configuration.Auth
{
  public  interface IJwtAuth
    {
        string Authentication(Usuario user);
    }
}
