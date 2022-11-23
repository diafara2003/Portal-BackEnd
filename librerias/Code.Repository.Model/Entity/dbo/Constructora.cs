
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Code.Repository.Model.Entity
{
    [Table("Constructoras", Schema = "dbo")]
    public class Constructora
    {
        [Key]
        public int ConstId{ get; set; }
        
        public string ConstNIT { get; set; }
        public string ConstRuta_API { get; set; }       
        public string ConstUrlLogo { get; set; }
        public string ConstNombre { get; set; }
        public Guid ConsApiKey { get; set; }
    }
}
