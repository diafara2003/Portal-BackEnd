
namespace Code.Repository.Session.Model
{
   public class UserSessionDTO
    {
        public int id { get; set; }
        public int idEmpresa { get; set; }
        public string nit { get; set; }
        public byte[] logo { get; set; }
        public string nombreEmpresa { get; set; }
        public string nombreUsuario { get; set; }       
        public string userCorreo { get; set; }
        public string URLtipo { get; set; }
        


    }
}
