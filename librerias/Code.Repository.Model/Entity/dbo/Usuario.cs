using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code.Repository.Model.Entity
{
    [Table("Usuario", Schema = "dbo")]
    public class Usuario
    {
        [Key]
        public int UserId { get; set; }        
        public bool UserPpal { get; set; }
        public int UserIdPpal { get; set; }
        public string UserCorreo { get; set; }
        public string UserClave { get; set; }
        public DateTime UserFechaRegistro { get; set; }
        public int UserEstado { get; set; }        
        public int? UserNivel { get; set; }        
        public string UserNombre { get; set; }
        public string UserCargo { get; set; }
        public string UserTipoD { get; set; }
        public string UserDoc { get; set; }
        public string UserCelular { get; set; }

    }
}
