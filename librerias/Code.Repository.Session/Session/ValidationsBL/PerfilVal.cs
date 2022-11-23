using Code.Repository.EntityFramework.Context;
using Code.Repository.Model.DTO.Login;
using Code.Repository.Model.DTO.ResponseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Session.ValidationsBL
{
    public class PerfilVal
    {

        public ResponseDTO AgregarPerfil(AgregarPerfilDTO request, int empresa)
        {
            ResponseDTO obj = new ResponseDTO();
            if (string.IsNullOrEmpty(request.perfil.nombre))
            {
                obj.Success = false;
                obj.mensaje = "El nombre es Obligatorio";
            }


            //se valida si ya existe el perfil en la base de datos
            ApplicationDatabaseContext objcnn = new ApplicationDatabaseContext();

            if (request.perfil.id == 0)
            {

                if (objcnn.perfil.Where(c =>
                             c.NivEmpresa == empresa
                            && c.NivNombre.ToLower().Equals(request.perfil.nombre.ToLower())
                           ).Count() > 0)
                {
                    obj.Success = false;
                    obj.mensaje = "El nombre del grupo ya existe";

                }
            }
            else
            {

                if (objcnn.perfil.Where(c => c.NivId != request.perfil.id
                                 && c.NivEmpresa == empresa
                                && c.NivNombre.ToLower().Equals(request.perfil.nombre.ToLower())
                               ).Count() > 0)
                {
                    obj.Success = false;
                    obj.mensaje = "El nombre del grupo ya existe";

                }
            }




            return obj;

        }

    }
}
