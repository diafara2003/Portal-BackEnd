using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity
{
    [Table("AuditoriaLogin", Schema = "dbo")]
    public class AuditoriaLogin
    {
        [Key]
        public int id { get; set; }
        public int usuario { get; set; }
        public DateTime fecha { get; set; }
        public int tercero { get; set; }

    }
}
