
using System.ComponentModel.DataAnnotations;

namespace Code.Repository.Session.Model
{
   public class UserLoginRequestDTO
    {
        [Required(ErrorMessage ="El usuario es obligatorio")]
        public string usuario { get; set; }
        [Required(ErrorMessage ="La contraseña es obligatoria")]
        public string clave { get; set; }
    }
}
