using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity.dbo
{
    [Table("TokenERP", Schema = "dbo")]
    public class TokenERP
    {
        [Key]
        public int Id { get; set; }
        public int IdConstructora { get; set; }
        public string Token { get; set; }
        public DateTime FechaToken { get; set; }
    }
}
