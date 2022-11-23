

using System.Collections.Generic;

namespace Code.Repository.Email.Model
{
    public class UserEMailDTO
    {
        public int id { get; set; }
        public string correo { get; set; }
        public string nombreEmpresa { get; set; }
        public string logoEMpresa { get; set; }
        public string clave { get; set; }
        public string NIT { get; set; }
    }

    public class UserRechazoDTO {
        public string nombreEmpresa { get; set; }
        public List<string> motivosRechazo { get; set; }
        public string comentarios { get; set; }
        public string logoEMpresa { get; set; }
    }
}
