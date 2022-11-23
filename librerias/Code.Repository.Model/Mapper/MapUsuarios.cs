

using Code.Repository.Model.DTO.Usuarios;
using Code.Repository.Model.Entity;
using System.Collections.Generic;


namespace Code.Repository.Model.Mapper
{
    public static class MapUsuarios
    {
        public static IEnumerable<UsuarioDTO> MapToDTO(this IEnumerable<Usuario> data)
        {
            List<UsuarioDTO> objlst = new List<UsuarioDTO>(); ;

            foreach (var item in data) objlst.Add(item.MapToDTO());

            return objlst;
        }


        public static UsuariosSinPerfilDTO MapToDTOPerfil(this Usuario data)
        {
            return new UsuariosSinPerfilDTO()
            {
                clave = data.UserClave,
                correo = data.UserCorreo,
                estado = data.UserEstado,
                id = data.UserId,
                isPrincipal = data.UserPpal,                
                SinPerfil = data.UserNivel == null ? true : false,
                cargo = data.UserCargo,
                documento = data.UserDoc,
                nombre = data.UserNombre,
                celular = data.UserCelular == null ? string.Empty : data.UserCelular
            };
        }


        public static UsuarioDTO MapToDTO(this Usuario data)
        {
            return new UsuarioDTO()
            {
                clave = data.UserClave,
                correo = data.UserCorreo,
                estado = data.UserEstado,
                id = data.UserId,
                isPrincipal = data.UserPpal,                
                cargo = data.UserCargo,
                documento = data.UserDoc,
                nombre = data.UserNombre,
                celular = data.UserCelular == null ? string.Empty : data.UserCelular
            };
        }

        public static Usuario MapToEntity(this UsuarioDTO data)
        {
            return new Usuario()
            {
                UserClave = data.clave,
                UserCorreo = data.correo,
                UserEstado = data.estado,
                UserId = data.id,
                UserPpal = data.isPrincipal,                
                UserCargo = data.cargo,
                UserNombre = data.nombre,
                UserDoc = data.documento,
                UserTipoD = "",
                UserCelular = data.celular == null ? string.Empty : data.celular
            };
        }
    }
}
