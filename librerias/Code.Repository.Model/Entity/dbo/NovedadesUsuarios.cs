using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.Model.Entity.dbo
{
    [Table("NovedadesUsuarios")]
    public class NovedadesUsuarios
    {
        [Key]
        public int NUId { get; set; }
        public int NUNovedadId { get; set; }
        public int NUUsuarioId { get; set; }
        public bool NUVisto { get; set; }
    }
}
