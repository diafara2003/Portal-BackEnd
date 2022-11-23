using Code.Repository.Model.DTOAuditoria;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.MappAuditoria
{
    public static class MapTerNotificacionProveedor
    {
        public static TerNotificacionProveedorDTOAuditoria MapToAuditoriaNotiProve(this Usuario usuario)
        {
            return new TerNotificacionProveedorDTOAuditoria()
            {
              Cargo = usuario.UserCargo,
              Celular = usuario.UserCelular,
              Correo = usuario.UserCorreo,
              Nombres = usuario.UserNombre             
            };
        }

        public static TerNotificacionLicitacionDTOAuditoria MapToAuditoriaNotiLici(this Usuario usuario, CategoriasTercero _categoria,
                                                                                            Constructora _constructora, Notificaciones _noti)
        {

            return new TerNotificacionLicitacionDTOAuditoria()
            {
                Cargo = usuario.UserCargo,
                Celular = usuario.UserCelular,
                Nombres = usuario.UserNombre,
                Correo = usuario.UserCorreo,
                NombreCategoria = _categoria == null ? "" : _categoria.CatTexto,
                Constructora = _constructora == null ? "" : _constructora.ConstNombre,
                Zona = _noti.NotZona
            };
        }
    }
}
