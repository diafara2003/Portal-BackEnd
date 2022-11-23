
using Code.Repository.Model.DTO.Usuarios;

namespace Code.Repository.Model.DTO.Notificaciones
{
    public class NotificacionDTO
    {
        public int usuario { get; set; }
        public int? categoria { get; set; }
        public string zona { get; set; }
        public int? constructora { get; set; }
        public int id { get; set; }

    }

    public class ConsultarNotificacionDTO
    {
        public int id { get; set; }
        public UsuarioDTONotificacion usuario { get; set; }
        public string zona { get; set; }
        public Constructora.ConstructoraNotificacionDTO constructora { get; set; }
        public Especialidades.EspecialidadDTO categoria { get; set; }
    }
}
