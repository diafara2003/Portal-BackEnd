using Code.Repository.EntityFramework.Context;
using Code.Repository.Session.Model;
using System.Collections.Generic;
using System.Linq;

namespace Code.Repository.Session.Implementation
{
    public class UserSession
    {
        public UserSessionDTO GetUserSession(int id)
        {
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();
            UserSessionDTO objUserSession = new UserSessionDTO();

            var _info_user = objcnn.usuario.Find(id);



            objUserSession = (from _user in objcnn.usuario
                              join _tercero in objcnn.terceros on _user.UserIdPpal equals _tercero.Terid
                              join _infoGeneral in objcnn.terInfGeneral on _tercero.Terid equals _infoGeneral.TigTerceroId
                              where _user.UserId == id //&& _user.UserTipo == _infoGeneral.TigTipoEmpresa
                              select new UserSessionDTO()
                              {
                                  id = _user.UserId,
                                  idEmpresa = _tercero.Terid,
                                  logo = _infoGeneral.TerLogo,
                                  nombreEmpresa = _infoGeneral.TigNombre,
                                  URLtipo = _tercero.TerRutaLogo,
                                  nit = _infoGeneral.TigNumeroIdentificacion,
                                  userCorreo = _user.UserCorreo
                                  //  nombreUsuario = _user.UserNombre
                              }
                 ).FirstOrDefault();





            return objUserSession;
        }
    }
}
