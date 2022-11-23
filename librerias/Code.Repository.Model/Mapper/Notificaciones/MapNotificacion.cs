using Code.Repository.Model.DTO.Abastecimiento.Cotizaciones;
using Code.Repository.Model.DTO.Abastecimiento.Licitaciones;
using Code.Repository.Model.DTO.Notificaciones;
using Code.Repository.Model.Entity;
using Code.Repository.Model.Entity.adp_cs;
using Code.Repository.Model.Entity.dbo;
using System.Collections.Generic;


namespace Code.Repository.Model.Mapper.NotificacionPortal
{
    public static class MapNotificacion
    {

        public static Notificaciones MapToEntity(this NotificacionDTO data, TipoNotificaciones tipo)
        {
            return new Notificaciones()
            {
                NotTipo = (int)tipo,
                NotId = 0,
                NotIdConstructora = data.constructora,
                NotIdCategoria = data.categoria,
                NotIdUsuario = data.usuario,
                NotZona = data.zona,


            };
        }

        public static ConsultarNotificacionDTO MapToDTO(this Notificaciones data,
            Constructora _constructora,
            CategoriasTercero _categoria,
            Usuario _usuario
            )
        {
            return new ConsultarNotificacionDTO()
            {

                constructora = _constructora == null ? null : new DTO.Constructora.ConstructoraNotificacionDTO()
                {
                    id = _constructora.ConstId,
                    nombre = _constructora.ConstNombre
                },
                categoria = _categoria == null ? null : new DTO.Especialidades.EspecialidadDTO()
                {
                    id = _categoria.CatId,
                    nombre = _categoria.CatTexto
                },
                usuario = new DTO.Usuarios.UsuarioDTONotificacion()
                {
                    id = _usuario.UserId,
                    cargo = _usuario.UserCargo == null ? "" : _usuario.UserCargo,
                    correo = _usuario.UserCorreo,
                    documento = _usuario.UserDoc,
                    nombres = _usuario.UserNombre,
                    celular = _usuario.UserCelular == null ? "" : _usuario.UserCelular
                },
                zona = data.NotZona == null ? "" : data.NotZona,
                id= data.NotId
            };

        }

        public static NotificacionesDTO MapNotificacionesDTO(this Novedades nov, NovedadesUsuarios novU)
        {
            return new NotificacionesDTO()
            {
                Visto = novU.NUVisto,
                Asunto = nov.NovAsunto,
                Fecha = nov.NovFecha,
                Observacion = nov.NovObservaciones

            };
        }
    }
}
