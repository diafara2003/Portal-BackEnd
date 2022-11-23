using Code.Repository.Model.DTO.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.DTO.Usuarios
{
    public enum estadoUsuario
    {
        Activo= 1,
        Inactivo = 0,
        Incompleto = -1
    }
    public class UsuarioDTO
    {
        public int id { get; set; }
        public string tipo { get; set; }
        public bool isPrincipal { get; set; }
        [Required(ErrorMessage = "El correo es obligatorio")]
        public string correo { get; set; }
        public string clave { get; set; }
        public int estado { get; set; }
        public string perfil { get; set; }
        public string nombre { get; set; }
        public string documento { get; set; }
        public string cargo { get; set; }
        public string celular { get; set; }
    }

    public class UsuarioDTONotificacion {
        public int id { get; set; }
        public string nombres { get; set; }
        public string correo { get; set; }
        public string documento { get; set; }
        public string cargo { get; set; }
        public string celular { get; set; }
    }


    public class UsuariosSinPerfilDTO: UsuarioDTO
    {
        public bool SinPerfil { get; set; }

    }

    public class UsuarioDetalleDTO
    {
        public UsuarioDTO informacion { get; set; }
        public PerfilDTO perfiles { get; set; }
    }

    public class CambiarEstadoUsuarioDTO {
        public int usuario { get; set; }
        public bool activo { get; set; }
    }
    public class UsuarioIdDTO
    {
        public int idUsuario { get; set; }

    }
}
