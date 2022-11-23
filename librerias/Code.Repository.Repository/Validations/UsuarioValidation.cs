using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Usuarios;
using System.Linq;

namespace Code.Repository.RepositoryBL.Validations
{
    public class UsuarioValidation
    {

        public bool ExisteUsuario(UsuarioDTO request)
        {

            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();


            var result = (from u in objcnn.usuario
                          where u.UserCorreo == request.correo
                          select u);

            if (result.Count() > 0) return true;
            else return false;

        }

    }
}
