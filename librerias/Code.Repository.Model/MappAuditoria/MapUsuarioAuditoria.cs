using Code.Repository.Model.DTO.Usuarios;
using Code.Repository.Model.DTOAuditoria;
using Code.Repository.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.MappAuditoria
{
    public static class MapUsuarioAuditoria
    {
        public static UsuarioDTOAuditoria MapToAuditoria(this UsuarioDTO data)
        {
            return new UsuarioDTOAuditoria()
            {
                UserCargo = data.cargo,
                UserCelular = data.celular,
                UserCorreo = data.correo,
                UserDoc = data.documento,
                UserEstado = data.estado,
                UserNombre = data.nombre,
            };
        }

        public static UsuarioDTOAuditoria MapToAuditoria(this Usuario data)
        {
            return new UsuarioDTOAuditoria()
            {
                UserCargo = data.UserCargo,
                UserNombre = data.UserNombre,
                UserEstado = data.UserEstado,
                UserDoc = data.UserDoc,
                UserCorreo = data.UserCorreo,
                UserCelular = data.UserCelular,
                
            };
        }
    }
}
